namespace Proyecto;
public class Producto
{
    public string Id { get; set; }
    public string Nombre { get; set; }
    public string Sku { get; set; }
    public string Descripcion { get; set; }

    public Producto(string id, string nombre, string sku, string descripcion)
    {
        Id = id;
        Nombre = nombre;
        Sku = sku;
        Descripcion = descripcion;
    }
}