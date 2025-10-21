namespace Proyecto;

public class Llamada : Interaccion
{
    public int DuracionMin { get; set; }

    public Llamada(string id, string fechaHora, string tema, string estado, string clienteId, string vendedorId, 
        List<Comentario> comentarios, int duracionMin)
        : base(id, fechaHora, tema, estado, clienteId, vendedorId, comentarios)
    {
        DuracionMin = duracionMin;
    }

    public override void Registrar(RepositorioInteracciones repositorio)
    {
        repositorio.Agregar(this);
    }
}