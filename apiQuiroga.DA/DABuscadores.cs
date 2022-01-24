using apiQuiroga.Models;
using apiQuiroga.Models.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;

namespace apiQuiroga.DA
{
    public class DABuscadores
    {
        private readonly Conexion _Conexion = null;

        public DABuscadores()
        {
            _Conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }

        public Result<List<BuscadorProductoModel>> Productos(string buscar)
        {

            var parametros = new ConexionParameters();
            parametros.Add("@pBuscar", ConexionDbType.VarChar, buscar);
            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);

            var r = _Conexion.ExecuteWithResults<BuscadorProductoModel>("QW_procCatArticulosCon", parametros);

            return r;
        }
    }
}
