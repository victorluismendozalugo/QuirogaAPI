using apiQuiroga.Models;
using apiQuiroga.Models.Surtido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;

namespace apiQuiroga.DA
{
    public class DASurtido
    {
        private readonly Conexion _Conexion = null;

        public DASurtido()
        {
            _Conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }

        public Result<List<SurtidoPedido>> PedidosCon(int idCedis, int idAgente, int idCliente)
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pID_Cedis", ConexionDbType.Int, idCedis);
            parametros.Add("@pID_Agente", ConexionDbType.Int, idAgente);
            parametros.Add("@pID_Cliente", ConexionDbType.Int, idCliente);

            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);

            var r = _Conexion.ExecuteWithResults<SurtidoPedido>("QW_P_procSurtidoPedidosCon", parametros);

            return r;
        }
    }
}
