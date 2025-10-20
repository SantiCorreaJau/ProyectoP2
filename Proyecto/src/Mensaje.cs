namespace Proyecto;

public class Mensaje : Interaccion
{
    public string Direccion { get; set; } 

    public Mensaje(
        string id,
        string fechaHora,
        string tema,
        string estado,
        string clienteId,
        string vendedorId,
        List<Comentario> comentarios,
        string direccion)
        : base(id, fechaHora, tema, estado, clienteId, vendedorId, comentarios)
    {
        Direccion = direccion;
    }

    public override void Registrar(RepositorioInteracciones repositorio)
    {
        
        repositorio.Agregar(this);
    }
}