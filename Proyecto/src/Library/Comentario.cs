namespace Proyecto;

    public class Comentario
    {
        public string id { get; set; }  // ID del comentario en especifico
        public string texto { get; set; }
        public string creadoEn { get; set; }
        public string autorId { get; set; } // Todas las interacciones llevan el ID de quien las crea (vendedor)
        public string interaccionId { get; set; }   // Sirve para saber a qué interacción se asocia el comentario.
        public bool visible { get; set; }

        public Comentario(string id, string texto, string creadoEn, string autorId, string interaccionId, bool visible)
        {
            this.id = id;
            this.texto = texto;
            this.creadoEn = creadoEn;
            this.autorId = autorId;
            this.interaccionId = interaccionId;
            this.visible = visible;
        }

        public void EditarTexto(string nuevoTexto)
        {
            texto = nuevoTexto;
        }

        public void Borrar()
        {
            visible = false;
        }

        public void Mostrar()
        {
            Console.WriteLine($"[{creadoEn}] {texto} (Autor: {autorId})");
        }
    }

