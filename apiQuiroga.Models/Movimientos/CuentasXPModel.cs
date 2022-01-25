using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiQuiroga.Models.Movimientos
{
    public class CuentasXPModel
    {
        public int ?IDMovimiento { get; set; }
        public int IDEmpresa { get; set; }
        public string IDFactura { get; set; }
        public int IDProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public decimal DescuentoPPago { get; set; }
        public int Plazo { get; set; }
        public string FechaFactura { get; set; }
        public string FechaAplicacion { get; set; }
        public string FechaPago { get; set; }
        public string FechaUltimoAbono { get; set; }
        public decimal SubTotalFactura { get; set; }
        public decimal IVA { get; set; }
        public decimal TotalFactura { get; set; }
        public decimal saldoFactura { get; set; }
        public string Estautus { get; set; }
        public decimal DsctoEspecial { get; set; }
        public decimal DsctoDevoluciones { get; set; }
        public decimal DsctoFaltantes { get; set; }
        public int UsuarioRegistro { get; set; }
        public string FechaRegistro { get; set; }

        public string Cheque { get; set; }
        public string Concepto { get; set; }
        public decimal Importe { get; set; }
        public decimal ImporteDescuento { get; set; }

    }
}
