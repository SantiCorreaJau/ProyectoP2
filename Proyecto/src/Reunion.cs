namespace Proyecto;

public class Reunion : Interaccion
{
    public string lugar { get; set; }

    public Reunion(string id, string fechaHora, string tema, string direccion, string estado, string clienteId, string vendedorId, List<Comentario> comentarios, string lugar)
        : base(id, fechaHora, tema, direccion, estado, clienteId, vendedorId, comentarios)
    {
        this.lugar = lugar;
    }

    public override void Registrar(string idCliente)
    {
        clienteId = idCliente;
        estado = "reunión registrada";
    }
}