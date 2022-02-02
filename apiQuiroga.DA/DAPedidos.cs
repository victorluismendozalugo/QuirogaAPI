using apiQuiroga.Models;
using apiQuiroga.Models.Catalogos;
using apiQuiroga.Models.Pedidos;
using apiQuiroga.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;
using WarmPack.Extensions;

namespace apiQuiroga.DA
{
    public class DAPedidos
    {
        private readonly Conexion _conexion = null;
        private readonly Conexion _conexion2 = null;

        public DAPedidos()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
            _conexion2 = new Conexion(ConexionType.MSSQLServer, Globales.ConexionSecundaria);
        }

        public Result<DataModel> PedidoCon(int IDEmpresa, int IDPedido)
        {
            var parametros = new ConexionParameters();
            try
            {
                parametros.Add("@pIDEmpresa", ConexionDbType.Int, IDEmpresa);
                parametros.Add("@pIDPedido", ConexionDbType.VarChar, IDPedido);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);
                parametros.Add("@pCodError", ConexionDbType.Int, System.Data.ParameterDirection.Output);

                var r = _conexion2.ExecuteWithResults<PedidoModel>("QW_procPedidoCon", parametros);

                return new Result<DataModel>()
                {
                    Value = parametros.Value("@pResultado").ToBoolean(),
                    Message = parametros.Value("@pMsg").ToString(),
                    Data = new DataModel()
                    {
                        CodigoError = parametros.Value("@pCodError").ToInt32(),
                        MensajeBitacora = parametros.Value("@pMsg").ToString(),
                        Data = r.Data
                    }
                };
            }
            catch (Exception ex)
            {
                return new Result<DataModel>()
                {
                    Value = false,
                    Message = "Problemas al obtener pedido",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                };
            }
        }

        public Result<DataModel> PedidoDetalleCon(int IDEmpresa, int IDPedido)
        {
            var parametros = new ConexionParameters();
            try
            {
                parametros.Add("@pIDEmpresa", ConexionDbType.Int, IDEmpresa);
                parametros.Add("@pIDPedido", ConexionDbType.VarChar, IDPedido);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);
                parametros.Add("@pCodError", ConexionDbType.Int, System.Data.ParameterDirection.Output);

                var r = _conexion2.ExecuteWithResults<PedidoDetalleModel>("QW_procPedidoDetalleCon", parametros);

                return new Result<DataModel>()
                {
                    Value = parametros.Value("@pResultado").ToBoolean(),
                    Message = parametros.Value("@pMsg").ToString(),
                    Data = new DataModel()
                    {
                        CodigoError = parametros.Value("@pCodError").ToInt32(),
                        MensajeBitacora = parametros.Value("@pMsg").ToString(),
                        Data = r.Data
                    }
                };
            }
            catch (Exception ex)
            {
                return new Result<DataModel>()
                {
                    Value = false,
                    Message = "Problemas al obtener detalle pedido",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                };
            }
        }

        //guarda PEDIDO
        public Result<DataModel> PedidoGuardar(PedidoModel pedido)
        {
            var parametros = new ConexionParameters();
            var xml = pedido.ToXml("root");
            try
            {
                parametros.Add("@pDatosXML", ConexionDbType.Xml, xml);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);
                parametros.Add("@pCodError", ConexionDbType.Int, System.Data.ParameterDirection.Output);

                var r = _conexion2.Execute("QW_procPedidoGuardar", parametros);

                return new Result<DataModel>()
                {
                    Value = parametros.Value("@pResultado").ToBoolean(),
                    Message = parametros.Value("@pMsg").ToString(),
                    Data = new DataModel()
                    {
                        CodigoError = parametros.Value("@pCodError").ToInt32(),
                        MensajeBitacora = parametros.Value("@pMsg").ToString(),
                        Data = r.Data
                    }
                };
            }
            catch (Exception ex)
            {
                return new Result<DataModel>()
                {
                    Value = false,
                    Message = "Problemas al guardar pedido",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                };
            }
        }

    }
}
