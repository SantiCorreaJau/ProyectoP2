namespace DefaultNamespace;

public class Vendedor
{
    public bool activo { get; set; }

    public Vendedor(string unId, string unNombre, string unApellido, string unTelefono, string unMail)
    {
        this.id = unId;
        this.nombre = unNombre;
        this.apellido = unApellido;
        this.telefono = unTelefono;
        this.email = unEmail;
    }
}