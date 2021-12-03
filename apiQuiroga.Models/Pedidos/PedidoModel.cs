using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiQuiroga.Models.Pedidos
{
    public class PedidoModel
    {
        public int IDPedidoEnc { get; set; }
        public int IDCliente { get; set; }
        public int IDEmpresa { get; set; }
        public DateTime FechaPedido { get; set; }
        public string Estatus { get; set; }
        public decimal TotalPedido { get; set; }
        public decimal SubTotalPedido { get; set; }
        public decimal IVA { get; set; }
        public string Observaciones{ get; set; }    
        public List<PedidoDetalleModel> pedidoDetalle { get; set; }

    }

    public class PedidoDetalleModel
    {
        public int IDPedidoEnc { get; set; }
        public int IDCliente { get; set; }
        public int IDEmpresa { get; set; }
        public int IDProducto { get; set; }
        public string Descripcion { get; set; }
        public int CantidadPedida { get; set; }
        public int CantidadSurtida { get; set; }
        public decimal Costo { get; set; }
        public decimal Precio { get; set; }
        public decimal TotalLinea { get; set; }
        public decimal TasaIVA { get; set; }
        public decimal IVA { get; set; }
        public DateTime FechaPedido { get; set;  }

    }
}
