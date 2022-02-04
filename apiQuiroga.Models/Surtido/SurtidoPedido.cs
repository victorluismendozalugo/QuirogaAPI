using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiQuiroga.Models.Surtido
{
    public class SurtidoPedido
    {
        public int Folio { get; set; }
        public int ID_Cliente { get; set; }
        public string Cliente { get; set; }
        public int ID_Agente { get; set; }
        public string Agente { get; set; }
        public DateTime FechaPedido { get; set; }
        public int ID_Cedis { get; set; }
    }
}
