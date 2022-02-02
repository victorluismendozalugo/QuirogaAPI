using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiQuiroga.Models.Pedidos
{
    public class PedidoModel
    {
        public int idPedidoEnc { get; set; }
        public int idCliente { get; set; }
        public int idEmpresa { get; set; }
        public DateTime fechaPedido { get; set; }
        public string estatus { get; set; }
        public decimal totalPedido { get; set; }
        public decimal subTotalPedido { get; set; }
        public decimal iva { get; set; }
        public string observaciones{ get; set; }   
        public string adicionadoPor { get; set; }
        public List<PedidoDetalleModel> pedidoDetalle { get; set; }

    }

    public class PedidoDetalleModel
    {
        public int idPedidoEnc { get; set; }
        public int idCliente { get; set; }
        public int idEmpresa { get; set; }
        public int idProducto { get; set; }
        public string descripcion { get; set; }
        public int cantidadPedida { get; set; }
        public int cantidadSurtida { get; set; }
        public decimal costo { get; set; }
        public decimal precio { get; set; }
        public decimal totalLinea { get; set; }
        public decimal tasaIva { get; set; }
        public decimal iva { get; set; }

    }
}
