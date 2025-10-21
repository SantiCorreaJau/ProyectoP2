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

        // --------------------------------------------------------------------
        // Historial de interacciones por cliente
        // --------------------------------------------------------------------
        public List<Interaccion> verHistorialInteracciones(string idCliente, RepositorioInteracciones repo)
        {
            var resultado = new List<Interaccion>();
            if (repo?.RepoInteracciones == null) return resultado;

            foreach (var i in repo.RepoInteracciones)
            {
                if (i != null && i.clienteId == idCliente)
                    resultado.Add(i);
            }
            return resultado;
        }

        // --------------------------------------------------------------------
        // Clientes sin contacto desde "dd-MM-yy" (usa DateTime.TryParseExact)
        // --------------------------------------------------------------------
        public List<string> verClientesSinContactoDesde(string fecha, RepositorioInteracciones repo)
        {
            var clientesSinContacto = new List<string>();
            if (repo?.RepoInteracciones == null) return clientesSinContacto;

            if (!DateTime.TryParseExact(fecha, "dd-MM-yy", CultureInfo.InvariantCulture,
                                        DateTimeStyles.None, out var limite))
                return clientesSinContacto;

            var ultimoPorCliente = new Dictionary<string, DateTime>();
            const string formato = "dd-MM-yy";

            foreach (var i in repo.RepoInteracciones)
            {
                if (i == null || string.IsNullOrWhiteSpace(i.clienteId) || string.IsNullOrWhiteSpace(i.fecha))
                    continue;

                if (!DateTime.TryParseExact(i.fecha, formato, CultureInfo.InvariantCulture,
                                            DateTimeStyles.None, out var f))
                    continue;

                if (!ultimoPorCliente.TryGetValue(i.clienteId, out var existente) || f > existente)
                    ultimoPorCliente[i.clienteId] = f;
            }

            foreach (var kv in ultimoPorCliente)
                if (kv.Value < limite) clientesSinContacto.Add(kv.Key);

            return clientesSinContacto;
        }

        // --------------------------------------------------------------------
        // Interacciones no leídas/pendientes
        // --------------------------------------------------------------------
        public List<Interaccion> verNoLeidos(List<Interaccion> listaInteracciones)
        {
            var resultado = new List<Interaccion>();
            if (listaInteracciones == null) return resultado;

            foreach (var i in listaInteracciones)
            {
                if (i == null || string.IsNullOrWhiteSpace(i.estado)) continue;
                var s = i.estado.Trim();
                if (s.Equals("no leído", StringComparison.OrdinalIgnoreCase) ||
                    s.Equals("no leido", StringComparison.OrdinalIgnoreCase) ||
                    s.Equals("pendiente", StringComparison.OrdinalIgnoreCase))
                {
                    resultado.Add(i);
                }
            }
            return resultado;
        }

        // --------------------------------------------------------------------
        // Mostrar interacciones en rango (incluye extremos)
        // --------------------------------------------------------------------
        public void verTotalRango(string fechaInicial, string fechaFinal, RepositorioInteracciones repo)
        {
            if (repo?.RepoInteracciones == null)
            {
                Console.WriteLine($"Interacciones entre {fechaInicial} y {fechaFinal}: 0 (repositorio vacío)");
                return;
            }

            if (!DateTime.TryParseExact(fechaInicial, "dd-MM-yy", CultureInfo.InvariantCulture,
                                        DateTimeStyles.None, out var desde))
            {
                Console.WriteLine("Fecha inicial inválida: " + fechaInicial);
                return;
            }
            if (!DateTime.TryParseExact(fechaFinal, "dd-MM-yy", CultureInfo.InvariantCulture,
                                        DateTimeStyles.None, out var hasta))
            {
                Console.WriteLine("Fecha final inválida: " + fechaFinal);
                return;
            }

            Console.WriteLine($"Interacciones entre {fechaInicial} y {fechaFinal}:");

            foreach (var i in repo.RepoInteracciones)
            {
                if (i == null || string.IsNullOrWhiteSpace(i.fecha)) continue;
                if (!DateTime.TryParseExact(i.fecha, "dd-MM-yy", CultureInfo.InvariantCulture,
                                            DateTimeStyles.None, out var f))
                    continue;

                if (f < desde || f > hasta) continue;

                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("ID: " + i.id);
                Console.WriteLine("Fecha: " + i.fecha);
                Console.WriteLine("Tipo: " + i.GetType().Name);
                Console.WriteLine("Cliente: " + i.clienteId);
                Console.WriteLine("Vendedor: " + i.vendedorId);
                Console.WriteLine("Tema: " + i.tema);
                Console.WriteLine("Estado: " + i.estado);

                // Propiedades que existen según tus clases
                if (i is Llamada llamadax)
                {
                    Console.WriteLine("Duración (min): " + llamadax.DuracionMin);
                }
                else if (i is Reunion reunionx)
                {
                    Console.WriteLine("Lugar: " + reunionx.Lugar);
                }
                else if (i is Mensaje mensajex)
                {
                    Console.WriteLine("Canal: " + mensajex.Canal);
                    Console.WriteLine("Contenido: " + mensajex.Contenido);
                }
                else if (i is CorreoElectronico correox)
                {
                    Console.WriteLine("Dirección: " + correox.Direccion); // Enviado/Recibido
                    Console.WriteLine("Asunto: " + correox.Asunto);
                }
            }
        }

        // --------------------------------------------------------------------
        // Resumen rápido (últimos 10 días / próximos 10 días)
        // --------------------------------------------------------------------
        public void verResumenRapido(RepositorioClientes repoClientes, RepositorioInteracciones repoInter)
        {
            int totalClientes = repoClientes?.RepoClientes?.Count ?? 0;

            DateTime hoy = DateTime.Now.Date;
            var recientes = new List<Interaccion>();
            var proximas = new List<Reunion>();

            if (repoInter?.RepoInteracciones != null)
            {
                foreach (var i in repoInter.RepoInteracciones) // <- usar repoInter acá
                {
                    if (i == null || string.IsNullOrWhiteSpace(i.fecha)) continue;
                    if (!DateTime.TryParseExact(i.fecha, "dd-MM-yy", CultureInfo.InvariantCulture,
                                                DateTimeStyles.None, out var f))
                        continue;

                    var fecha = f.Date;
                    var diasDesde = (hoy - fecha).TotalDays;

                    if (diasDesde >= 0 && diasDesde <= 10)
                        recientes.Add(i);

                    if (i is Reunion r)
                    {
                        var diasHasta = (fecha - hoy).TotalDays;
                        if (diasHasta >= 0 && diasHasta <= 10)
                            proximas.Add(r);
                    }
                }
            }

            Console.WriteLine("===== Resumen Rápido =====");
            Console.WriteLine("Clientes totales: " + totalClientes);
            Console.WriteLine("Interacciones recientes (últimos 10 días): " + recientes.Count);
            Console.WriteLine("Reuniones próximas (próximos 10 días): " + proximas.Count);
            Console.WriteLine();

            Console.WriteLine("---- Interacciones recientes ----");
            foreach (var r in recientes)
            {
                Console.WriteLine(
                    $"{r.fecha} | {r.GetType().Name} | Cliente: {r.clienteId} | Vendedor: {r.vendedorId} | Tema: {r.tema} | Estado: {r.estado}"
                );
            }

            Console.WriteLine("---- Reuniones próximas ----");
            foreach (var reu in proximas)
            {
                Console.WriteLine(
                    $"{reu.fecha} | Lugar: {reu.Lugar} | Cliente: {reu.clienteId} | Vendedor: {reu.vendedorId} | Tema: {reu.tema}"
                );
            }

            Console.WriteLine("============================");
        }

        // --------------------------------------------------------------------
        // Comentarios
        // --------------------------------------------------------------------
        public void agregarComentario(string idInteraccion, string texto, RepositorioInteracciones repo)
        {
            if (repo?.RepoInteracciones == null)
            {
                Console.WriteLine("Repositorio vacío.");
                return;
            }

            foreach (var i in repo.RepoInteracciones)
            {
                if (i == null) continue;
                if (i.id == idInteraccion)
                {
                    i.comentarios ??= new List<Comentario>();
                    string fechaActual = DateTime.Now.ToString("dd-MM-yy");
                    var nuevo = new Comentario("C" + (i.comentarios.Count + 1),
                                               texto ?? "", fechaActual, this.id, i.id, true);
                    i.comentarios.Add(nuevo);

                    Console.WriteLine("Comentario agregado a la interacción " + idInteraccion);
                    return;
                }
            }

            Console.WriteLine("No se encontró la interacción con ID " + idInteraccion);
        }

        // --------------------------------------------------------------------
        // Etiquetas -> trabajar SOLO con cliente.etiquetasIds (Cliente NO tiene 'etiquetas')
        // --------------------------------------------------------------------
        public void agregarEtiquetaACliente(string idCliente, string idEtiqueta,
                                            RepositorioClientes repoClientes, RepositorioEtiquetas repoEtiquetas)
        {
            if (repoClientes?.RepoClientes == null || repoEtiquetas?.RepoEtiquetas == null)
            {
                Console.WriteLine("Repositorios vacíos.");
                return;
            }

            // Buscar cliente
            Cliente cliente = null;
            foreach (var c in repoClientes.RepoClientes)
            {
                if (c != null && c.id == idCliente) { cliente = c; break; }
            }
            if (cliente == null)
            {
                Console.WriteLine("No se encontró el cliente con ID " + idCliente);
                return;
            }

            // Buscar etiqueta (solo para mostrar su nombre al final)
            Etiqueta etiqueta = null;
            foreach (var e in repoEtiquetas.RepoEtiquetas)
            {
                if (e != null && e.id == idEtiqueta) { etiqueta = e; break; }
            }
            if (etiqueta == null)
            {
                Console.WriteLine("No se encontró la etiqueta con ID " + idEtiqueta);
                return;
            }

            // Asegurar lista de IDs
            cliente.etiquetasIds ??= new List<string>();

            // Evitar duplicado
            foreach (var existingId in cliente.etiquetasIds)
            {
                if (existingId == idEtiqueta)
                {
                    Console.WriteLine("El cliente ya tiene esta etiqueta asignada.");
                    return;
                }
            }

            cliente.etiquetasIds.Add(idEtiqueta);
            Console.WriteLine("Etiqueta '" + etiqueta.nombre + "' agregada al cliente " + cliente.nombre);
        }
    }
}
