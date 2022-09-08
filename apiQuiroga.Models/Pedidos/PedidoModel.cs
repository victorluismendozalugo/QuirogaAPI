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
        public string cve_Sucursal { get; set; }
        public int idCliente { get; set; }
        public int idEmpresa { get; set; }
        public int idAgente { get; set; }
        public DateTime fechaPedido { get; set; }
        public string estatus { get; set; }
        public decimal totalPedido { get; set; }
        public decimal subTotalPedido { get; set; }
        public decimal iva { get; set; }
        public string observaciones { get; set; }
        public string adicionadoPor { get; set; }
        public string cveAlmacen { get; set; }

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

    public class BuscadorProductoPedidoModel
    {
        public int codArticulo { get; set;  }
        public string descripcion { get; set; }
        public decimal precio { get; set; }
        public string formula { get; set; }
        public string laboratorio { get; set; }
        public int existencia { get; set; }
        public int existenciaOtrosCedis { get; set; }
        public string codigoSat { get; set; }
        public string codigoBarras { get; set; }
        public decimal precioPublico { get; set; }
        public decimal iva { get; set; }
        public decimal ieps { get; set; }
        public int cantidad { get; set; }
        public Boolean ofertado { get; set; }
        public Boolean conOferta { get; set; }

    }
}
