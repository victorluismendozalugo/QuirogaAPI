using apiQuiroga.DA;
using apiQuiroga.Models;
using apiQuiroga.Models.Usuario;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using System;
using System.Linq;
using WarmPack.Classes;

namespace apiQuiroga.Modules
{
    public class Usuarios : NancyModule
    {
        private readonly DAUsuario _DAUsuario = null;

        public Usuarios() : base("/usuario")
        {

            //Before += ctx =>
            //{
            //    if (!ctx.Request.Headers.Keys.Contains("api-key"))
            //    {
            //        return HttpStatusCode.Unauthorized;
            //    }
            //    else
            //    {
            //        var apikey = ctx.Request.Headers["api-key"].FirstOrDefault() ?? string.Empty;
            //        if (apikey != Globales.ApiKey)
            //        {
            //            return HttpStatusCode.Unauthorized;
            //        }
            //        else
            //        {
            //            return null;
            //        }
            //    }
            //};
            //this.RequiresAuthentication();

            _DAUsuario = new DAUsuario();

            Post("/menu", _ => Menu());
        }

        private object Menu()
        {
            try
            {
                UsuarioMenuModel p = this.Bind();

                var r = _DAUsuario.MenuCon(p.CodigoUsuario);

                return Response.AsJson(new Result<DataModel>()
                {
                    Value = r.Value,
                    Message = r.Message,
                    Data = new DataModel()
                    {
                        CodigoError = r.Data.CodigoError,
                        MensajeBitacora = r.Data.MensajeBitacora,
                        Data = r.Data.Data
                    }
                });
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result<DataModel>()
                {
                    Value = false,
                    Message = "Problemas en el menú",
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