namespace Proyecto;

public class RepositorioClientes
{
    public List<Cliente> RepoClientes { get; set; }   // coleccion en memoria de clientes

    public RepositorioClientes()
    {
        RepoClientes = new List<Cliente>();           // arrancamos vacio
    }

    public void crearCliente(
        string id,
        string nombre,
        string apellido,
        string telefono,
        string email,
        string genero,
        string fechaNacimiento,
        string creadoEn,
        string vendedorId)
    {
        Cliente nuevo = new Cliente(id, nombre, apellido, telefono, email, genero, fechaNacimiento, creadoEn);
        RepoClientes.Add(nuevo);
    }


    public void modificarCliente(
        string idCliente,
        string nombre = null,
        string apellido = null,
        string telefono = null,
        string email = null,
        string genero = null,
        string fechaNacimiento = null)
    {
        // busca el cliente y, si existe, actualiza solo los campos no nulos
        Cliente c = buscarCliente(idCliente);
        if (c != null)
        {
            c.Actualizar(nombre, apellido, telefono, email, genero, fechaNacimiento);
        }
        // si no existe, no hace nada (comportamiento silencioso)
    }

    public void eliminarCliente(string idCliente)
    {
        // busca y, si lo encuentra, lo saca de la lista
        Cliente c = buscarCliente(idCliente);
        if (c != null)
        {
            RepoClientes.Remove(c);
        }
    }

    public Cliente buscarCliente(string criterio)
    {
        // busca por coincidencia exacta en id, nombre, apellido o email (la primera que matchee)
        foreach (Cliente c in RepoClientes)
        {
            if (c.id == criterio || c.nombre == criterio || c.apellido == criterio || c.email == criterio)
            {
                return c;                              // devuelve al primer match y corta
            }
        }
        return null;                                   // si no hay coincidencias, null
    }

    public List<Cliente> listarClientes()
    {
        return RepoClientes;                           // devuelve la lista “viva” (se puede modificar desde afuera)
    }

    public void reasignarCliente(string idCliente, string idVendedorNuevo)
    {
        //aca reasignamos el cliente a otro vendedor
        Cliente c = buscarCliente(idCliente);
        if (c != null)
        {
            c.id = idVendedorNuevo;                    
        }
    }
}
