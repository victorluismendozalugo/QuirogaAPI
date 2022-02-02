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
    public class ReciboMercancia : NancyModule
    {
        private readonly DAReciboMercancia _dAReciboMercancia = null;

        public ReciboMercancia() : base("/recibo")
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
            _dAReciboMercancia = new DAReciboMercancia();

            Post("/guardar", _ => ReciboMciaGuardar());
            Get("/consultar/{IDEmpresa}/{IDOrden}", p => ReciboMciaCon(p));
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

        private object ReciboMciaCon(dynamic p)
        {

            try
            {
                var dat = this.Bind<ReciboMercanciaModel>();
                var result = _dAReciboMercancia.ReciboMciaCon(dat);

                DataSet ds = (DataSet)result.Data.Data;

                ReciboMercanciaModel rb = new ReciboMercanciaModel();
                List<ReciboDetalleModel> lst1 = ds.Tables[1].ToList<ReciboDetalleModel>();

                rb.FechaRecibo = ds.Tables[0].Rows[0]["fechaRecibo"].ToString();
                rb.Estatus = ds.Tables[0].Rows[0]["estatus"].ToString();
                rb.IDRecibo = Convert.ToInt32(ds.Tables[0].Rows[0]["IDRecibo"]);
                rb.IDEmpresa = Convert.ToInt32(ds.Tables[0].Rows[0]["IDEmpresa"]);
                rb.IDOrden = Convert.ToInt32(ds.Tables[0].Rows[0]["IDOrden"]);
                rb.IDFactura = ds.Tables[0].Rows[0]["IDFactura"].ToString();

                rb.reciboDetalle = lst1;

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