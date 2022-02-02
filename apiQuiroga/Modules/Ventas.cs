using apiQuiroga.DA;
using apiQuiroga.Models;
using apiQuiroga.Models.Catalogos;
using apiQuiroga.Models.Ventas;
using apiQuiroga.Models.Usuario;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WarmPack.Classes;

namespace apiQuiroga.Modules
{
    public class Ventas : NancyModule
    {
        private readonly DAVentas _DAVentas = null;

        public Ventas() : base("/Ventas")
        {
            _DAVentas = new DAVentas();
            Post("/CuentasPCobrarcon", _ => CuentasPCobrarCon());
            //Post("/facturadetalle", _ => FacturaDetalleCon());

        }

        private object CuentasPCobrarCon()
        {
            try
            {
                CuentasPCobrarModel p = this.Bind();

                var r = _DAVentas.CuentasPCobrarCon(p.idEmpresa, p.folioFactura, p.idCliente, p.fechaInicio, p.fechaFin, p.estatus);

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
                    Message = "Problemas al obtener factura",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }

        //private object FacturaDetalleCon()
        //{
        //    try
        //    {
        //        FacturaDetalleModel p = this.Bind();

        //        var r = _DAVentas.FacturaDetalleCon(p.IDEmpresa, p.IDFacturaEnc);

        //        return Response.AsJson(new Result<DataModel>()
        //        {
        //            Value = r.Value,
        //            Message = r.Message,
        //            Data = new DataModel()
        //            {
        //                CodigoError = r.Data.CodigoError,
        //                MensajeBitacora = r.Data.MensajeBitacora,
        //                Data = r.Data.Data
        //            }
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Response.AsJson(new Result<DataModel>()
        //        {
        //            Value = false,
        //            Message = "Problemas al obtener detalle factura",
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