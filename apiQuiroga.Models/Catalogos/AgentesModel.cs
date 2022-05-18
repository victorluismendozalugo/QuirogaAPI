using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiQuiroga.Models.Catalogos
{
    public class AgentesModel
    {
        public int CodigoAgente { get; set; }
        public int CodigoUsuario { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Plaza { get; set; }
        public int GrupoClientes { get; set; }
        public string Estatus { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime FechaBaja { get; set; }
        public decimal ImportePresupuesto { get; set; }
        public int PiezasPresupuesto { get; set; }
        public int Empresa { get; set; }
        public int AlmacenVirtual { get; set; }

    }
    public class AgentesDFQModel
    {
        public int IDAgente { get; set; }
        public string Nombre { get; set; }
        public string Estatus { get; set; }
        public int IDCedis { get; set; }
        public int SAIAgente { get; set; }
    }
}
