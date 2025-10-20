namespace Proyecto;

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
    
    public List<Interaccion> verHistorialInteracciones(string idCliente, RepositorioInteracciones repo)
    {
        List<Interaccion> resultado = new List<Interaccion>();

        foreach (Interaccion i in repo.RepoInteracciones)
        {
            if (i.clienteId == idCliente)
            {
                resultado.Add(i);
            }
        }

        return resultado;
    }
    
    public List<string> verClientesSinContactoDesde(string fecha, RepositorioInteracciones repo)
{
    List<string> clientesSinContacto = new List<string>();  // Esta es la lista que voy a devolver
    
    int dLimite = (fecha[0] - '0') * 10 + (fecha[1] - '0'); // Convierto la fecha que me pasaron (dd-mm-aa) a int
    int mLimite = (fecha[3] - '0') * 10 + (fecha[4] - '0');
    int aLimite = (fecha[6] - '0') * 10 + (fecha[7] - '0');
    
    Dictionary<string, string> ultimaFechaCliente = new Dictionary<string, string>(); // Creamos un diccionario
    //                                              donde almacenamos Cliente y ultima fecha de contaco

    foreach (Interaccion i in repo.RepoInteracciones)   // Recorro el repositorio de interacciones
    {
        string idCliente = i.clienteId;            
        string fechaInt = i.fecha;
        
        if (!ultimaFechaCliente.ContainsKey(idCliente)) // Si el cliente no estaba en el diccionario, lo agregamos
        {
            ultimaFechaCliente[idCliente] = fechaInt;
        }
        else // si el cliente ya estaba en el diccionario, tengo que quedarme con la interaccion mas reciente
        {
            string fGuardada = ultimaFechaCliente[idCliente]; // Almaceno la que ya tenia guardada y la comparo con la que estoy revisando ahora

            int d1 = (fechaInt[0] - '0') * 10 + (fechaInt[1] - '0');
            int m1 = (fechaInt[3] - '0') * 10 + (fechaInt[4] - '0');
            int a1 = (fechaInt[6] - '0') * 10 + (fechaInt[7] - '0');

            int d2 = (fGuardada[0] - '0') * 10 + (fGuardada[1] - '0');
            int m2 = (fGuardada[3] - '0') * 10 + (fGuardada[4] - '0');
            int a2 = (fGuardada[6] - '0') * 10 + (fGuardada[7] - '0');

            bool masReciente = (a1 > a2) || (a1 == a2 && m1 > m2) || (a1 == a2 && m1 == m2 && d1 > d2);

            if (masReciente)
            {
                ultimaFechaCliente[idCliente] = fechaInt;
            }
        }
    }

    foreach (string idCliente in ultimaFechaCliente.Keys)   
    {
        string f = ultimaFechaCliente[idCliente];   // Recorro las fechas de cada ultima interaccion de cada cliente

        int d = (f[0] - '0') * 10 + (f[1] - '0');
        int m = (f[3] - '0') * 10 + (f[4] - '0');
        int a = (f[6] - '0') * 10 + (f[7] - '0');

        bool anteriorALimite = (a < aLimite) ||     // Si entran en el rango determinado, las agrego a la lista que voy a devolver
                               (a == aLimite && m < mLimite) ||
                               (a == aLimite && m == mLimite && d < dLimite);

        if (anteriorALimite)
        {
            clientesSinContacto.Add(idCliente);
        }
    }

    return clientesSinContacto;         // Devuelvo la lista con los clientes y sus últimas interacciones que cumplen el criterio
}
    
    public List<Interaccion> verNoLeidos(List<Interaccion> listaInteracciones)
    {
        List<Interaccion> resultado = new List<Interaccion>();  // Lista que voy a devolver

        foreach (Interaccion i in listaInteracciones)
        {
            if (i.estado == "no leído" || i.estado == "no leido" || i.estado == "pendiente")    // Si el estado de la interaccion es 'no leido' o 'pendiente',
                                                                                                // lo agrego a la lista
            {
                resultado.Add(i);
            }
        }

        return resultado;
    }
    
    public void verTotalRango(string fechaInicial, string fechaFinal, RepositorioInteracciones repo)
{
    // Convierto las fechas limites a int
    int dDesde = (fechaInicial[0] - '0') * 10 + (fechaInicial[1] - '0');
    int mDesde = (fechaInicial[3] - '0') * 10 + (fechaInicial[4] - '0');
    int aDesde = (fechaInicial[6] - '0') * 10 + (fechaInicial[7] - '0');

    int dHasta = (fechaFinal[0] - '0') * 10 + (fechaFinal[1] - '0');
    int mHasta = (fechaFinal[3] - '0') * 10 + (fechaFinal[4] - '0');
    int aHasta = (fechaFinal[6] - '0') * 10 + (fechaFinal[7] - '0');

    Console.WriteLine("Interacciones entre " + fechaInicial + " y " + fechaFinal + ":");

    foreach (Interaccion i in repo.RepoInteracciones)
    {
        // Por cada interaccion, convierto las fechas a int
        string f = i.fecha;
        int d = (f[0] - '0') * 10 + (f[1] - '0');
        int m = (f[3] - '0') * 10 + (f[4] - '0');
        int a = (f[6] - '0') * 10 + (f[7] - '0');

        bool enRango = false; // Declaro una variable enRango

        
        if (a > aDesde && a < aHasta)       // Si la fecha de la interaccion actual esta en el rango deseado, pongo enRango = true
            enRango = true;
        else if (a == aDesde && a == aHasta)
        {
            if (m > mDesde && m < mHasta) enRango = true;
            else if (m == mDesde && m == mHasta && d >= dDesde && d <= dHasta) enRango = true;
            else if (m == mDesde && d >= dDesde) enRango = true;
            else if (m == mHasta && d <= dHasta) enRango = true;
        }
        else if (a == aDesde && (m > mDesde || (m == mDesde && d >= dDesde))) enRango = true;
        else if (a == aHasta && (m < mHasta || (m == mHasta && d <= dHasta))) enRango = true;

        if (enRango)        // Printeo todos los datos de la interaccion
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("ID: " + i.id);
            Console.WriteLine("Fecha: " + i.fecha);
            Console.WriteLine("Tipo: " + i.GetType().Name);
            Console.WriteLine("Cliente: " + i.clienteId);
            Console.WriteLine("Vendedor: " + i.vendedorId);
            Console.WriteLine("Tema: " + i.tema);
            Console.WriteLine("Estado: " + i.estado);
            
            if (i is Llamada llamadax)      // Comparo el tipo de la interaccion y agrego algunos datos extra
            {
                Console.WriteLine("Duración (min): " + llamadax.duracionMin);
            }
            else if (i is Reunion reunionx)
            {
                Console.WriteLine("Lugar: " + reunionx.lugar);
            }
            else if (i is Mensaje mensajex)
            {
                Console.WriteLine("Canal: " + mensajex.canal);
                Console.WriteLine("Contenido: " + mensajex.contenido);
            }
            else if (i is Correo correox)
            {
                Console.WriteLine("Asunto: " + correox.asunto);
                Console.WriteLine("Contenido: " + correox.contenido);
                Console.WriteLine("Remitente: " + correox.remitente);
                Console.WriteLine("Destinatario: " + correox.destinatario);
            }
        }
    }
}
    
    public void verResumenRapido(RepositorioClientes repoClientes, RepositorioInteracciones repoInter)
{
    int totalClientes = 0;  // Declaro una variable para contar clientes
    if (repoClientes != null && repoClientes.RepoClientes != null) // Si el repositorio de clientes NO está vacío, cuento la cantidad
    {
        totalClientes = repoClientes.RepoClientes.Count;
    }

    System.DateTime hoy = System.DateTime.Now;  // Registro día, mes y año actual
    int dHoy = hoy.Day;
    int mHoy = hoy.Month;
    int aHoy = hoy.Year % 100; // dd-MM-yy

    List<Interaccion> recientes = new List<Interaccion>();  // Creo una lista para interacciones recientes
    List<Reunion> proximas = new List<Reunion>();       // Creo una lista para interacciones proximas

    if (repoInter != null && repoInter.RepoInteracciones != null)   // Si el repositorio de interacciones NO está vacío, reviso cada una
    {
        foreach (Interaccion i in repoInter.RepoInteracciones)   // Recorro el repositorio de interacciones
        {
            if (i == null || string.IsNullOrEmpty(i.fecha) || i.fecha.Length < 8) continue; // Si la fecha coincide con el formato que estoy buscando, comparo

            string f = i.fecha; // Separo el formato dd-mm-aa a int
            int d = (f[0] - '0') * 10 + (f[1] - '0');
            int m = (f[3] - '0') * 10 + (f[4] - '0');
            int a = (f[6] - '0') * 10 + (f[7] - '0');

            if (a == aHoy && m == mHoy) // Si la interaccion coincide con el mes y año actual, y además tiene menos de 10 días de diferencia
                                        // con la fecha de hoy, la agrego a recientes
            {
                if (dHoy - d >= 0 && dHoy - d <= 10)
                {
                    recientes.Add(i);
                }

                if (i is Reunion)   // Ademas, si es una reunion y es dentro de los proximos 10 días, la agrego a proximas
                {
                    if (d - dHoy >= 0 && d - dHoy <= 10)
                    {
                        proximas.Add((Reunion)i);
                    }
                }
            }
        }
    }

    Console.WriteLine("===== Resumen Rápido =====");     // Muestro el panel de resumen rápido
    Console.WriteLine("Clientes totales: " + totalClientes);
    Console.WriteLine("Interacciones recientes (últimos 10 días): " + recientes.Count);
    Console.WriteLine("Reuniones próximas (próximos 10 días): " + proximas.Count);
    Console.WriteLine();

    Console.WriteLine("---- Interacciones recientes ----");
    foreach (Interaccion r in recientes)    // Recorro las interacciones recientes y las muestro
    {
        System.Console.WriteLine(
            r.fecha + " | " +
            r.GetType().Name + " | " +
            "Cliente: " + r.clienteId + " | " +
            "Vendedor: " + r.vendedorId + " | " +
            "Tema: " + r.tema + " | " +
            "Estado: " + r.estado
        );
    }
    
    Console.WriteLine("---- Reuniones próximas ----"); // Recorro las reuniones proximas y las muestro
    foreach (Reunion reu in proximas)
    {
        Console.WriteLine(
            reu.fecha + " | " +
            "Lugar: " + reu.lugar + " | " +
            "Cliente: " + reu.clienteId + " | " +
            "Vendedor: " + reu.vendedorId + " | " +
            "Tema: " + reu.tema
        );
    }

    System.Console.WriteLine("============================");
}

    public void agregarComentario(string idInteraccion, string texto, RepositorioInteracciones repo)
    {
        foreach (Interaccion i in repo.RepoInteracciones)
        {
            if (i.id == idInteraccion)
            {
                if (i.comentarios == null)
                {
                    i.comentarios = new List<Comentario>();
                }

                string fechaActual = System.DateTime.Now.ToString("dd-MM-yy");
                Comentario nuevo = new Comentario("C" + (i.comentarios.Count + 1), texto, fechaActual, this.id, i.id, true);
                i.comentarios.Add(nuevo);

                Console.WriteLine("Comentario agregado a la interacción " + idInteraccion);
                return;
            }
        }

        Console.WriteLine("No se encontró la interacción con ID " + idInteraccion);
    }

    public void agregarEtiquetaACliente(string idCliente, string idEtiqueta, RepositorioClientes repoClientes, RepositorioEtiquetas repoEtiquetas)
    {
        // Buscar cliente
        Cliente cliente = null;
        foreach (Cliente c in repoClientes.RepoClientes)
        {
            if (c.id == idCliente)
            {
                cliente = c;
                break;
            }
        }

        if (cliente == null)
        {
            Console.WriteLine("No se encontró el cliente con ID " + idCliente);
            return;
        }

        // Buscar etiqueta
        Etiqueta etiqueta = null;
        foreach (Etiqueta e in repoEtiquetas.RepoEtiquetas)
        {
            if (e.id == idEtiqueta)
            {
                etiqueta = e;
                break;
            }
        }

        if (etiqueta == null)
        {
            Console.WriteLine("No se encontró la etiqueta con ID " + idEtiqueta);
            return;
        }

        // Si el cliente no tiene lista de etiquetas, la creamos
        if (cliente.etiquetas == null)
        {
            cliente.etiquetas = new List<Etiqueta>();
        }

        // Evitar duplicados
        foreach (Etiqueta e in cliente.etiquetas)
        {
            if (e.id == etiqueta.id)
            {
                Console.WriteLine("El cliente ya tiene esta etiqueta asignada.");
                return;
            }
        }

        // Agregar la etiqueta
        cliente.etiquetas.Add(etiqueta);
        Console.WriteLine("Etiqueta '" + etiqueta.nombre + "' agregada al cliente " + cliente.nombre);
    }



}