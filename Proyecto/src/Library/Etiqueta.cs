namespace DefaultNamespace;

public class Etiqueta
{
    public string id { get; set; }
    public string nombre { get; set; }
    public string descripcion { get; set; }

    public Etiqueta(string unId, string unNombre, string unaDescripcion)
    {
        this.id = unId;
        this.nombre = unNombre;
        this.descripcion = unaDescripcion;
    }
}