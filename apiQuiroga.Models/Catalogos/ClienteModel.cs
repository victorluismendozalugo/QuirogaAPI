using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Database;

namespace apiQuiroga.Models.Catalogos
{
    public class ClienteModel
    {
        [ConexionColumn("CodCliente")]
        public int ID_Cliente { get; set; }
        [ConexionColumn("Nombre")]
        public string RazonSocial { get; set; }
        public string NombreComercial { get; set; }
        public string Sucursal { get; set; }
        public string Rfc { get; set; }
        public string Estatus { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime FechaBaja { get; set; }
        public int ID_Agente { get; set; }
        public int ID_ListaPrecio { get; set; }
        public int ID_Cedis { get; set; }
    }
}
