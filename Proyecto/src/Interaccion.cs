namespace DefaultNamespace;

public interface Interaccion
{
    public class Interaccion
    {
        public string id { get; set; }
        public string fechaHora { get; set; }
        public string tema { get; set; }
        public string direccion { get; set; }
        public string estado { get; set; }
        public string clienteId { get; set; }
        public string vendedorId { get; set; }
        public List<Comentario> comentarios { get; set; }

        public Interaccion(string id, string fechaHora, string tema, string direccion, string estado, string clienteId, string vendedorId, List<Comentario> comentarios)
        {
            this.id = id;
            this.fechaHora = fechaHora;
            this.tema = tema;
            this.direccion = direccion;
            this.estado = estado;
            this.clienteId = clienteId;
            this.vendedorId = vendedorId;
            this.comentarios = comentarios;
        }

        public void Registrar(string idCliente)
        {
            clienteId = idCliente;
        }

        public void CambiarEstado(string nuevoEstado)
        {
            estado = nuevoEstado;
        }

        public void AgregarComentario(string texto)
        {
            if (comentarios == null)
            {
                comentarios = new List<Comentario>();
            }
            string fechaActual = System.DateTime.Now.ToString();
            comentarios.Add(new Comentario("", texto, fechaActual, vendedorId, id, true));
        }

        public bool EsPendiente()
        {
            return estado == "pendiente";
        }

        public bool EsReciente(string fechaLimite)
        {
            return string.Compare(fechaHora, fechaLimite) > 0;
        }
    }
}