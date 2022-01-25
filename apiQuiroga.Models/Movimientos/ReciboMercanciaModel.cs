using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiQuiroga.Models.Movimientos
{
    public class ReciboMercanciaModel
    {
        public int IDRecibo { get; set; }
        public int IDEmpresa { get; set; }
        public int IDOrden { get; set; }
        public string IDFactura { get; set; }
        public string FechaRecibo { get; set; }
        public string Estatus { get; set; }
        public int UsuarioRegistro { get; set; }
        public List<ReciboDetalleModel> reciboDetalle { get; set; }

    }

    public class ReciboDetalleModel
    {
        public int IDRecibo { get; set; }
        public int IDEmpresa { get; set; }
        public int IDProducto { get; set; }
        public int cantSolicitada { get; set; }
        public int cantRecibida { get; set; }
        public int cantDevuelta { get; set; }
        public int cantMerma { get; set; }
        public int cantSCosto { get; set; }
        public string lote { get; set; }
        public string fechaCaducidad { get; set; }
        public string fechaRecepcion { get; set; }
        public string estatus { get; set; }
        public string nombreProducto { get; set; }
        public string codigoBarras { get; set; }
        public decimal costoPactado { get; set; }
        public decimal costoUnitario { get; set; }
        public int iva { get; set; }
        public decimal subtotalIVA { get; set; }
        public decimal subTotal { get; set; }
        public decimal totalLinea { get; set; }


        public static implicit operator List<object>(ReciboDetalleModel v)
        {
            throw new NotImplementedException();
        }
    }
}
