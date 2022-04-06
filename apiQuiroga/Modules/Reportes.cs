using apiQuiroga.DA;
using apiQuiroga.Models;
using CrystalDecisions.CrystalReports.Engine;
using Nancy;
using System;
using System.Linq;
using System.IO;
using WarmPack.Classes;
using Nancy.Security;
using System.Data;
using apiQuiroga.Models.Catalogos;
using Nancy.ModelBinding;
using apiQuiroga.Models.Movimientos;

namespace apiQuiroga.Modules
{
    public class Reportes : NancyModule
    {
        private readonly DAReportes _DAReportes = null;

        public Reportes() : base("/reportes")
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

            _DAReportes = new DAReportes();

            //Post("/ordencompra", _ => GeneraImpresionOrdenCompra());
            Post("/ordencompra", _ => RptOrdenCompra());

        }

        private object GeneraImpresionOrdenCompra()
        {
            try
            {
                OrdenCompraModel p = this.Bind();

                var r = _DAReportes.GeneraImpresionOrdenCompra(p.claveOrden, p.claveEmpresa);

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
                    Message = "Problemas al obtener el cabecero de la orden",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object RptOrdenCompra()
        {
            String b64Str;
            try
            {
                OrdenCompraModel p = this.Bind();

                var r = _DAReportes.RptOrdenCompra(p.claveOrden, p.claveEmpresa);

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

                DataSet ds = (DataSet)r.Data.Data;

                ReportDocument cryRpt = new ReportDocument();
                cryRpt.Load(Globales.RutaApp + "Reportes\\Movimientos\\OrdenCompra.rpt");
                cryRpt.Database.Tables["Cabecero"].SetDataSource(ds.Tables[0]);
                cryRpt.Database.Tables["Detalle"].SetDataSource(ds.Tables[1]);

                using (Stream stm = cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat))
                {
                    stm.Seek(0, SeekOrigin.Begin);
                    byte[] buffer = new byte[stm.Length];
                    stm.Read(buffer, 0, (int)stm.Length);
                    b64Str = Convert.ToBase64String(buffer);
                }

                return Response.AsJson(new Result<DataModel>()
                {
                    Value = true,
                    Message = "Orden compra",
                    Data = new DataModel()
                    {
                        CodigoError = 0,
                        MensajeBitacora = "",
                        Data = b64Str
                    }
                });
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result<DataModel>()
                {
                    Value = false,
                    Message = "Problemas al obtener la orden de compra",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }



        //private object FamiliasCat()
        //{
        //    String b64Str;
        //    try
        //    {
        //        FamiliasModel p = this.Bind();

        //        var r = _DAReportes.FamiliasCat();

        //        if (r.Value == false)
        //        {
        //            return Response.AsJson(new Result<DataModel>()
        //            {
        //                Value = r.Value,
        //                Message = r.Message,
        //                Data = new DataModel()
        //                {
        //                    CodigoError = r.Data.CodigoError,
        //                    MensajeBitacora = r.Data.MensajeBitacora,
        //                    Data = ""
        //                }
        //            });
        //        }

        //        DataSet ds = (DataSet)r.Data.Data;

        //        ReportDocument cryRpt = new ReportDocument();
        //        cryRpt.Load(Globales.RutaApp + "Reportes\\Catalogos\\repCatFamilias.rpt");
        //        cryRpt.Database.Tables["Detalle"].SetDataSource(ds.Tables[0]);

        //        using (Stream stm = cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat))
        //        {
        //            stm.Seek(0, SeekOrigin.Begin);
        //            byte[] buffer = new byte[stm.Length];
        //            stm.Read(buffer, 0, (int)stm.Length);
        //            b64Str = Convert.ToBase64String(buffer);
        //        }

        //        return Response.AsJson(new Result<DataModel>()
        //        {
        //            Value = true,
        //            Message = "Reporte de familias",
        //            Data = new DataModel()
        //            {
        //                CodigoError = 0,
        //                MensajeBitacora = "",
        //                Data = b64Str
        //            }
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Response.AsJson(new Result<DataModel>()
        //        {
        //            Value = false,
        //            Message = "Problemas en reporte de familias",
        //            Data = new DataModel()
        //            {
        //                CodigoError = 101,
        //                MensajeBitacora = ex.Message,
        //                Data = ""
        //            }
        //        });
        //    }
        //}
    }
}