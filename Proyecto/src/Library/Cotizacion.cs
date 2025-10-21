namespace Proyecto;

public class Cotizacion
{
    public string Id { get; set; }
    public string FechaEnvio { get; set; }
    public float Importe { get; set; }
    public string Estado { get; set; }

    public Cotizacion(string id, string fechaEnvio, float importe, string estado)
    {
        Id = id;
        FechaEnvio = fechaEnvio;
        Importe = importe;
        Estado = estado;
    }
}
