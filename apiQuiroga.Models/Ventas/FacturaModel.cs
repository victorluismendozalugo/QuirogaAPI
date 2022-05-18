using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiQuiroga.Models.Facturas
{
    public class FacturaModel
    {
        public int IDFacturaEnc { get; set; }
        public int IDCliente { get; set; }
        public string Rfc { get; set; }
        public string RSCliente { get; set; }
        public int IDEmpresa { get; set; }
        public string Serie { get; set; }
        public int Folio { get; set; }
        public DateTime FechaFactura { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime FechaTimbrado { get; set; }
        public decimal SubTotal { get; set; }
        public decimal IVA { get; set; }
        public decimal IEPS { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime FechaPago { get; set; }
        public string Estatus { get; set; }
        public DateTime FechaCancelacion { get; set; }
        public string UUID { get; set; }
        public int IDPedidoEnc { get; set; }
        public string MotivoCancelacion { get; set; }
        public string Observaciones { get; set; }
        public List<FacturaDetalleModel> pedidoDetalle { get; set; }

    }

    public class FacturaDetalleModel
    {
        public int IDEmpresa { get; set; }
        public int IDFacturaEnc { get; set; }        
        public int IDFacturaDet { get; set; }
        public int IDProducto { get; set; }
        
        public double Precio { get; set; }
        public int CantidadPedida { get; set; }
        public int CantidadSurtida { get; set; }
        public decimal ImporteLinea { get; set; }
        public decimal Costo { get; set; }
        public decimal TasaIVA { get; set; }
        public decimal TasaIEPS { get; set; }
        public decimal ImporteIva { get; set; }
        public decimal ImporteIEPS { get; set; }

    }
}
