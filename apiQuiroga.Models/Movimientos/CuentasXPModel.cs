using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiQuiroga.Models.Movimientos
{
    public class CuentasXPModel
    {
        public int EmpresaID { get; set; }
        public int ProveedorID { get; set; }
        public string NumeroFactura { get; set; }
        public decimal DescuentoPpago { get; set; }
        public int Plazo { get; set; }
        public string FechaFactura { get; set; }
        public string FechaEntrada { get; set; }
        public string FechaPago { get; set; }
        public string FechaUltimoAbono { get; set; }
        public decimal SubtotalFactura { get; set; }
        public decimal IVA { get; set; }
        public decimal TotalFactura { get; set; }
        public decimal SaldoFactura { get; set; }
        public string Estatus { get; set; }
        public decimal DsctoEspecial { get; set; }
        public decimal DsctoDevoluciones { get; set; }
        public decimal DsctoFaltantes { get; set; }
        public string UsuarioAdicion { get; set; }
        public string FechaAdicion { get; set; }
        public string razon_social { get; set; }

    }
}
