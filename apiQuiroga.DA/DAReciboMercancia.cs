using apiQuiroga.Models;
using apiQuiroga.Models.Movimientos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;
using WarmPack.Extensions;

namespace apiQuiroga.DA
{
    public class DAReciboMercancia
    {
        private readonly Conexion _conexion = null;
        public DAReciboMercancia()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionSecundaria);
        }
        public Result<DataModel> ReciboMciaGuardar(ReciboMercanciaModel recibo)
        {
            var parametros = new ConexionParameters();
            var xml = recibo.ToXml("root");
            try
            {
                parametros.Add("@pDatosXML", ConexionDbType.Xml, xml);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);
                parametros.Add("@pCodError", ConexionDbType.Int, System.Data.ParameterDirection.Output);

                var r = _conexion.Execute("QW_procReciboMciaGuardar", parametros);

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
                    Message = "Problemas al registrar los datos",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                };
            }
        }

        public Result<DataModel> ReciboMciaCon(ReciboMercanciaModel rec)
        {
            try
            {
                DataSet dsRep;
                ConexionParameters parametros = new ConexionParameters();

                parametros.Add("@pIDEmpresa", ConexionDbType.Int, rec.IDEmpresa);
                parametros.Add("@pIDOrden", ConexionDbType.Int, rec.IDOrden);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output);
                parametros.Add("@pCodError", ConexionDbType.Int, System.Data.ParameterDirection.Output);

                var r =  this._conexion.ExecuteWithResults("QW_procReciboMciaCon", parametros, out dsRep);


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
    }
}
