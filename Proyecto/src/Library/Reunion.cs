namespace Proyecto;

public class Reunion : Interaccion
{
    //es como una interaccion cualquiera, nada mas que tiene un lugar como string
    public string Lugar { get; set; }

    public Reunion(string id, string fechaHora, string tema, bool pendiente,
        string clienteId, string vendedorId, List<Comentario> comentarios, string lugar)
        : base(id, fechaHora, tema, pendiente, clienteId, vendedorId, comentarios)
    {
        Lugar = lugar;
    }
}