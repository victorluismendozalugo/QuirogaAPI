using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiQuiroga.Models.Configuracion
{
    public class PerfilesUsuarioModel
    {
        public int CodigoPerfil { get; set; }
        public string DescripcionPerfil { get; set; }
        public string Estatus { get; set; }
        public string AdicionadoPor { get; set; }
        public string FechaAdicion { get; set; }
        public string ModificadoPor { get; set; }
        public string FechaModificacion { get; set; }

        //opciones X perfil
        public string DescripcionMenu { get; set; }
        public string Tipo { get; set; }
        public int CodigoPadre { get; set; }
        public int Orden { get; set; }
        public int CodigoMenuOpcion { get; set; }

    }
}
