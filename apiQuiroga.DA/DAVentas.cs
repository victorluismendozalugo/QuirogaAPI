using apiQuiroga.Models;
using apiQuiroga.Models.Catalogos;
using apiQuiroga.Models.Ventas;
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
    public class DAVentas
    {
        private readonly Conexion _conexion = null;
        private readonly Conexion _conexion2 = null;

        public DAVentas()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
            _conexion2 = new Conexion(ConexionType.MSSQLServer, Globales.ConexionSecundaria);
        }

        public Result<DataModel> CuentasPCobrarCon(int IDEmpresa, int Factura, int IDCliente, string FechaInicio, string FechaFin, string Estatus)
        {
            var parametros = new ConexionParameters();
            try
            {
                parametros.Add("@pIDEmpresa", ConexionDbType.Int, IDEmpresa);
                parametros.Add("@pFactura", ConexionDbType.Int, Factura);
                parametros.Add("@pIDCliente", ConexionDbType.Int, IDCliente);
                parametros.Add("@pFechaInicio", ConexionDbType.VarChar, FechaInicio);
                parametros.Add("@pFechaFin", ConexionDbType.VarChar, FechaFin);
                parametros.Add("@pEstatus", ConexionDbType.VarChar, Estatus);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);
                parametros.Add("@pCodError", ConexionDbType.Int, System.Data.ParameterDirection.Output);

                var r = _conexion2.ExecuteWithResults<CuentasPCobrarModel    >("QW_procCuentasPCobrarCon", parametros);

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
                    Message = "Problemas al obtener cuentas por cobrar",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                };
            }
        }

        //public Result<DataModel> FacturaDetalleCon(int IDEmpresa, int IDPedido)
        //{
        //    var parametros = new ConexionParameters();
        //    try
        //    {
        //        parametros.Add("@pIDEmpresa", ConexionDbType.Int, IDEmpresa);
        //        parametros.Add("@pIDFactura", ConexionDbType.VarChar, IDPedido);
        //        parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
        //        parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);
        //        parametros.Add("@pCodError", ConexionDbType.Int, System.Data.ParameterDirection.Output);

        //        var r = _conexion2.ExecuteWithResults<FacturaDetalleModel>("QW_procFacturaDetalleCon", parametros);

        //        return new Result<DataModel>()
        //        {
        //            Value = parametros.Value("@pResultado").ToBoolean(),
        //            Message = parametros.Value("@pMsg").ToString(),
        //            Data = new DataModel()
        //            {
        //                CodigoError = parametros.Value("@pCodError").ToInt32(),
        //                MensajeBitacora = parametros.Value("@pMsg").ToString(),
        //                Data = r.Data
        //            }
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Result<DataModel>()
        //        {
        //            Value = false,
        //            Message = "Problemas al obtener detalle factura",
        //            Data = new DataModel()
        //            {
        //                CodigoError = 101,
        //                MensajeBitacora = ex.Message,
        //                Data = ""
        //            }
        //        };
        //    }
        //}
    }
}
