using apiQuiroga.DA;
using apiQuiroga.Models;
using apiQuiroga.Models.Catalogos;
using apiQuiroga.Models.Facturas;
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
    public class Facturas : NancyModule
    {
        private readonly DAFacturas _DAFacturas = null;

        public Facturas() : base("/Facturas")
        {
            _DAFacturas = new DAFacturas();
            Post("/facturacon", _ => FacturaCon());
            Post("/facturadetalle", _ => FacturaDetalleCon());

        }

        private object FacturaCon()
        {
            try
            {
                FacturaModel p = this.Bind();

                var r = _DAFacturas.FacturaCon(p.IDEmpresa, p.Folio, p.IDCliente, p.FechaInicio, p.FechaFin, p.Estatus);

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

        private object FacturaDetalleCon()
        {
            try
            {
                FacturaDetalleModel p = this.Bind();

                var r = _DAFacturas.FacturaDetalleCon(p.IDEmpresa, p.IDFacturaEnc);

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
                    Message = "Problemas al obtener detalle factura",
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