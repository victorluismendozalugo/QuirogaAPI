using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiQuiroga.Models.Movimientos
{
    public class FacturasRecibidasModel
    {
        public int IDMovimiento { get; set; }
        public int IDEmpresa { get; set; }
        public int IDRecibo { get; set; }
        public int IDOrden { get; set; }
        public string IDFactura { get; set; }
        public string FechaFactura { get; set; }
        public string FechaMovimiento { get; set; }
        public string Estatus { get; set; }
        public int UsuarioRegistro { get; set; }
        public List<FacturasRecibidasDetalleModel> facturasRecibidasDetalle { get; set; }
    }

    public class FacturasRecibidasDetalleModel
    {
        public int IDMovimiento { get; set; }
        public int IDEmpresa { get; set; }
        public int IDProducto { get; set; }
        public int CantSolicitada { get; set; }
        public int CantRecibida { get; set; }
        public int CantDevuelta { get; set; }
        public int CantMerma { get; set; }
        public int CantSCosto { get; set; }
        public decimal CostoPactado { get; set; }
        public decimal CostoUnitario { get; set; }
        public int IVA { get; set; }
        public decimal SubtotalIva { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalLinea { get; set; }
        public string Lote { get; set; }
        public string FechaCaducidad { get; set; }
        public string Estatus { get; set; }

    }
}
