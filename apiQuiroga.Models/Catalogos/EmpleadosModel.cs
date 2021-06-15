namespace apiQuiroga.Models.Catalogos
{
   public class EmpleadosModel
   {
      public int EmpID { get; set; }
      public string EmpNombre { get; set; }
      public string EmpPrimerApellido { get; set; }
      public string EmpSegundoApellido { get; set; }
      public string EmpCorreo { get; set; }
      public string EmpTelefono { get; set; }
      public string EmpEstatus { get; set; }
      public string EmpNombreCompleto
      {
         get
         {
            return EmpNombre + ' ' + EmpPrimerApellido + ' ' + EmpSegundoApellido;
         }
      }
   }
}
