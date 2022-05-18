using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiQuiroga.Models.CajasPedido
{
    public class CajasPedidoModel
    {
        public int idCaja { get; set; }
        public int numeroCaja { get; set; }
        public string descripcionCaja { get; set; }
        public string estatusCaja { get; set; }
        public string descripcionEstatus { get; set; }
        public int idUbicacion { get; set; }
        public string descripcionUbicacion { get; set; }
        public string usuario { get; set; }
        public string motivoCambio { get; set; }
        public List<CajasPedidoMovimientosModel> cajasRecibir { get; set; }

    }

    public class CajasPedidoMovimientosModel
    {
        public int idMovimiento { get; set; }
        public int idCaja { get; set; }
        public int numeroCaja { get; set; }
        public string descripcionCaja { get; set; }
        public int idUbicacion { get; set; }
        public string descripcionUbicacion { get; set; }
        public int idUbicacionDestino { get; set; }
        public string descripcionUbicacionDestino { get; set; }
        public string guia { get; set; }
        public string estatus { get; set; }
        public string descripcionEstatus { get; set; }
        public string fechaSalida { get; set; }
        public string fechaRecepcion { get; set; }
        
    }

    public class CajasPedidoDetalleCambios
    {
        public int IdCaja { get; set; }
        public int IdCambio { get; set; }
        public string MotivoCambio { get; set; }
        public string UsuarioCambio { get; set; }
        public string FechaCambio { get; set; }
    }
}
