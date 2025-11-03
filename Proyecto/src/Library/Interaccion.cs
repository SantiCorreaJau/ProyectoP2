namespace Proyecto;

public abstract class Interaccion       // Creamos una clase abstracta porque nunca vamos a crear una interacción 'pura'
                                        // siempre va a ser un correo, una cotización, etc.
{
    public string id { get; set; }
    public string fecha { get; set; }
    public string tema { get; set; }
    public string estado { get; set; }
    public string clienteId { get; set; }
    public string vendedorId { get; set; }
    public List<Comentario> comentarios { get; set; }

    public Interaccion(string id, string fechaHora, string tema, string estado, string clienteId, string vendedorId, List<Comentario> comentarios)
    {
        this.id = id;
        this.fecha = fechaHora;
        this.tema = tema;
        this.estado = estado;
        this.clienteId = clienteId;
        this.vendedorId = vendedorId;
        this.comentarios = comentarios;
    }


    public void Registrar(RepositorioInteracciones repositorio)
    {
        repositorio.Agregar(this);

    }

    public void CambiarEstado(string nuevoEstado)
    {
        estado = nuevoEstado;
    }

    public bool EsPendiente()
    {
        if (this.estado == "pendiente")
        {
            return true;
        }
        else return false;

    }

    public bool EsReciente(string fechaInteraccion)
    {
        // Fecha de la interacción (dd-mm-yy) la paso a enteros
        int dia = (fechaInteraccion[0] - '0') * 10 + (fechaInteraccion[1] - '0');
        int mes = (fechaInteraccion[3] - '0') * 10 + (fechaInteraccion[4] - '0');
        int año = (fechaInteraccion[6] - '0') * 10 + (fechaInteraccion[7] - '0');

        // Fecha actual (dd-mm-yy) la paso a enteros
        string hoyStr = DateTime.Now.ToString("dd-MM-yy");
        int diaHoy = (hoyStr[0] - '0') * 10 + (hoyStr[1] - '0');
        int mesHoy = (hoyStr[3] - '0') * 10 + (hoyStr[4] - '0');
        int añoHoy = (hoyStr[6] - '0') * 10 + (hoyStr[7] - '0');

        // Mismo año y mes, resta directa de días
        if (año == añoHoy && mes == mesHoy)
        {
            int diferencia = diaHoy - dia;
            return diferencia <= 10 && diferencia >= 0;
        }

        // Mismo año, mes anterior calculo la diferencia tomando que el mes anterior tiene 30 dias
        if (año == añoHoy && mesHoy - mes == 1)
        {
            int diasDelMesAnterior = 30;
            int diferencia = (diaHoy + (diasDelMesAnterior - dia));
            return diferencia <= 10 && diferencia >= 0;
        }

        // Distinto año o más de un mes, no es reciente
        return false;
    }

}