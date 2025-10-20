namespace Proyecto;

public class RepositorioInteracciones
{
    public List<Interaccion> RepoInteracciones { get; set; }

    public RepositorioInteracciones()
    {
        RepoInteracciones = new List<Interaccion>();
    }

    public void Agregar(Interaccion inter) //se creo metodo para poder agregar a la lisa de Interacciones
    {
        RepoInteracciones.Add(inter);
    }
    
   // public List<Interaccion> Filtrar
    
}