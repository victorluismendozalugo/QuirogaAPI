using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiQuiroga.Models.Usuario
{
    public class UsuarioMenuModel
    {
        public int id { get; set; }
        public int parentid { get; set; }
        public string name { get; set; }
        public string icon { get; set; }
        public string url { get; set; }
        public string CodigoUsuario { get; set; }

    }
}
