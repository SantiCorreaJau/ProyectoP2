namespace DefaultNamespace;

public class VentaItem
{
    public string id { get; set; }
    public int cantidad { get; set; }
    public float precioUnitario { get; set; }
    public float subtotal { get; set; } //TOTAL*CANTIDAD

    public VentaItem(string unId, int unaCantidad, float unPrecioUnitario, float unSubtotal)
    {
        this.id = unId;
        this.cantidad = unaCantidad;
        this.precioUnitario = unPrecioUnitario;
        this.subtotal = unSubtotal;
    }
}