namespace Proyecto;

public class Cliente
{
   public string id { get; set; }
   public string nombre { get; set; }
   public string apellido { get; set; }
   public string telefono { get; set; }
   public string email { get; set; }
   public string genero { get; set; }
   public string fechaNacimiento { get; set; }
   public bool activo { get; set; }
   public string creadoEn { get; set; }
   
   public List<string> etiquetasIds { get; set; }


   public Cliente(string unId, string unNombre, string unApellido, string unTelefono, string unEmail, string gen,
      string fechaNac, string creado)
   {
      this.id = unId;
      this.nombre = unNombre;
      this.apellido = unApellido;
      this.telefono = unTelefono;
      this.email = unEmail;
      this.genero = gen;
      this.fechaNacimiento = fechaNac;
      this.activo = true;
      this.creadoEn = creado;
      etiquetasIds = new List<string>();

   }
   
   public void Actualizar(
      string nombre = null,
      string apellido = null,
      string telefono = null,
      string email = null,
      string genero = null,
      string fechaNacimiento = null
   )
   {
      if(nombre != null) this.nombre = nombre;
      if(apellido != null) this.apellido = apellido;
      if(telefono != null) this.telefono = telefono;
      if(email != null) this.email = email;
      if(genero != null) this.genero = genero;
      if(fechaNacimiento != null) this.fechaNacimiento = fechaNacimiento;
   }

}
