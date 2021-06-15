using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiQuiroga.Models.Catalogos
{
    public class OperacionesModel
    {
        public int OperacionID { get; set; }
        public int OperacionEmpresaID { get; set; }
        public string OperacionDesc { get; set; }
        public int OperacionMenu { get; set; }
        public string OperacionURL { get; set; }
        public int OperacionPerfil { get; set; }
        public string OperacionFecha { get; set; }
        public string OperacionUsuario { get; set; }
        public string OperacionEstatus { get; set; }

        //movimientos
        public int OperacionMovimientoID { get; set; }
        public string OpUsuarioSolicito { get; set; }
        public string OpUsuarioAutorizo { get; set; }
        public string OpFechaMovimiento { get; set; }
        public string OpMovObservaciones { get; set; }
    }
}
