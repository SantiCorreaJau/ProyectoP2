namespace Proyecto;

public class Reunion : Interaccion
{
    public string Lugar { get; set; }

    public Reunion(
        string id,
        string fechaHora,
        string tema,
        string estado,
        string clienteId,
        string vendedorId,
        List<Comentario> comentarios,
        string lugar)
        : base(id, fechaHora, tema, estado, clienteId, vendedorId, comentarios)
    {
        Lugar = lugar;
    }

    public override void Registrar(RepositorioInteracciones repositorio)
    {
        
        repositorio.Agregar(this);
    }
}