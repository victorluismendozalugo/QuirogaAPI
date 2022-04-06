using apiQuiroga.DA;
using apiQuiroga.Models.Catalogos;
using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace apiQuiroga.Modules
{
    public class ProductosModule : NancyModule
    {
        private readonly DAProductos _DAProductos = null;

        public ProductosModule() : base("/productos")
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
            _DAProductos = new DAProductos();

            Get("/ofertas", _ => GetOfertas());
            Post("/ofertas", _ => GuardarOfertas());
            Post("/ofertas/delete", _ => DeleteOfertas());
        }


        private object GetOfertas()
        {
            var result = _DAProductos.ProductosOfertasCon();
            return Response.AsJson(result);
        }
        private object GuardarOfertas()
        {
            var dat = this.Bind<ProductosModel>();
            var result = _DAProductos.ProductosOfertasGuardar(dat);
            return Response.AsJson(result);
        }
        
        private object DeleteOfertas()
        {
            ProductosModel p = this.Bind<ProductosModel>();
            var result = _DAProductos.ProductosOfertasEliminar(p);
            return Response.AsJson(result);
        }
    }
}