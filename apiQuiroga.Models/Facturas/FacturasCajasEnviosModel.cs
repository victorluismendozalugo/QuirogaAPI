using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiQuiroga.Models.Facturas
{
    public class FacturasCajasEnviosModel
    {
        public int IdMovimiento { get; set; }
        public int IDOrigen { get; set; }
        public int IDDestino { get; set; }
        public string Factura { get; set; }
        public int IDCliente { get; set; }
        public string FechaFactura { get; set; }
        public string Serie { get; set; }
        public string Guia { get; set; }
        public string UsuarioRegistro { get; set; }
        public int IDCaja1 { get; set; }
        public int IDCaja2 { get; set; }
        public string RazonSocial { get; set; }
        public string FechaSalida { get; set; }
        public string FechaRecepcion { get; set; }
        public string Estatus { get; set; }

        public List<FacturasCajasDetalleModel> FacturasDet { get; set; }

    }

    public class FacturasCajasDetalleModel
    {
        public string Factura { get; set; }
    }
}
