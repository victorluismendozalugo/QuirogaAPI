namespace apiQuiroga.Models.Catalogos
{
   public class LineasModel
   {
      public int LineaID { get; set; }
      public string LineaDesc { get; set; }
      public decimal LineaComisionMedico { get; set; }
      public decimal LineaComisionVendedor { get; set; }
      public bool LineaManejaDescuento { get; set; }
   }
}
