namespace Proyecto;

public class Cotizacion : Interaccion
{
    public float importe { get; set; }
    public Cotizacion(string id, string fecha, string tema, bool  pendiente, string clienteId, string vendedorId, 
        List<Comentario> comentarios, float importe)
        : base(id, fecha, tema, pendiente, clienteId, vendedorId, comentarios)
    {
        this.importe = importe;
    }
}
