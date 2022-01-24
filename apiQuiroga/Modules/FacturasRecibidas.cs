using apiQuiroga.DA;
using apiQuiroga.Models;
using apiQuiroga.Models.Movimientos;
using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WarmPack.Classes;
using WarmPack.Extensions;

namespace apiQuiroga.Modules
{
    public class FacturasRecibidas : NancyModule
    {
        private readonly DAFacturasRecibidas _DAFacturasRecibidas = null;

        public FacturasRecibidas() : base("/factrec")
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
            _DAFacturasRecibidas = new DAFacturasRecibidas();

            Post("/guardar", _ => FacturasRecibidasGuardar());
            Get("/consultar/{IDEmpresa}/{IDMovimiento}", p => FacturasRecibidasCon(p));
        }

        private object FacturasRecibidasGuardar()
        {
            try
            {
                FacturasRecibidasModel p = this.Bind();
                var r = _DAFacturasRecibidas.FacturasRecibidasGuardar(p);
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

        private object FacturasRecibidasCon(dynamic p)
        {

            try
            {
                var dat = this.Bind<FacturasRecibidasModel>();
                var result = _DAFacturasRecibidas.FacturasRecibidasCon(dat);

                DataSet ds = (DataSet)result.Data.Data;

                FacturasRecibidasModel rb = new FacturasRecibidasModel();
                List<FacturasRecibidasDetalleModel> lst1 = ds.Tables[1].ToList<FacturasRecibidasDetalleModel>();

                rb.IDMovimiento = Convert.ToInt32(ds.Tables[0].Rows[0]["IDMovimiento"]);
                rb.IDRecibo = Convert.ToInt32(ds.Tables[0].Rows[0]["IDRecibo"]);
                rb.IDEmpresa = Convert.ToInt32(ds.Tables[0].Rows[0]["IDEmpresa"]);
                rb.IDOrden = Convert.ToInt32(ds.Tables[0].Rows[0]["IDOrden"]);
                rb.IDFactura = ds.Tables[0].Rows[0]["IDFactura"].ToString();


                rb.facturasRecibidasDetalle = lst1;

                return Response.AsJson(new Result<DataModel>()
                {
                    Value = true,
                    Message = "Orden compra",
                    Data = new DataModel()
                    {
                        CodigoError = 0,
                        MensajeBitacora = "",
                        Data = rb
                    }
                });
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result<DataModel>()
                {
                    Value = false,
                    Message = "Problemas al obtener los datos",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
            //return Response.AsJson(rb);
        }
    }
}