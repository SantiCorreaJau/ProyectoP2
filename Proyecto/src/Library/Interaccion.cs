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
        this.fecha = fecha;
        this.tema = tema;
       // this.direccion = direccion;
        this.estado = estado;
        this.clienteId = clienteId;
        this.vendedorId = vendedorId;
        this.comentarios = comentarios;
    }

    public abstract void Registrar(RepositorioInteracciones repositorio);

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
        // Separo la fecha de la interaccion a 3 variables int
        int dia = (fechaInteraccion[0] - '0') * 10 + (fechaInteraccion[1] - '0');
        int mes = (fechaInteraccion[3] - '0') * 10 + (fechaInteraccion[4] - '0');
        int año = (fechaInteraccion[6] - '0') * 10 + (fechaInteraccion[7] - '0');

        // Separo la fecha actual a otras 3 variables
        string hoyStr = DateTime.Now.ToString("dd-MM-yy");
        int diaHoy = (hoyStr[0] - '0') * 10 + (hoyStr[1] - '0');
        int mesHoy = (hoyStr[3] - '0') * 10 + (hoyStr[4] - '0');
        int añoHoy = (hoyStr[6] - '0') * 10 + (hoyStr[7] - '0');

        // Si son del mismo año y mes, se restan los dias
        if (año == añoHoy && mes == mesHoy)
        {
            int diferencia = diaHoy - dia;
            return diferencia <= 10 && diferencia >= 0;
        }

        // Mismo año, mes anterior, calculamos la diferencia tomando que el mes anterior tiene 30 dias
        if (año == añoHoy && mesHoy - mes == 1)
        {
            int diasDelMesAnterior = 30;
            int diferencia = (diaHoy + (diasDelMesAnterior - dia));
            return diferencia <= 10 && diferencia >= 0;
        }

        // Si tienen mas de un mes de distancia o un año distinto, false
        return false;
    }
}