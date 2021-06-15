namespace apiQuiroga.Models.Catalogos
{
   public class FormulasModel
   {
      public int FormulaID { get; set; }
      public string FormulaDesc { get; set; }
      public bool FormulaDesglozar { get; set; }
      public bool FormulaCombinada { get; set; }
      public string FormulaAccionTerapeutica { get; set; }
      public string FormulaProdLider { get; set; }
      public short FormulaGenericoID { get; set; }
   }
}
