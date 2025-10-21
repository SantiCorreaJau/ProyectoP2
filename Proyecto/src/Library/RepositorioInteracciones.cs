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
    
   public List<Interaccion> FiltrarTipo(string tipo)
    {
        List<Interaccion> resultado = new List<Interaccion>();

        foreach (Interaccion i in RepoInteracciones)
        {
            if (tipo == "llamada" && i is Llamada) resultado.Add(i);
            else if (tipo == "reunion" && i is Reunion) resultado.Add(i);
            else if (tipo == "mensaje" && i is Mensaje) resultado.Add(i);
            else if (tipo == "correo" && i is CorreoElectronico) resultado.Add(i);
        }

        return resultado;
    }

    public List<Interaccion> FiltrarCliente(string clienteId, List<Interaccion> lista)
{
    List<Interaccion> resultado = new List<Interaccion>();

    foreach (Interaccion i in lista)
    {
        if (i.clienteId == clienteId)
        {
            resultado.Add(i);
        }
    }

    return resultado;
}

public List<Interaccion> FiltrarVendedor(string vendedorId, List<Interaccion> lista)
{
    List<Interaccion> resultado = new List<Interaccion>();

    foreach (Interaccion i in lista)
    {
        if (i.vendedorId == vendedorId)
        {
            resultado.Add(i);
        }
    }

    return resultado;
}

public List<Interaccion> FiltrarPendiente(List<Interaccion> lista)
{
    List<Interaccion> resultado = new List<Interaccion>();

    foreach (Interaccion i in lista)
    {
        if (i.estado == "pendiente")
        {
            resultado.Add(i);
        }
    }

    return resultado;
}

public List<Interaccion> FiltrarRangoFecha(string desde, string hasta, List<Interaccion> lista)
{
    List<Interaccion> resultado = new List<Interaccion>();

    int dDesde = (desde[0] - '0') * 10 + (desde[1] - '0');
    int mDesde = (desde[3] - '0') * 10 + (desde[4] - '0');
    int aDesde = (desde[6] - '0') * 10 + (desde[7] - '0');

    int dHasta = (hasta[0] - '0') * 10 + (hasta[1] - '0');
    int mHasta = (hasta[3] - '0') * 10 + (hasta[4] - '0');
    int aHasta = (hasta[6] - '0') * 10 + (hasta[7] - '0');

    foreach (Interaccion i in lista)
    {
        string f = i.fecha;

        int d = (f[0] - '0') * 10 + (f[1] - '0');
        int m = (f[3] - '0') * 10 + (f[4] - '0');
        int a = (f[6] - '0') * 10 + (f[7] - '0');

        bool ok = false;

        if (a > aDesde && a < aHasta) ok = true;
        else if (a == aDesde && a == aHasta)
        {
            if (m > mDesde && m < mHasta) ok = true;
            else if (m == mDesde && m == mHasta && d >= dDesde && d <= dHasta) ok = true;
            else if (m == mDesde && d >= dDesde) ok = true;
            else if (m == mHasta && d <= dHasta) ok = true;
        }
        else if (a == aDesde && (m > mDesde || (m == mDesde && d >= dDesde))) ok = true;
        else if (a == aHasta && (m < mHasta || (m == mHasta && d <= dHasta))) ok = true;

        if (ok)
        {
            resultado.Add(i);
        }
    }

    return resultado;
}

public List<Interaccion> FiltrarRecientes(List<Interaccion> lista)
{
    List<Interaccion> resultado = new List<Interaccion>();

    DateTime hoy = DateTime.Now;
    int diaHoy = hoy.Day;
    int mesHoy = hoy.Month;
    int a単oHoy = hoy.Year % 100;

    foreach (Interaccion i in lista)
    {
        string f = i.fecha;

        int dia = (f[0] - '0') * 10 + (f[1] - '0');
        int mes = (f[3] - '0') * 10 + (f[4] - '0');
        int a単o = (f[6] - '0') * 10 + (f[7] - '0');

        bool esReciente = false;

        if (a単o == a単oHoy && mes == mesHoy)
        {
            if (diaHoy - dia <= 10 && diaHoy - dia >= 0)
            {
                esReciente = true;
            }
        }

        if (esReciente)
        {
            resultado.Add(i);
        }
    }

    return resultado;
}

}