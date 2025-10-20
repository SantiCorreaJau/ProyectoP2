namespace Proyecto;

public class Mensaje : Interaccion
{
    public string canal { get; set; }
    public string contenido { get; set; }

    public Mensaje(string id, string fechaHora, string tema, string direccion, string estado, string clienteId, string vendedorId, List<Comentario> comentarios, string canal, string contenido)
        : base(id, fechaHora, tema, direccion, estado, clienteId, vendedorId, comentarios)
    {
        this.canal = canal;
        this.contenido = contenido;
    }

    public override void Registrar(string idCliente)
    {
        clienteId = idCliente;
        estado = "mensaje registrado";
    }
}