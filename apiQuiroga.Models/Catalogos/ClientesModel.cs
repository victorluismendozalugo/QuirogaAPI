using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiQuiroga.Models.Catalogos
{
    public class ClientesModel
    {
        public int ClienteID { get; set; }
        public string ClienteRazonSocial { get; set; }
        public string ClienteNombre { get; set; }
        public string ClienteApellidoP { get; set; }
        public string ClienteApellidoM { get; set; }
        public string ClienteCurp { get; set; }
        public string ClienteRfc { get; set; }
        public string ClienteTelefono { get; set; }
        public string ClienteFechaRegistro { get; set; }
        public string ClienteFechaModificacion { get; set; }
        public int ClienteGrupoClientes { get; set; }
        public string ClienteEmail { get; set; }
        public string ClienteCtaContable { get; set; }
        public string ClienteAdicionadoPor { get; set; }
        public string ClienteModificadoPor { get; set; }
        public string ClienteTipoPersona { get; set; }
        public string ClienteResponsableFiscal { get; set; }
        public string ClienteEmailResponsable { get; set; }
    }
}
