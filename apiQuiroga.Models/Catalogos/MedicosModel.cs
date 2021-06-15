namespace apiQuiroga.Models.Catalogos
{
   public class MedicosModel
   {
      public int MedID { get; set; }
      public string MedNombre { get; set; }
      public string MedPrimerApellido { get; set; }
      public string MedSegundoApellido { get; set; }
      public string MedCorreo { get; set; }
      public string MedTelefono { get; set; }
      public string MedEstatus { get; set; }
      public string MedNombreCompleto
      {
         get
         {
            return MedNombre + ' ' + MedPrimerApellido + ' ' + MedSegundoApellido;
         }
      }

      public int MedTitulo { get; set; }
      public string MedEspecialidad { get; set; }
      public string MedEscuela { get; set; }
      public string MedCedula { get; set; }
      public string MedSSA { get; set; }
   }
}
