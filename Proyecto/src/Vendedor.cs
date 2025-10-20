namespace Proyecto;

public class Vendedor : IUsuario
{
    public string id { get; set; }
    public string nombre { get; set; }
    public string apellido { get; set; }
    public string email { get; set; }
    public string telefono { get; set; }
    public bool activo { get; set; }
    public List<Cliente> Clientes { get; private set; }

    public Vendedor(string unId, string unNombre, string unApellido, string unTelefono, string unMail)
    {
        id = unId;
        nombre = unNombre;
        apellido = unApellido;
        telefono = unTelefono;
        email = unMail;
        activo = true;
        Clientes = new List<Cliente>();
    }

    public void CrearCliente(string id,string nombre, string apellido, string telefono, string mail,string genero,
        string fecha,string creado)
    {
        Cliente cl = new Cliente(id, nombre, apellido, telefono, mail, genero, fecha, creado);
        Clientes.Add(cl);
    }
    
    // Modificar cliente existente (actualización parcial)
    public void ModificarCliente(
        Cliente cliente,
        string nombre = null,
        string apellido = null,
        string telefono = null,
        string email = null,
        string genero = null,
        string fechaNacimiento = null
    )
    {
        // Delegamos al propio cliente para que actualice sus datos
        cliente.Actualizar(
            nombre: nombre,
            apellido: apellido,
            telefono: telefono,
            email: email,
            genero: genero,
            fechaNacimiento: fechaNacimiento
        );
    }
}