using apiQuiroga.Models;
using apiQuiroga.Models.Movimientos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;

namespace apiQuiroga.DA
{
    public class DACuentasPpagar
    {
        private readonly Conexion _conexion = null;
        public DACuentasPpagar()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionSecundaria);
        }

        public Result<DataModel> CuentasPorPagarCon(CuentasXPModel cuentas)
        {
            var parametros = new ConexionParameters();
            try
            {
                parametros.Add("@pIDEmpresa", ConexionDbType.Int, cuentas.IDEmpresa);
                parametros.Add("@pIDMovimiento", ConexionDbType.Int, cuentas.IDMovimiento);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);
                parametros.Add("@pCodError", ConexionDbType.Int, System.Data.ParameterDirection.Output);

                var r = _conexion.ExecuteWithResults<CuentasXPModel>("QW_procCuentasPorPagarCon", parametros);

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
                    Message = "Problemas al obtener los datos",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                };
            }
        }
        public Result<DataModel> CuentasPorPagarDetalleCon(CuentasXPModel cuentas)
        {
            var parametros = new ConexionParameters();
            try
            {
                parametros.Add("@pIDEmpresa", ConexionDbType.Int, cuentas.IDEmpresa);
                parametros.Add("@pIDMovimiento", ConexionDbType.Int, cuentas.IDMovimiento);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);
                parametros.Add("@pCodError", ConexionDbType.Int, System.Data.ParameterDirection.Output);

                var r = _conexion.ExecuteWithResults<CuentasXPModel>("QW_procCuentasPorPagarDetalleCon", parametros);

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
                    Message = "Problemas al obtener los datos",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                };
            }
        }

        public Result CuentasPorPagarGuardar(CuentasXPModel cuentas)
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pIDMovimiento", ConexionDbType.Int, cuentas.IDMovimiento);
            parametros.Add("@pIDEmpresa", ConexionDbType.Int, cuentas.IDEmpresa);
            parametros.Add("@pIDFactura", ConexionDbType.VarChar, cuentas.IDFactura);
            parametros.Add("@pCheque", ConexionDbType.VarChar, cuentas.Cheque);
            parametros.Add("@pConcepto", ConexionDbType.VarChar, cuentas.Concepto);
            parametros.Add("@pFechaPago", ConexionDbType.VarChar, cuentas.FechaPago);
            parametros.Add("@pImporte", ConexionDbType.Decimal, cuentas.Importe);
            parametros.Add("@pImporteDescuento", ConexionDbType.Decimal, cuentas.ImporteDescuento);
            parametros.Add("@pUsuarioRegistro", ConexionDbType.VarChar, cuentas.UsuarioRegistro);

            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);

            var r = _conexion.Execute("QW_procCuentasPorPagarGuardar", parametros);

            return r;

        }
    }
}
