using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiQuiroga.Models.Catalogos
{
    public class ProductosTempModel
    {
        public int productoID { get; set; }
        public int productoEmpresaID { get; set; }
        public string productoCodigoBarras { get; set; }
        public string productoDesc { get; set; }
        public string productoLote { get; set; }
        public string productoFechaCaducidad { get; set; }
        public string productoUsuarioAdicion { get; set; }
        public string productoFechaAdicion { get; set; }
        public string productoEstatus { get; set; }
    }
}
