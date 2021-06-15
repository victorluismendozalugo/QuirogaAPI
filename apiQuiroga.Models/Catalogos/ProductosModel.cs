namespace apiQuiroga.Models.Catalogos
{
    public class ProductosModel
    {
        public int ProductoID { get; set; }
        public string ProductoDesc { get; set; }
        public int ProductoLaboratorioID { get; set; }
        public int ProductoLineaID { get; set; }
        public int ProductoFamiliaID { get; set; }
        public int ProductoFormulaID { get; set; }
        public int ProductoPresentacionID { get; set; }
        public int ProductoUbicacion { get; set; }
        public decimal ProductoIVA { get; set; }
        public decimal ProductoIEPS { get; set; }
        public int ProductoFormaAdministrar { get; set; }
        public int ProductoEmpaque { get; set; }
        public int ProductoContenido { get; set; }
        public decimal ProductoPrecioPublico { get; set; }
        public string ProductoEstatus { get; set; }
        public bool ProductoTieneCaducidad { get; set; }
        public string ProductoFormulaDesc { get; set; }
        public bool ProductoExcepcionPedidos { get; set; }
        public bool ProductoEtiqueta { get; set; }
        public bool ProductoVanidoso { get; set; }
        public int ProductoUnidadVenta { get; set; }
        public int ProductoGotasxML { get; set; }
        public int ProductoMedida1ID { get; set; }
        public int ProductoMedida2ID { get; set; }
        public string ProductoCodSAT { get; set; }
        public string ProductoCveUnidadSAT { get; set; }
        public decimal ProductoCostoPromedio { get; set; }
        public decimal ProductoUltimoCosto { get; set; }
        public string ProductoCodigoBarras { get; set; }
        public string ProductoTipo { get; set; }
        public decimal ProductoPrecioFarmacia { get; set; }
        public string ProductoGrupoSSA { get; set; }
        public string ProductoTemporalidad { get; set; }
        public int ProductoGrupoNumero { get; set; }
    }
}
