namespace DefaultNamespace;

public class Llamada : Interaccion
{
    public int duracionMin { get; set; }

    public Llamada(
        string id,
        string fechaHora,
        string tema,
        string direccion,
        string estado,
        string clienteId,
        string vendedorId,
        List<Comentario> comentarios,
        int duracionMin)
        : base(id, fechaHora, tema, direccion, estado, clienteId, vendedorId, comentarios)
    {
        this.duracionMin = duracionMin;
    }

    public override void Registrar(string id,
        string fechaHora,
        string tema,
        string direccion,
        string estado,
        string clienteId,
        string vendedorId,
        List<Comentario> comentarios,
        int duracionMin)
    {
        Llamada nuevaLlamada = new Llamada(
            id, fechaHora, tema, direccion, estado,
            clienteId, vendedorId, comentarios, duracionMin
        );

        RepositorioInteracciones.add(nuevaLlamada);
    }
}