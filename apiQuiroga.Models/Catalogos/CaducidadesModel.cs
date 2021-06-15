using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiQuiroga.Models.Catalogos
{
    public class CaducidadesModel
    {
        public int CadClaveEmpresa { get; set; }
        public int CadClaveArticulo { get; set; }
        public string CadLote { get; set; }
        public string CadFecha { get; set; }
        public int CadPiezas { get; set; }
        public int CadApartados { get; set; }
    }
}
