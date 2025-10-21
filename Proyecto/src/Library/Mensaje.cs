namespace Proyecto;

public class Mensaje : Interaccion
{
    public string Direccion { get; set; } 
    public string Contenido { get; set; } 
    public string Canal { get; set; } 
    
    public Mensaje(
        string id,
        string fechaHora,
        string tema,
        string estado,
        string clienteId,
        string vendedorId,
        List<Comentario> comentarios,
        string direccion, string unContenido, string unCanal)
        : base(id, fechaHora, tema, estado, clienteId, vendedorId, comentarios)
    {
        Direccion = direccion;
        Contenido = unContenido;
        Canal = unCanal;
    }

    public override void Registrar(RepositorioInteracciones repositorio)
    {
        
        repositorio.Agregar(this);
    }
}