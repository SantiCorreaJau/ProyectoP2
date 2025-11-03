namespace Proyecto;

public class CorreoElectronico : Interaccion
{
    public string Direccion { get; set; } // "Enviado" o "Recibido"
    public string Asunto { get; set; }  

    public string Cuerpo { get; set; }
    public CorreoElectronico(string id, string fechaHora, string tema, bool pendiente, string clienteId, 
        string vendedorId, List<Comentario> comentarios, string direccion, string asunto,string uncuerpo)
        : base(id, fechaHora, tema, pendiente, clienteId, vendedorId, comentarios)
    {
        Direccion = direccion;
        Asunto = asunto;
        Cuerpo = uncuerpo;
    }
}