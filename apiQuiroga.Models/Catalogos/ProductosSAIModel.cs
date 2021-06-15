using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiQuiroga.Models.Catalogos
{
    public class ProductosSAIModel
    {
        public int cse_prod { get; set; }
        public int cve_prod { get; set; }
        public string lugar { get; set; }
        public decimal existencia { get; set; }
    }
}
