namespace Proyecto;

public class Llamada : Interaccion
{
    public int DuracionMin { get; set; }

    public Llamada(string id, string fecha, string tema, bool  pendiente, string clienteId, string vendedorId, 
        List<Comentario> comentarios, int duracionMin)
        : base(id, fecha, tema, pendiente, clienteId, vendedorId, comentarios)
    {
        DuracionMin = duracionMin;
    }
}