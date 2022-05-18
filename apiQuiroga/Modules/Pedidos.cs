using apiQuiroga.DA;
using apiQuiroga.Models;
using apiQuiroga.Models.Catalogos;
using apiQuiroga.Models.Pedidos;
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
    public class Pedidos : NancyModule
    {
        private readonly DAPedidos _DAPedidos = null;

        public Pedidos() : base("/Pedidos")
        {
            _DAPedidos = new DAPedidos();
            Post("/pedidocon", _ => PedidoCon());
            Post("/pedidodetalle", _ => PedidoDetalleCon());
            Post("/pedido/guardar", _ => PedidoGuardar());
            Get("/foliosucursal/{idEmpresa}", _ => ObtenFolioPedidoPorSucursal());
            Post("/productos", _ => PostProductos());
        }

        private object PedidoCon()
        {
            try
            {
                PedidoModel p = this.Bind();

                var r = _DAPedidos.PedidoCon(p.idEmpresa, p.idPedidoEnc);

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
                    Message = "Problemas al obtener pedido",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }

        private object PedidoDetalleCon()
        {
            try
            {
                PedidoDetalleModel p = this.Bind();

                var r = _DAPedidos.PedidoDetalleCon(p.idEmpresa, p.idPedidoEnc);

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
                    Message = "Problemas al obtener detalle pedido",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }

        private object PedidoGuardar()
        {
            try
            {
                PedidoModel p = this.Bind();
                var r = _DAPedidos.PedidoGuardar(p);
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
                    Message = "Problemas al guardar movimiento pedido",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object ObtenFolioPedidoPorSucursal()
        {
            var p = this.Bind<PedidoModel>();
            var result = _DAPedidos.ObtenFolioPedidoPorSucursal(p);
            return Response.AsJson(result);
        }

        private object PostProductos()
        {
            try
            {
                var p = this.BindModel();
                string buscar = p.buscar;
                int codCliente = p.codCliente;
                int codAgente = p.codAgente;

                var r = _DAPedidos.Productos(buscar, codCliente, codAgente);

                return Response.AsJson(r);
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result(ex));
            }
        }

    }
}