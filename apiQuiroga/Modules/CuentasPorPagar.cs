using apiQuiroga.DA;
using apiQuiroga.Models.Movimientos;
using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace apiQuiroga.Modules
{
    public class CuentasPorPagar : NancyModule
    {
        private readonly DACuentasPpagar _DACuentasPpagar = null;

        public CuentasPorPagar() : base("/cuentas")
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
            _DACuentasPpagar = new DACuentasPpagar();

            Get("/consultar/{IDEmpresa}/{IDMovimiento}", p => CuentasPorPagarCon(p));
            Get("/consultar/detalle/{IDEmpresa}/{IDMovimiento}", p => CuentasPorPagarDetalleCon(p));
            Get("/consultar/{IDEmpresa}", p => CuentasPorPagarCon(p));

            Post("/pagar", _ => CuentasPorPagarGuardar());
        }


        private object CuentasPorPagarCon(dynamic p)
        {
            var dat = this.Bind<CuentasXPModel>();
            var result = _DACuentasPpagar.CuentasPorPagarCon(dat);
            return Response.AsJson(result);
        }
        private object CuentasPorPagarDetalleCon(dynamic p)
        {
            var dat = this.Bind<CuentasXPModel>();
            var result = _DACuentasPpagar.CuentasPorPagarDetalleCon(dat);
            return Response.AsJson(result);
        }

        private object CuentasPorPagarGuardar()
        {
            CuentasXPModel p = this.Bind();
            var result = _DACuentasPpagar.CuentasPorPagarGuardar(p);
            return Response.AsJson(result);
        }
    }
}