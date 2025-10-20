namespace DefaultNamespace;

public class Venta
{
    public string id { get; set; }
    public string fecha { get; set; }
    public float total { get; set; }

    public Venta(string unId, string unaFecha, float unTotal)
    {
        this.id = unId;
        this.fecha = unaFecha;
        this.total = unTotal;
    }
}