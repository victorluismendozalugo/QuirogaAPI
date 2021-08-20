using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiQuiroga.Models.Catalogos
{
    public class EmpresasModel
    {
        public int clave_empresa { get; set; }
        public string Razon_Social { get; set; }
        public string Nombre_Comercial { get; set; }
        public string NombreSucursal { get; set; }
        public string Curp { get; set; }
        public string RFC { get; set; }
        public string Calle { get; set; }
        public string EntreCalles { get; set; }
        public int Localidad { get; set; }
        public int Ciudad { get; set; }
        public int Estado { get; set; }
        public int CodigoPostal { get; set; }
        public string Telefono { get; set; }
        public string Fax{ get; set; }
        public string RepresentanteLegal { get; set; }
        public string CorreoElectronico { get; set; }
        public string CorreoElectronicoFacturacion { get; set; }

    }
}
