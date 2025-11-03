namespace Proyecto;

public class Mensaje : Interaccion
{
    public string Direccion { get; set; } 
    public string Contenido { get; set; } 
    public string Canal { get; set; } 

    public Mensaje(string id, string fechaHora, string tema, bool pendiente,
        string clienteId, string vendedorId, List<Comentario> comentarios, string direccion, string unContenido, string unCanal)
        : base(id, fechaHora, tema, pendiente, clienteId, vendedorId, comentarios)
    {
        Direccion = direccion;
        Contenido = unContenido;
        Canal = unCanal;
    }
}