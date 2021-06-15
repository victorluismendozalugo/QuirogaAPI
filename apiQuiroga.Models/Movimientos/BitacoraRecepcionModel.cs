using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiQuiroga.Models.Movimientos
{
    public class BitacoraRecepcionModel
    {
        public int BitacoraID { get; set; }
        public int BitProveedorID { get; set; }
        public int BitPaqueteriaID { get; set; }
        public string BitGUIA { get; set; }
        public int BitCantidadBultos { get; set; }
        public string BitUsuarioAdicion { get; set; }
        public string BitRepartidor { get; set; }
        public string BitObservaciones { get; set; }
        public string BitFechaRegistro { get; set; }
        public string BitOrdenCompra { get; set; }
        public string BitNumeroFactura { get; set; }
        public int BitCantidadTarimas { get; set; }
        public string BitEstatus { get; set; }

        public string BitEstatusDescripcion
        {

            get
            {

                switch (BitEstatus)
                {
                    case "R":
                        return "RECIBIDO";
                    case "L":
                        return "LIBERADO";
                    default:
                        return "";
                }

            }

        }
    }
}