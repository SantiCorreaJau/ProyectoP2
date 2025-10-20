namespace Proyecto;

public class RepositorioClientes
{
    public List<Cliente> RepoClientes { get; set; }

    public RepositorioClientes()
    {
        RepoClientes = new List<Cliente>();
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
        nuevo.vendedorId = vendedorId;
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
        Cliente c = buscarCliente(idCliente);
        if (c != null)
        {
            c.Actualizar(nombre, apellido, telefono, email, genero, fechaNacimiento);
        }
    }

    public void eliminarCliente(string idCliente)
    {
        Cliente c = buscarCliente(idCliente);
        if (c != null)
        {
            RepoClientes.Remove(c);
        }
    }

    public Cliente buscarCliente(string criterio)
    {
        foreach (Cliente c in RepoClientes)
        {
            if (c.id == criterio || c.nombre == criterio || c.apellido == criterio || c.email == criterio)
            {
                return c;
            }
        }
        return null;
    }

    public List<Cliente> listarClientes()
    {
        return RepoClientes;
    }

    public void reasignarCliente(string idCliente, string idVendedorNuevo)
    {
        Cliente c = buscarCliente(idCliente);
        if (c != null)
        {
            c.vendedorId = idVendedorNuevo;
        }
    }
}