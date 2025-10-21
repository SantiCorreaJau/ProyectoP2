using DefaultNamespace;

namespace Proyecto;


public class RepositorioEtiquetas
{
    public List<Etiqueta> RepoEtiquetas { get; set; }

    public RepositorioEtiquetas()
    {
        RepoEtiquetas = new List<Etiqueta>();
    }

    public void crearEtiqueta(string id, string nombre, string descripcion)
    {
        foreach (Etiqueta e in RepoEtiquetas)
        {
            if (e.id == id)
            {
                Console.WriteLine("El ID de etiqueta ya está en uso.");
                //return null;
            }
        }
        RepoEtiquetas.Add(new Etiqueta(id, nombre, descripcion));
    }

    public Etiqueta buscarPorId(string id)
    {
        foreach (Etiqueta e in RepoEtiquetas)
        {
            if (e.id == id) return e;
        }
        return null;
    }

    public List<Etiqueta> ListarEtiquetas()
    {
        return RepoEtiquetas;
    }

    public void EliminarEtiqueta(string id)
    {
        Etiqueta encontrada = null;
        foreach (Etiqueta e in RepoEtiquetas)
        {
            if (e.id == id) { encontrada = e; break; }
        }
        if (encontrada != null) RepoEtiquetas.Remove(encontrada);
    }

    public void RenombrarEtiqueta(string id, string nuevoNombre, string nuevaDescripcion = null)
    {
        Etiqueta e = buscarPorId(id);
        if (e != null)
        {
            if (nuevoNombre != null) e.nombre = nuevoNombre;
            if (nuevaDescripcion != null) e.descripcion = nuevaDescripcion;
        }
    }
}