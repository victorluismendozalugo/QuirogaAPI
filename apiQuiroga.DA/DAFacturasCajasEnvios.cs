using apiQuiroga.Models;
using apiQuiroga.Models.Facturas;
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
    public class DAFacturasCajasEnvios
    {
        private readonly Conexion _conexion = null;

        public DAFacturasCajasEnvios()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionSecundaria);
        }

        public Result<List<FacturasCajasEnviosModel>> FacturasCon(FacturasCajasEnviosModel mod)
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pIDOrigen", ConexionDbType.Int, mod.IDOrigen);
            parametros.Add("@pIDDestino", ConexionDbType.Int, mod.IDDestino);
            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);

            var r = _conexion.ExecuteWithResults<FacturasCajasEnviosModel>("QW_procFacturasSAICon", parametros);

            return r;
        }

        public Result<List<FacturasCajasEnviosModel>> CajasMovimientosCon()
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);
            parametros.Add("@pCodError", ConexionDbType.Int, System.Data.ParameterDirection.Output);

            var r = _conexion.ExecuteWithResults<FacturasCajasEnviosModel>("QW_procCajasMovimientosCon", parametros);

            return r;
        }
        public Result<List<FacturasCajasEnviosModel>> CajasMovimientosGuiasCon(FacturasCajasEnviosModel f)
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pIdMovimiento", ConexionDbType.Int, f.IdMovimiento);
            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);
            parametros.Add("@pCodError", ConexionDbType.Int, System.Data.ParameterDirection.Output);

            var r = _conexion.ExecuteWithResults<FacturasCajasEnviosModel>("QW_procCajasMovimientosGuiasCon", parametros);

            return r;
        }
        public Result<List<FacturasCajasEnviosModel>> EstatusCajasCon(FacturasCajasEnviosModel fact)
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pIDCaja", ConexionDbType.Int, fact.IDCaja1);
            parametros.Add("@pIdUbicacion", ConexionDbType.Int, fact.IDOrigen);
            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);
            parametros.Add("@pCodError", ConexionDbType.Int, System.Data.ParameterDirection.Output);
            var r = _conexion.ExecuteWithResults<FacturasCajasEnviosModel>("QW_procCajasEstatusCon", parametros);

            return r;
        }
        public Result<List<FacturasCajasEnviosModel>> EstatusCajasGuiasCon(FacturasCajasEnviosModel fact)
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pGuia", ConexionDbType.VarChar, fact.Guia);
            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);
            parametros.Add("@pCodError", ConexionDbType.Int, System.Data.ParameterDirection.Output);
            var r = _conexion.ExecuteWithResults<FacturasCajasEnviosModel>("QW_procCajasGuiasEstatusCon", parametros);

            return r;
        }

        public Result<DataModel> CajasMovimientosGuardar(FacturasCajasEnviosModel fact)
        {
            var parametros = new ConexionParameters();
            var xml = fact.FacturasDet.ToXml("root");
            try
            {
                parametros.Add("@pIdMovimiento", ConexionDbType.Int, fact.IdMovimiento);
                parametros.Add("@pIdCaja1", ConexionDbType.Int, fact.IDCaja1);
                parametros.Add("@pIdCaja2", ConexionDbType.Int, fact.IDCaja2);
                parametros.Add("@pIdUbicacion", ConexionDbType.Int, fact.IDOrigen);
                parametros.Add("@pIdUbicacionDestino", ConexionDbType.Int, fact.IDDestino);
                parametros.Add("@pGuia", ConexionDbType.VarChar, fact.Guia);
                parametros.Add("@pUsuarioRegistro", ConexionDbType.VarChar, fact.UsuarioRegistro);
                parametros.Add("@pFacturas", ConexionDbType.Xml, xml);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);
                parametros.Add("@pCodError", ConexionDbType.Int, System.Data.ParameterDirection.Output);
                //parametros.Add("@pMovimiento", ConexionDbType.Int, System.Data.ParameterDirection.Output);

                var r = _conexion.Execute("QW_procCajasPedidoGuardar", parametros);

                return new Result<DataModel>()
                {
                    Value = parametros.Value("@pResultado").ToBoolean(),
                    Message = parametros.Value("@pMsg").ToString(),
                    Data = new DataModel()
                    {
                        CodigoError = parametros.Value("@pCodError").ToInt32(),
                        MensajeBitacora = parametros.Value("@pMsg").ToString(),
                        //CodigoMovimiento = parametros.Value("@pMovimiento").ToInt32(),
                        Data = r.Data
                    }
                };
            }
            catch (Exception ex)
            {
                return new Result<DataModel>()
                {
                    Value = false,
                    Message = "Problemas al guardar el movimiento",
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
