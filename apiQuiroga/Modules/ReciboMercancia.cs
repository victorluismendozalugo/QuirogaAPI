using apiQuiroga.DA;
using apiQuiroga.Models;
using apiQuiroga.Models.Movimientos;
using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WarmPack.Classes;

namespace apiQuiroga.Modules
{
    public class ReciboMercancia : NancyModule
    {
        private readonly DAReciboMercancia _dAReciboMercancia = null;

        public ReciboMercancia() : base("/recibo")
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
            //this.RequiresAuthentication();
            _dAReciboMercancia = new DAReciboMercancia();

            Post("/guardar", _ => ReciboMciaGuardar());
        }

        private object ReciboMciaGuardar()
        {
            try
            {
                ReciboMercanciaModel p = this.Bind();
                var r = _dAReciboMercancia.ReciboMciaGuardar(p);
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
                    Message = "Problemas al registrar los datos",
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