using apiQuiroga.Models;
using apiQuiroga.Models.Catalogos;
using apiQuiroga.Models.Usuario;
using System;
using WarmPack.Classes;
using WarmPack.Database;

namespace apiQuiroga.DA
{
    public class DAUsuario
    {
        private readonly Conexion _conexion = null;

        public DAUsuario()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }

        public Result<DataModel> Login(UsuarioCredencialesModel credenciales)
        {
            var parametros = new ConexionParameters();
            try
            {
                parametros.Add("@pUsuario", ConexionDbType.VarChar, credenciales.Usuario);
                parametros.Add("@pPassword", ConexionDbType.VarChar, credenciales.Password);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);
                parametros.Add("@pCodError", ConexionDbType.Int, System.Data.ParameterDirection.Output);

                var r = new UsuarioModel();
                _conexion.ExecuteWithResults("procUsuariosIdentificar", parametros, row =>
                {
                    r.IdUsuario = row["IdUsuario"].ToInt32();
                    r.Usuario = row["Usuario"].ToString();
                });

                return new Result<DataModel>()
                {
                    Value = parametros.Value("@pResultado").ToBoolean(),
                    Message = parametros.Value("@pMsg").ToString(),
                    Data = new DataModel()
                    {
                        CodigoError = parametros.Value("@pCodError").ToInt32(),
                        MensajeBitacora = parametros.Value("@pMsg").ToString(),
                        Data = r
                    }
                };
            }
            catch (Exception ex)
            {
                return new Result<DataModel>()
                {
                    Value = false,
                    Message = "Problemas en acceso del usuario",
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