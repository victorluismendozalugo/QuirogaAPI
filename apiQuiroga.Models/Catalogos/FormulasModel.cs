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
        public int FormulaGenericoID { get; set; }
        public bool FormulaAntibiotico { get; set; }
        public int FormulaCodTipoControl { get; set; }
    }
}
