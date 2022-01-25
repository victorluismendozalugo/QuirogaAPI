using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiQuiroga.Models.Catalogos
{
    public class UbicacionDetModel
    {
        public int ID_CatUbicacionesEnc { get; set; }
        public int ID_CatUbicacionesDet { get; set; }
        public int ID_Articulo { get; set; }
        public string descripcion { get; set; }
        public string CodBarras { get; set; }
        public int Cantidad { get; set; }
        public string Lote { get; set; }
        public DateTime FechaCaducidad { get; set; }
    }
}
