namespace Proyecto;

public class CorreoElectronico : Interaccion
{
    public string Direccion { get; set; } // "Enviado" o "Recibido"
    public string Asunto { get; set; }

    public CorreoElectronico(
        string id,
        string fechaHora,
        string tema,
        string estado,
        string clienteId,
        string vendedorId,
        List<Comentario> comentarios,
        string direccion,
        string asunto)
        : base(id, fechaHora, tema, estado, clienteId, vendedorId, comentarios)
    {
        Direccion = direccion;
        Asunto = asunto;
    }

    public override void Registrar(RepositorioInteracciones repositorio)
    {
        
        repositorio.Agregar(this);
    }
}