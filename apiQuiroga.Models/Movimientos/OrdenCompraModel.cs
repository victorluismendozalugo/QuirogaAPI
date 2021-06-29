using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiQuiroga.Models.Movimientos
{
    public class OrdenCompraModel
    {
        #region ordenCompraCabecero
        public int claveEmpresa { get; set; }
        public int claveOrden { get; set; }
        public string claveFactura { get; set; }
        public int claveProvedor { get; set; }
        public int plazo { get; set; }
        public decimal descuentoProntoPago { get; set; }
        public string fechaOrden { get; set; }
        public string fechaRecepcion { get; set; }
        public string fechaCancelacion { get; set; }
        public string fechaFactura { get; set; }
        public decimal importeTotal { get; set; }
        public string estatusOrden { get; set; }
        public int usuarioRegistro { get; set; }
        public string fechaRegistro { get; set; }

        #endregion ordenCompraCabecero
        #region ordenCompraDetalle
        public int claveProducto { get; set; }
        public decimal costoPactado { get; set; }
        public decimal costoUnitario { get; set; }
        public decimal descuento1 { get; set; }
        public decimal descuento2 { get; set; }
        public decimal descuento3 { get; set; }
        public decimal descuento4 { get; set; }
        public int cantidadSolicitada { get; set; }
        public int cantiadadDevolucion { get; set; }
        public int cantidadScargo { get; set; }
        public string lote { get; set; }
        public string fechaCaducidad { get; set; }
        public decimal iva { get; set; }
        public decimal subTotalIVA { get; set; }
        public decimal subTotal { get; set; }
        public decimal totalLinea { get; set; }

        #endregion ordenCompraDetalle

        public string provRazonSocial { get; set; }
    }   
}
