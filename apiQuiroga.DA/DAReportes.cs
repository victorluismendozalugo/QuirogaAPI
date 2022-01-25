using apiQuiroga.Models;
using System;
using System.Data;
using WarmPack.Classes;
using WarmPack.Database;

namespace apiQuiroga.DA
{
    public class DAReportes
    {
        private readonly Conexion _conexion2 = null;

        public DAReportes()
        {
            _conexion2 = new Conexion(ConexionType.MSSQLServer, Globales.ConexionSecundaria);
        }

        public Result<DataModel> GeneraImpresionOrdenCompra(int claveOrden, int claveEmpresa)
        {
            var parametros = new ConexionParameters();
            try
            {
                parametros.Add("@pClaveEmpresa", ConexionDbType.Int, claveEmpresa);
                parametros.Add("@pClaveOrden", ConexionDbType.Int, claveOrden);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);
                parametros.Add("@pCodError", ConexionDbType.Int, System.Data.ParameterDirection.Output);

                var r = _conexion2.Execute("QW_procGeneraImpresionOrdenCompra", parametros);

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
                    Message = "Problemas al generar",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                };
            }
        }

        public Result<DataModel> RptOrdenCompra(int claveOrden, int claveEmpresa)
        {
            var parametros = new ConexionParameters();
            DataSet dsRep;
            try
            {
                parametros.Add("@pClaveEmpresa", ConexionDbType.Int, claveEmpresa);
                parametros.Add("@pClaveOrden", ConexionDbType.Int, claveOrden);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);
                parametros.Add("@pCodError", ConexionDbType.Int, System.Data.ParameterDirection.Output);

                var r = _conexion2.ExecuteWithResults("QW_rptOrdenCompraCon", parametros, out dsRep);

                return new Result<DataModel>()
                {
                    Value = parametros.Value("@pResultado").ToBoolean(),
                    Message = parametros.Value("@pMsg").ToString(),
                    Data = new DataModel()
                    {
                        CodigoError = parametros.Value("@pCodError").ToInt32(),
                        MensajeBitacora = parametros.Value("@pMsg").ToString(),
                        Data = dsRep
                    }
                };
            }
            catch (Exception ex)
            {
                return new Result<DataModel>()
                {
                    Value = false,
                    Message = "Problemas en orden compra",
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