using apiQuiroga.DA;
using apiQuiroga.Models;
using apiQuiroga.Models.Usuario;
using Nancy;
using Nancy.ModelBinding;
using System;
using System.Linq;
using WarmPack.Classes;

namespace apiQuiroga.Modules
{
    public class AuthModule : NancyModule
    {
        private readonly DAUsuario _DAUsuario = null;

        public AuthModule() : base("/seguridad")
        {


            Before += ctx =>
            {
                if (!ctx.Request.Headers.Keys.Contains("api-key"))
                {
                    return HttpStatusCode.Unauthorized;
                }
                else
                {
                    var apikey = ctx.Request.Headers["api-key"].FirstOrDefault() ?? string.Empty;
                    if (apikey != Globales.ApiKey)
                    {
                        return HttpStatusCode.Unauthorized;
                    }
                    else
                    {
                        return null;
                    }
                }
            };


            _DAUsuario = new DAUsuario();
            Post("/login", _ => Login());
        }

        private object Login()
        {
            try
            {
                var credenciales = this.Bind<UsuarioCredencialesModel>();
                var r = _DAUsuario.Login(credenciales);

                if (r.Value == false)
                {
                    return Response.AsJson(new Result<DataModel>()
                    {
                        Value = r.Value,
                        Message = r.Message,
                        Data = new DataModel()
                        {
                            CodigoError = r.Data.CodigoError,
                            MensajeBitacora = r.Data.MensajeBitacora,
                            Data = ""
                        }
                    });
                }

                UsuarioModel u = (UsuarioModel)r.Data.Data;
                var accessToken = Globales.GetJwtUsuario(u);

                return Response.AsJson(new Result()
                {
                    Value = true,
                    Message = r.Message,
                    Data = new DataModel()
                    {
                        CodigoError = r.Data.CodigoError,
                        MensajeBitacora = r.Data.MensajeBitacora,
                        Data = accessToken
                    }
                });
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result<DataModel>()
                {
                    Value = false,
                    Message = "Problemas en acceso del usuario",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }

    }
}