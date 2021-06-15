using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiQuiroga.Models.Movimientos
{
    public class ComprasModel
    {
        public int claveEmpresa { get; set; }
        public int clavePedido { get; set; }
        public int claveProveedor { get; set; }
        public string nombreProveedor { get; set; }
        public int plazo { get; set; }
        public decimal descuentoPpago { get; set; }
        public string fechaPedido { get; set; }
        public string fechaRecibo { get; set; }
        public string fechaEnvio { get; set; }
        public string fechaEmbarque { get; set; }
        public string fechaFactura { get; set; }
        public string fechaCancelacion { get; set; }
        public decimal importeTotal { get; set; }
        public string estatus { get; set; }
        public string estatusDetalle
        {
            get
            {
                switch (estatus)
                {
                    case "A":
                        return "Capturado";
                    case "R":
                        return "Recibido";
                    case "T":
                        return "Transito";
                    case "P":
                        return "Parcial";
                    case "C":
                        return "Cancelado";
                    case "L":
                        return "Aplicado";
                    default:
                        return "";
                }
            }
        }
        public string adicionadoPor { get; set; }
        public string fechaAdicion { get; set; }


        public int claveProducto { get; set; }
        public decimal ultimoCosto { get; set; }
        public int cantidadSolicitada { get; set; }
        public int cantidadSurtida { get; set; }
        public decimal IVA { get; set; }
        public decimal subTotal { get; set; }
        public decimal subTotalIVA { get; set; }
        public decimal totalLinea { get; set; }
        public string lote { get; set; }
        public string fechaCaducidad { get; set; }
        public string nombreProducto { get; set; }
        public int cantidadMerma { get; set; }
        public decimal costoUnitario { get; set; }
        public decimal descuentoCompra { get; set; }
        public decimal descuentoOferta { get; set; }
        public decimal descuentoEspecial { get; set; }
        public decimal precioNeto { get; set; }
        public decimal costoPactado { get; set; }
        public int cantidadSCargo { get; set; }
        public string numeroFactura { get; set; }
        public string codigoBarras { get; set; }

    }
}
