using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiQuiroga.Models.Ventas
{
    public class CuentasPCobrarModel
    {
        public int idEmpresa { get; set; }
        public int idCuentaPCobrar { get; set; }
        public int idCliente { get; set; }
        public int idFacturaEnc { get; set; }
        public int folioFactura{ get; set; }
        public string clienteRazonSocial { get; set; }
        public string clienteNombre { get; set; }
        public string fechaCuenta { get; set; }
        public string fechaVencimiento { get; set; }
        public string fechaCancelacion { get; set; }
        public string fechaPago { get; set; }
        public string fechaInicio { get; set; }
        public string fechaFin { get; set; }
        public decimal subTotal { get; set; }
        public decimal iVA { get; set; }
        public decimal ieps { get; set; }
        public decimal total { get; set; }
        public decimal importePagado { get; set; }
        public decimal saldo { get; set; }
        public string estatus { get; set; }
    }

    public class CuentasPCobrarDetalleModel
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
