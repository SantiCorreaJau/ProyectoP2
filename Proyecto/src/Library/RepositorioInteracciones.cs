namespace Proyecto;

public class RepositorioInteracciones
{
    public List<Interaccion> RepoInteracciones { get; set; }

    public RepositorioInteracciones()
    {
        RepoInteracciones = new List<Interaccion>();
    }

    public void Agregar(Interaccion inter) // Metodo para agregar cualquier interaccion al Repositorio de Interacciones
    // todas las interacciones que se crean, automaticamente se agregan al repo de interacciones
    {
        RepoInteracciones.Add(inter);
    }
    
   public List<Interaccion> FiltrarTipo(string tipo) // Filtro por el tipo de interaccion (llamada, mensaje, etc)
    {
        List<Interaccion> resultado = new List<Interaccion>();  // Declaro la lista que voy a devolver

        foreach (Interaccion i in RepoInteracciones)    // Recorro todo el repositorio total
        {
            if (tipo == "llamada" && i is Llamada) resultado.Add(i);    // Pregunto por cada tipo de interaccion
            else if (tipo == "reunion" && i is Reunion) resultado.Add(i);
            else if (tipo == "mensaje" && i is Mensaje) resultado.Add(i);
            else if (tipo == "correo" && i is CorreoElectronico) resultado.Add(i);
        }

        return resultado;   // Devuelvo la lista de todas las interacciones que coinciden con el tipo del filtro
    }

    public List<Interaccion> FiltrarCliente(string clienteId, List<Interaccion> lista)  // Filtro por todas las interacciones que pertenecen a un cliente en especifico por su ID
    {
        List<Interaccion> resultado = new List<Interaccion>();  // Declaro la lista que voy a devolver

        foreach (Interaccion i in lista){    // Recorro la listay si los ids coinciden, los agrego a la lista anterior
            if (i.clienteId == clienteId)
            {
            resultado.Add(i);
            }
        }
        return resultado;
    }

public List<Interaccion> FiltrarVendedor(string vendedorId, List<Interaccion> lista)    // Filtro por vendedor
{
    List<Interaccion> resultado = new List<Interaccion>();  // Mismo sistema a los filtrados anteriores

    foreach (Interaccion i in lista)
    {
        if (i.vendedorId == vendedorId)
        {
            resultado.Add(i);
        }
    }

    return resultado;
}

public List<Interaccion> FiltrarPendiente(List<Interaccion> lista)  // Filtro por el estado de la interaccion
{
    List<Interaccion> resultado = new List<Interaccion>();  // Mismo sistema. Recorro la lista y pregunto por el estado "pendiente"

    foreach (Interaccion i in lista)
    {
        if (i.estado == "pendiente")
        {
            resultado.Add(i);
        }
    }

    return resultado;
}

public List<Interaccion> FiltrarRangoFecha(string desde, string hasta, List<Interaccion> lista) // tomo como parametro dos fechas
                // de formato dd-mm-aa
{
    List<Interaccion> resultado = new List<Interaccion>();

    // Fecha desde
    int dDesde = (desde[0] - '0') * 10 + (desde[1] - '0');
    int mDesde = (desde[3] - '0') * 10 + (desde[4] - '0');
    int aDesde = (desde[6] - '0') * 10 + (desde[7] - '0');

    // Fecha hasta
    int dHasta = (hasta[0] - '0') * 10 + (hasta[1] - '0');
    int mHasta = (hasta[3] - '0') * 10 + (hasta[4] - '0');
    int aHasta = (hasta[6] - '0') * 10 + (hasta[7] - '0');

    // Calculo el mes anterior a la fechaDesde
    int mAnterior = mDesde - 1;
    int aAnterior = aDesde;
    if (mAnterior == 0)
    {
        mAnterior = 12;
        aAnterior = aDesde - 1;
    }

    foreach (Interaccion i in lista)
    {
        string f = i.fecha;
        int d = (f[0] - '0') * 10 + (f[1] - '0');
        int m = (f[3] - '0') * 10 + (f[4] - '0');
        int a = (f[6] - '0') * 10 + (f[7] - '0');

        bool ok = false;

        // Si el año queda en el medio de los rangos, es true
        if (a > aDesde && a < aHasta) ok = true;
        // Si el año es igual a alguno de los limites. . .
        else if (a == aDesde && a == aHasta)
        {
            if (m > mDesde && m < mHasta) ok = true;    // Si esta entre medio de los limites de meses, es true
            else if (m == mDesde && m == mHasta && d >= dDesde && d <= dHasta) ok = true;   // Si coinciden los meses y esta dentro de los dias, es true
            else if (m == mDesde && d >= dDesde) ok = true; // Si coincide el mes desde pero el dia es mayor al dia Desde, es true
            else if (m == mHasta && d <= dHasta) ok = true; // si coincide el mes hasta, pero el dia es menor, es true
        }
        else if (a == aDesde && a == aHasta && m == mDesde && m == mHasta)
        {
            // Mismo año y mismo mes, comparar solo días
            if (d >= dDesde && d <= dHasta) ok = true;
        }
        else if (a == aDesde && a == aHasta && m == mDesde && m < mHasta)
        {
            // Mismo año, rango de meses distintos
            if (m == mDesde && d >= dDesde) ok = true;
            else if (m == mHasta && d <= dHasta) ok = true;
            else if (m > mDesde && m < mHasta) ok = true;
        }

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
    int añoHoy = hoy.Year % 100;

    foreach (Interaccion i in lista)
    {
        string f = i.fecha;
        // Convierto el dia de hoy a strings
        int dia = (f[0] - '0') * 10 + (f[1] - '0');
        int mes = (f[3] - '0') * 10 + (f[4] - '0');
        int año = (f[6] - '0') * 10 + (f[7] - '0');

        bool esReciente = false;
        
        if (año == añoHoy && mes == mesHoy)
        {
            if (diaHoy - dia <= 10 && diaHoy - dia >= 0)    // Si coinciden los meses y los años y hay menos de 10 dias de
                                                            // diferencia, es reciente
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