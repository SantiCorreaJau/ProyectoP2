using System;
using System.Collections.Generic;
using System.Globalization;
using DefaultNamespace; // para Etiqueta

namespace Proyecto
{
    public class Vendedor : IUsuario
    {
        public string id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public bool activo { get; set; }

        public Vendedor(string unId, string unNombre, string unApellido, string unTelefono, string unMail)
        {
            id = unId;
            nombre = unNombre;
            apellido = unApellido;
            telefono = unTelefono;
            email = unMail;
            activo = true;
        }
        
        public List<Interaccion> verHistorialInteracciones(string idCliente, RepositorioInteracciones repo) // Devuelve una lista de todas las interacciones
                                            // de un mismo cliente
        {
            var resultado = new List<Interaccion>();    // Creo la lista que voy a devolver
            if (repo?.RepoInteracciones == null) return resultado;  // Si el repositorio esta vacio devuelvo la lista vacia

            foreach (var i in repo.RepoInteracciones)   // recorro el repo
            {
                if (i.clienteId == idCliente)  //Si la interaccion NO esta vacia y coincide el id del cliente lo agrego a la lista
                    resultado.Add(i);
            }
            return resultado;
        }
        
        public List<string> verClientesSinContactoDesde(string fecha, RepositorioInteracciones repo) // Devuelve una lista de clientes los cuales
                                                                                            // no tengo contacto desde una fecha formato dd-mm-aa
        {
            List<string> clientesSinContacto = new List<string>();      // Creo la lista que despues voy a devolver
    if (repo == null || repo.RepoInteracciones == null) return clientesSinContacto;     // Me fijo si el repo no esta vacio

    // Paso la fecha del formato dd-mm-aa a variables con enteros
    int dLimite = (fecha[0] - '0') * 10 + (fecha[1] - '0');
    int mLimite = (fecha[3] - '0') * 10 + (fecha[4] - '0');
    int aLimite = (fecha[6] - '0') * 10 + (fecha[7] - '0');

    // Creo un diccionario donde voy a almacenar (cliente, ultima interaccion)
    Dictionary<string, int[]> ultimoPorCliente = new Dictionary<string, int[]>();

    foreach (Interaccion i in repo.RepoInteracciones) // Recorro todas las interacciones del repositorio
    {
        string f = i.fecha;                           // Guardo la fecha en una variable corta para leer más fácil
        int d = (f[0] - '0') * 10 + (f[1] - '0');     // Día: toma caracteres 0 y 1 y los convierte a número
        int m = (f[3] - '0') * 10 + (f[4] - '0');     // Mes: toma caracteres 3 y 4 y los convierte a número
        int a = (f[6] - '0') * 10 + (f[7] - '0');     // Año (2 dígitos): toma caracteres 6 y 7 y los convierte a número

        if (!ultimoPorCliente.ContainsKey(i.clienteId))       // Si aún no guardamos una fecha para este cliente
        {
            ultimoPorCliente[i.clienteId] = new int[] { d, m, a }; // Guardamos esta como “última fecha” (día, mes, año)
        }
        else
        {
            int[] existente = ultimoPorCliente[i.clienteId];  // Recuperamos la última fecha ya guardada para el cliente
            // Comparación manual para quedarnos con la fecha más reciente:
            // 1) Si el año nuevo es mayor
            // 2) O si el año es igual pero el mes nuevo es mayor
            // 3) O si año y mes son iguales pero el día nuevo es mayor
            if (a > existente[2] ||
                (a == existente[2] && m > existente[1]) ||
                (a == existente[2] && m == existente[1] && d > existente[0]))
            {
                // Si la fecha actual de la interacción es más reciente, la reemplazamos
                ultimoPorCliente[i.clienteId] = new int[] { d, m, a };
            }
        }
    }

    // Verifica quiénes no tienen contacto desde la fecha límite
    foreach (var par in ultimoPorCliente)
    {
        int d = par.Value[0];
        int m = par.Value[1];
        int a = par.Value[2];

        // Si la última interacción fue antes de la fecha límite → agregar a la lista
        bool antes = false;

        if (a < aLimite)
            antes = true;
        else if (a == aLimite && m < mLimite)
            antes = true;
        else if (a == aLimite && m == mLimite && d < dLimite)
            antes = true;

        if (antes)
            clientesSinContacto.Add(par.Key);
    }

    return clientesSinContacto;
    
}


        
        
        public List<Interaccion> verNoLeidos(RepositorioInteracciones repo)
        {
            // Creo una lista vacía donde voy a guardar las interacciones que están sin leer o pendientes
            List<Interaccion> resultado = new List<Interaccion>();

            // Si la lista que me pasan es nula, devuelvo la lista vacía directamente
            if (repo == null)
            {
                return resultado;
            }

            // Recorro todas las interacciones de la lista
            foreach (var i in repo.RepoInteracciones)   // recorro el repo
            {
                if (i.pendiente == true) 
                    resultado.Add(i);
            }
            
            return resultado;
        }
        
        
        public void verTotalRango(string fechaInicial, string fechaFinal, RepositorioInteracciones repo)
{
    // Si el repositorio está vacío o no existe, aviso y salgo
    if (repo == null || repo.RepoInteracciones == null)
    {
        Console.WriteLine("No hay interacciones cargadas.");
        return;
    }

    // Separo la fecha inicial "dd-MM-yy" en día, mes y año
    int dDesde = (fechaInicial[0] - '0') * 10 + (fechaInicial[1] - '0');
    int mDesde = (fechaInicial[3] - '0') * 10 + (fechaInicial[4] - '0');
    int aDesde = (fechaInicial[6] - '0') * 10 + (fechaInicial[7] - '0');

    // Separo la fecha final "dd-MM-yy" en día, mes y año
    int dHasta = (fechaFinal[0] - '0') * 10 + (fechaFinal[1] - '0');
    int mHasta = (fechaFinal[3] - '0') * 10 + (fechaFinal[4] - '0');
    int aHasta = (fechaFinal[6] - '0') * 10 + (fechaFinal[7] - '0');

    Console.WriteLine("--------------------------------------------------");
    Console.WriteLine("Interacciones entre " + fechaInicial + " y " + fechaFinal + ":");

    // Recorro todas las interacciones del repositorio
    for (int x = 0; x < repo.RepoInteracciones.Count; x++)
    {
        Interaccion i = repo.RepoInteracciones[x];
        if (i == null) continue;             // Si no hay interacción, salto
        if (i.fecha == null || i.fecha.Length < 8) continue; // Si la fecha es inválida, salto

        // Separo la fecha de la interacción también en día, mes y año
        string f = i.fecha;
        int d = (f[0] - '0') * 10 + (f[1] - '0');
        int m = (f[3] - '0') * 10 + (f[4] - '0');
        int a = (f[6] - '0') * 10 + (f[7] - '0');

        // Variable para decidir si está dentro del rango
        bool dentro = false;

        // Comparación manual: año → mes → día
        if (a > aDesde && a < aHasta)
            dentro = true;
        else if (a == aDesde && a == aHasta)
        {
            // Si es el mismo año, verifico los meses y días
            if (m > mDesde && m < mHasta) dentro = true;
            else if (m == mDesde && m == mHasta && d >= dDesde && d <= dHasta) dentro = true;
            else if (m == mDesde && d >= dDesde) dentro = true;
            else if (m == mHasta && d <= dHasta) dentro = true;
        }
        else if (a == aDesde && (m > mDesde || (m == mDesde && d >= dDesde)))
            dentro = true;
        else if (a == aHasta && (m < mHasta || (m == mHasta && d <= dHasta)))
            dentro = true;

        // Si la fecha está dentro del rango, la muestro
        if (dentro)
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("ID: " + i.id);
            Console.WriteLine("Fecha: " + i.fecha);
            Console.WriteLine("Tipo: " + i.GetType().Name);
            Console.WriteLine("Cliente: " + i.clienteId);
            Console.WriteLine("Vendedor: " + i.vendedorId);
            Console.WriteLine("Tema: " + i.tema);
            Console.WriteLine("Pendiente: " + i.pendiente);

            // Según el tipo de interacción, muestro más información
            if (i is Llamada)
            {
                Llamada llam = (Llamada)i;
                Console.WriteLine("Duración (min): " + llam.DuracionMin);
            }
            else if (i is Reunion)
            {
                Reunion reu = (Reunion)i;
                Console.WriteLine("Lugar: " + reu.Lugar);
            }
            else if (i is Mensaje)
            {
                Mensaje msg = (Mensaje)i;
                Console.WriteLine("Canal: " + msg.Canal);
                Console.WriteLine("Contenido: " + msg.Contenido);
            }
            else if (i is CorreoElectronico)
            {
                CorreoElectronico mail = (CorreoElectronico)i;
                Console.WriteLine("Dirección: " + mail.Direccion);
                Console.WriteLine("Asunto: " + mail.Asunto);
            }
        }
    }
}
        
        public void verResumenRapido(RepositorioClientes repoClientes, RepositorioInteracciones repoInter)
{
    // Contador total de clientes (si el repo está vacío, 0)
    int totalClientes = 0;
    if (repoClientes != null && repoClientes.RepoClientes != null)
    {
        totalClientes = repoClientes.RepoClientes.Count;
    }

    // Obtengo la fecha de hoy con formato "dd-MM-yy"
    string hoyStr = System.DateTime.Now.ToString("dd-MM-yy");
    int dHoy = (hoyStr[0] - '0') * 10 + (hoyStr[1] - '0');
    int mHoy = (hoyStr[3] - '0') * 10 + (hoyStr[4] - '0');
    int aHoy = (hoyStr[6] - '0') * 10 + (hoyStr[7] - '0');

    // Listas para guardar interacciones recientes y reuniones próximas
    List<Interaccion> recientes = new List<Interaccion>();
    List<Reunion> proximas = new List<Reunion>();

    // Si el repositorio de interacciones existe
    if (repoInter != null && repoInter.RepoInteracciones != null)
    {
        // Recorro todas las interacciones del repositorio
        for (int i = 0; i < repoInter.RepoInteracciones.Count; i++)
        {
            Interaccion inter = repoInter.RepoInteracciones[i];

            // Validaciones básicas
            if (inter == null) continue;
            if (inter.fecha == null || inter.fecha.Length < 8) continue;

            // Separo la fecha de la interacción
            string f = inter.fecha;
            int d = (f[0] - '0') * 10 + (f[1] - '0');
            int m = (f[3] - '0') * 10 + (f[4] - '0');
            int a = (f[6] - '0') * 10 + (f[7] - '0');

            // Variable para medir la diferencia en días (calculada a mano)
            int diferenciaDias = 0;
            bool mismaFecha = (a == aHoy && m == mHoy && d == dHoy);

            // Calculo manualmente cuántos días pasaron desde la interacción hasta hoy
            if (a == aHoy && m == mHoy)
            {
                // Mismo mes y año → diferencia directa
                diferenciaDias = dHoy - d;
            }
            else if (a == aHoy && mHoy - m == 1)
            {
                // Mes anterior del mismo año → sumo los días restantes del mes anterior + días actuales
                int diasMesAnterior = 30; // sin métodos, asumimos 30 días
                diferenciaDias = (diasMesAnterior - d) + dHoy;
            }
            else if (a == aHoy - 1 && m == 12 && mHoy == 1)
            {
                // Caso especial: diciembre del año anterior → enero actual
                int diasDiciembre = 31;
                diferenciaDias = (diasDiciembre - d) + dHoy;
            }
            else
            {
                // Si está más atrás que eso, lo consideramos viejo
                diferenciaDias = 999;
            }

            // Si la interacción fue dentro de los últimos 10 días, la guardo como reciente
            if (diferenciaDias >= 0 && diferenciaDias <= 10)
            {
                recientes.Add(inter);
            }

            // Si es una reunión, verifico si es próxima (fecha posterior dentro de 10 días)
            if (inter is Reunion)
            {
                Reunion reu = (Reunion)inter;
                int diferenciaFutura = 0;

                if (a == aHoy && m == mHoy)
                {
                    diferenciaFutura = d - dHoy;
                }
                else if (a == aHoy && m - mHoy == 1)
                {
                    int diasActual = 30; // asumimos 30 días en el mes actual
                    diferenciaFutura = (diasActual - dHoy) + d;
                }
                else if (a == aHoy + 1 && mHoy == 12 && m == 1)
                {
                    int diasDiciembre = 31;
                    diferenciaFutura = (diasDiciembre - dHoy) + d;
                }
                else
                {
                    diferenciaFutura = 999;
                }

                if (diferenciaFutura >= 0 && diferenciaFutura <= 10)
                {
                    proximas.Add(reu);
                }
            }
        }
    }

    // Muestro el resumen final por consola
    Console.WriteLine("===== Resumen Rápido =====");
    Console.WriteLine("Clientes totales: " + totalClientes);
    Console.WriteLine("Interacciones recientes (últimos 10 días): " + recientes.Count);
    Console.WriteLine("Reuniones próximas (próximos 10 días): " + proximas.Count);
    Console.WriteLine();

    Console.WriteLine("---- Interacciones recientes ----");
    for (int i = 0; i < recientes.Count; i++)
    {
        Interaccion r = recientes[i];
        Console.WriteLine(r.fecha + " | " + r.GetType().Name +
                          " | Cliente: " + r.clienteId +
                          " | Vendedor: " + r.vendedorId +
                          " | Tema: " + r.tema +
                          " | Pendiente: " + r.pendiente);
    }

    Console.WriteLine("---- Reuniones próximas ----");
    for (int i = 0; i < proximas.Count; i++)
    {
        Reunion reu = proximas[i];
        Console.WriteLine(reu.fecha + " | Lugar: " + reu.Lugar +
                          " | Cliente: " + reu.clienteId +
                          " | Vendedor: " + reu.vendedorId +
                          " | Tema: " + reu.tema);
    }

    Console.WriteLine("============================");
}

        
        public void agregarComentario(string idInteraccion, string texto, RepositorioInteracciones repo)
        {
            // Construyo la fecha actual a mano (dd-MM-yy)
            int dia = DateTime.Now.Day;
            int mes = DateTime.Now.Month;
            int año = DateTime.Now.Year % 100;

            string sDia = (dia < 10 ? "0" + dia : "" + dia);
            string sMes = (mes < 10 ? "0" + mes : "" + mes);
            string sAño = (año < 10 ? "0" + año : "" + año);
            string fechaActual = sDia + "-" + sMes + "-" + sAño;

            // Recorro todas las interacciones
            foreach (Interaccion i in repo.RepoInteracciones)
            {
                // Si coincide el id, agrego el comentario
                if (i.id == idInteraccion)
                {
                    // Si la lista de comentarios no existe, la creo
                    if (i.comentarios == null)
                    {
                        i.comentarios = new List<Comentario>();
                    }

                    // Creo el nuevo comentario
                    string nuevoId = "C" + (i.comentarios.Count + 1);
                    Comentario nuevo = new Comentario(nuevoId, texto, fechaActual, this.id, i.id, true);

                    // Agrego a la lista
                    i.comentarios.Add(nuevo);

                    Console.WriteLine("Comentario agregado a la interacción " + idInteraccion);
                    return;
                }
            }

            Console.WriteLine("No se encontró la interacción con ID " + idInteraccion);
        }

        
        public void agregarEtiquetaACliente(string idCliente, string idEtiqueta, RepositorioClientes repoClientes, RepositorioEtiquetas repoEtiquetas)
        {
            // Si las listas no existen o están vacías, aviso y salgo
            if (repoClientes == null || repoClientes.RepoClientes == null || repoEtiquetas == null || repoEtiquetas.RepoEtiquetas == null)
            {
                Console.WriteLine("Los repositorios están vacíos.");
                return;
            }

            // Busco el cliente
            Cliente clienteEncontrado = null;
            foreach (Cliente c in repoClientes.RepoClientes)
            {
                if (c.id == idCliente)
                {
                    clienteEncontrado = c;
                }
            }

            if (clienteEncontrado == null)
            {
                Console.WriteLine("No se encontró el cliente con ID " + idCliente);
                return;
            }

            // Busco la etiqueta
            Etiqueta etiquetaEncontrada = null;
            foreach (Etiqueta e in repoEtiquetas.RepoEtiquetas)
            {
                if (e.id == idEtiqueta)
                {
                    etiquetaEncontrada = e;
                }
            }

            if (etiquetaEncontrada == null)
            {
                Console.WriteLine("No se encontró la etiqueta con ID " + idEtiqueta);
                return;
            }

            // Si el cliente no tiene lista de etiquetas, la creo
            if (clienteEncontrado.etiquetasIds == null)
            {
                clienteEncontrado.etiquetasIds = new List<string>();
            }

            // Verifico que no tenga ya la etiqueta
            bool yaTiene = false;
            foreach (string id in clienteEncontrado.etiquetasIds)
            {
                if (id == idEtiqueta)
                {
                    yaTiene = true;
                }
            }

            if (yaTiene)
            {
                Console.WriteLine("El cliente ya tiene esta etiqueta asignada.");
                return;
            }

            // Agrego la etiqueta
            clienteEncontrado.etiquetasIds.Add(idEtiqueta);
            Console.WriteLine("Etiqueta '" + etiquetaEncontrada.nombre + "' agregada al cliente " + clienteEncontrado.nombre);
        }

    }
}