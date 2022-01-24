using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiQuiroga.Models.Catalogos
{
    public class UbicacionModel
    {
        public int ID_CatUbicacionesEnc { get; set; }
        public int ID_Empresa { get; set; }
        public int ID_Ubicacion { get; set; }
        public int ID_Almacen { get; set; }
        public string Almacen { get; set; }
        public string ID_Pasillo { get; set; }
        public int ID_Rack { get; set; }
        public int ID_Nivel { get; set; }
        public int ID_Seccion { get; set; }
        public decimal Largo { get; set; }
        public decimal Ancho { get; set; }
        public decimal Alto { get; set; }
        public decimal Capacidad { get; set; }
    }
}
