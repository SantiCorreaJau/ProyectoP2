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
    }
}