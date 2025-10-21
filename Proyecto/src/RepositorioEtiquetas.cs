using DefaultNamespace;                

namespace Proyecto                     
{                                      

public class RepositorioEtiquetas       // clase re simple, hace de "BD" en memoria pa las etiquetas
{
    public List<Etiqueta> RepoEtiquetas { get; set; }  // aca guardamos todas las etiquetas, una lista y fue

    public RepositorioEtiquetas()       // ctor, se llama cuando haces new RepositorioEtiquetas()
    {
        RepoEtiquetas = new List<Etiqueta>();          // arrancamos con la lista vacia, obvio
    }

    public void crearEtiqueta(string id, string nombre, string descripcion)  // crea una etiqueta nueva
    {
        foreach (Etiqueta e in RepoEtiquetas)          // revisamos lo que ya hay
        {
            if (e.id == id)                            // si ya existe una con ese id...
            {
                Console.WriteLine("El ID de etiqueta ya esta en uso.");  // tiramos el warning por consola
                //return null;                         // si de verdad queres bloquear el alta, descomentá esto y corta aca
            }
        }
        RepoEtiquetas.Add(new Etiqueta(id, nombre, descripcion)); // igual la agrega (ojo, permite duplicados como esta)
    }

    public Etiqueta buscarPorId(string id)             // busca una etiqueta por id y la devuelve
    {
        foreach (Etiqueta e in RepoEtiquetas)          // recorremos una x una
        {
            if (e.id == id) return e;                  // la encontramos? listo, la devolvemos al toque
        }
        return null;                                   // no estaba? devolvemos null y arreglate gato
    }

    public List<Etiqueta> ListarEtiquetas()            // te devuelve todas las etiquetas
    {
        return RepoEtiquetas;                          // devolvemos la lista posta (se puede modificar desde afuera eh)
        // si queres ser prolijo: return new List<Etiqueta>(RepoEtiquetas); asi no te rompen la lista interna
    }

    public void EliminarEtiqueta(string id)            // borra una etiqueta por id si existe
    {
        Etiqueta encontrada = null;                    // guardamos aca cuando la ubiquemos
        foreach (Etiqueta e in RepoEtiquetas)          // scaneamos la lista
        {
            if (e.id == id) { encontrada = e; break; } // la vimos? la guardamos y cortamos el for
        }
        if (encontrada != null) RepoEtiquetas.Remove(encontrada);  // si estaba, la sacamos de la lista
        // si no estaba, bueno nada, no hace nada y nadie llora
    }

    public void RenombrarEtiqueta(string id, string nuevoNombre, string nuevaDescripcion = null) // cambia nombre/desc
    {
        Etiqueta e = buscarPorId(id);                  // reciclamos la funcion de buscar, no reinventemos la rueda
        if (e != null)                                 // si existe la etiqueta
        {
            if (nuevoNombre != null) e.nombre = nuevoNombre;          // si te pasaron nombre, lo pisamos
            if (nuevaDescripcion != null) e.descripcion = nuevaDescripcion; // si te pasaron desc, la pisamos
        }
        // si no existe, bueno no hacemos nada, total no hay a quien cambiarle el nombre
    }
}

}                                                      
