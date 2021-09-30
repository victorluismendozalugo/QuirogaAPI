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
        public char estatus { get; set; }
    }
}
