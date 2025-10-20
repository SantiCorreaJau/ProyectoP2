namespace Proyecto;

public class Comentario
{
    public class Comentario
    {
        public string id { get; set; }
        public string texto { get; set; }
        public string creadoEn { get; set; }
        public string autorId { get; set; }
        public string interaccionId { get; set; }
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
            System.Console.WriteLine($"[{creadoEn}] {texto} (Autor: {autorId})");
        }
    }

}