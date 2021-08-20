using apiQuiroga.DA;
using apiQuiroga.Models;
using apiQuiroga.Models.Catalogos;
using apiQuiroga.Models.Movimientos;
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
    public class Movimientos : NancyModule
    {
        private readonly DAMovimientos _DAMovimientos = null;


        public Movimientos() : base("/movimientos")
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

            _DAMovimientos = new DAMovimientos();

            //Post("/compras", _ => Compras());
            //Get("/compras", _ => Compras());
            //Post("/comprasdetalle", _ => ComprasDetalle());
            //Post("/comprasdetalle/actualizar", _ => ComprasDetalleActualiza());
            //Get("/comprasdetalle", _ => ComprasDetalle());
            //Post("/compras/guardar", _ => ComprasGuardar());
            //Post("/compras/detguardar", _ => ComprasDetGuardar());
            //Post("/compras/actualizar", _ => ComprasActualizar());
            //Post("/compras/validar", _ => ComprasValidar());
            //Post("/compras/aplicar", _ => ComprasAplicarFactura());
            //Post("/bitacora", _ => BitacoraRecepcion());
            //Post("/bitacora/guardar", _ => BitacoraRecepcionGuardar());
            //Post("/usuarios", _ => Usuarios());

            //Post("/operaciones/autorizar", _ => OperacionSupervisadaAutorizar());

            //Post("/compras/cuentasXpagar", _ => CuentasXPagar());

            //Post("/recibo/guardar", _ => ReciboGuardar());
            //Post("/recibo/detguardar", _ => ReciboDetGuardar());

            //genera-consulta y actualiza las ordenes de compra...
            Post("/ordencompra", _ => OrdenCompraCon());
            Post("/ordencompra/detalle", _ => OrdenCompraDetalleCon());
            Post("/ordencompra/guardar", _ => OrdenCompraGuardar());
<<<<<<< HEAD
            Post("/ordencompra/autorizar", _ => OrdenCompraAutorizar());
            //Post("/ordencompra/detalle/guardar", _ => OrdenCompraDetalleGuardar());
            //genera-consulta y actualiza las ordenes de compra...
=======
            Post("/ordencompra/detalle/guardar", _ => OrdenCompraDetalleGuardar());
            ///genera-consulta y actualiza las ordenes de compra...
            ///
            Post("/ventas/pedidocon", _ => PedidoCon());
            Post("/ventas/pedidodetalle", _ => PedidoDetalleCon());
>>>>>>> 5f99ad6d2cf2ea9e8a08b0da4b9c508d4dbe537f
        }

        ///genera-consulta y actualiza las ordenes de compra...
        private object OrdenCompraCon()
        {
            try
            {
                OrdenCompraModel p = this.Bind();

                var r = _DAMovimientos.OrdenCompraCon(p.claveOrden, p.claveEmpresa, p.estatusOrden);

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
        private object OrdenCompraDetalleCon()
        {
            try
            {
                OrdenCompraModel p = this.Bind();

                var r = _DAMovimientos.OrdenCompraDetalleCon(p.claveOrden, p.claveEmpresa, p.estatusOrden);

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
                    Message = "Problemas al obtener el detalle de la orden",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object OrdenCompraGuardar()
        {
            try
            {
                OrdenCompraModel p = this.Bind();
                var r = _DAMovimientos.OrdenCompraGuardar(p);
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
                    Message = "Problemas al guardar el movimiento",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object OrdenCompraAutorizar()
        {
            try
            {
                OrdenCompraModel p = this.Bind();

                var r = _DAMovimientos.OrdenCompraAutorizar(p.claveOrden, p.claveEmpresa, p.estatusOrden);

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
                    Message = "Problemas al autorizar la orden",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        ///genera-consulta y actualiza las ordenes de compra...

        private object Compras()
        {
            try
            {
                ComprasModel p = this.Bind();

                var r = _DAMovimientos.Compras(p.clavePedido, p.estatus);

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
                    Message = "Problemas al obtener las compras",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object CuentasXPagar()
        {
            try
            {
                CuentasXPModel p = this.Bind();

                var r = _DAMovimientos.CuentasPorPagar(p.EmpresaID, p.NumeroFactura);

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
                    Message = "Problemas al obtener las cuentas",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object ComprasDetalle()
        {
            try
            {
                ComprasModel p = this.Bind();

                var r = _DAMovimientos.ComprasDet(p.clavePedido, p.estatus);

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
                    Message = "Problemas al obtener las compras",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object ComprasDetalleActualiza()
        {
            try
            {
                ComprasModel p = this.Bind();

                var r = _DAMovimientos.ComprasDetalleActualiza(p);

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
                    Message = "Problemas al obtener las compras",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object ComprasGuardar()
        {
            try
            {
                //var t = this.BindToken();
                ComprasModel p = this.Bind();

                var r = _DAMovimientos.ComprasGuardar(p);

                return Response.AsJson(new Result<DataModel>()
                {
                    Value = r.Value,
                    Message = r.Message,
                    Data = new DataModel()
                    {
                        CodigoError = r.Data.CodigoError,
                        MensajeBitacora = r.Data.MensajeBitacora,
                        CodigoMovimiento = r.Data.CodigoMovimiento,
                        Data = r.Data.Data
                    }
                });
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result<DataModel>()
                {
                    Value = false,
                    Message = ex.Message, // "Problemas al guardar ...",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object ReciboGuardar()
        {
            try
            {
                //var t = this.BindToken();
                ComprasModel p = this.Bind();

                var r = _DAMovimientos.ReciboGuardar(p);

                return Response.AsJson(new Result<DataModel>()
                {
                    Value = r.Value,
                    Message = r.Message,
                    Data = new DataModel()
                    {
                        CodigoError = r.Data.CodigoError,
                        MensajeBitacora = r.Data.MensajeBitacora,
                        CodigoMovimiento = r.Data.CodigoMovimiento,
                        Data = r.Data.Data
                    }
                });
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result<DataModel>()
                {
                    Value = false,
                    Message = ex.Message, // "Problemas al guardar ...",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object ComprasActualizar()
        {
            try
            {
                //var t = this.BindToken();
                ComprasModel p = this.Bind();

                var r = _DAMovimientos.ComprasActualizar(p);

                return Response.AsJson(new Result<DataModel>()
                {
                    Value = r.Value,
                    Message = r.Message,
                    Data = new DataModel()
                    {
                        CodigoError = r.Data.CodigoError,
                        MensajeBitacora = r.Data.MensajeBitacora,
                        CodigoMovimiento = r.Data.CodigoMovimiento,
                        Data = r.Data.Data
                    }
                });
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result<DataModel>()
                {
                    Value = false,
                    Message = ex.Message, // "Problemas al guardar ...",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object ComprasValidar()
        {
            try
            {
                //var t = this.BindToken();
                ComprasModel p = this.Bind();

                var r = _DAMovimientos.ComprasValidar(p);

                return Response.AsJson(new Result<DataModel>()
                {
                    Value = r.Value,
                    Message = r.Message,
                    Data = new DataModel()
                    {
                        CodigoError = r.Data.CodigoError,
                        MensajeBitacora = r.Data.MensajeBitacora,
                        CodigoMovimiento = r.Data.CodigoMovimiento,
                        Data = r.Data.Data
                    }
                });
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result<DataModel>()
                {
                    Value = false,
                    Message = ex.Message, // "Problemas al guardar ...",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object ComprasAplicarFactura()
        {
            try
            {
                //var t = this.BindToken();
                ComprasModel p = this.Bind();

                var r = _DAMovimientos.ComprasAplicarFactura(p);

                return Response.AsJson(new Result<DataModel>()
                {
                    Value = r.Value,
                    Message = r.Message,
                    Data = new DataModel()
                    {
                        CodigoError = r.Data.CodigoError,
                        MensajeBitacora = r.Data.MensajeBitacora,
                        CodigoMovimiento = r.Data.CodigoMovimiento,
                        Data = r.Data.Data
                    }
                });
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result<DataModel>()
                {
                    Value = false,
                    Message = ex.Message, // "Problemas al guardar ...",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object ComprasDetGuardar()
        {
            try
            {
                //var t = this.BindToken();
                ComprasModel p = this.Bind();

                var r = _DAMovimientos.ComprasDetalleGuardar(p);

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
                    Message = ex.Message, // "Problemas al guardar ...",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object ReciboDetGuardar()
        {
            try
            {
                //var t = this.BindToken();
                ComprasModel p = this.Bind();

                var r = _DAMovimientos.ReciboDetalleGuardar(p);

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
                    Message = ex.Message, // "Problemas al guardar ...",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object BitacoraRecepcion()
        {
            try
            {
                BitacoraRecepcionModel p = this.Bind();

                var r = _DAMovimientos.Bitacora(p.BitacoraID, p.BitUsuarioAdicion);

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
                    Message = "Problemas al obtener la bitácora",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object BitacoraRecepcionGuardar()
        {
            try
            {
                //var t = this.BindToken();
                BitacoraRecepcionModel p = this.Bind();

                var r = _DAMovimientos.BitacoraGuardar(p);

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
                    Message = ex.Message, // "Problemas al guardar ...",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object Usuarios()
        {
            try
            {
                //var t = this.BindToken();
                UsuarioModel p = this.Bind();

                var r = _DAMovimientos.Usuarios(p.Usuario, 0);

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
                    Message = "Problemas al obtener usuarios",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object OperacionSupervisadaAutorizar()
        {
            try
            {
                //var t = this.BindToken();
                OperacionesModel p = this.Bind();

                var r = _DAMovimientos.OperacionSupervisadaAutorizar(p);

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
                    Message = ex.Message, // "Problemas al guardar ...",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }

        private object PedidoCon()
        {
            try
            {
                PedidoModel p = this.Bind();

                var r = _DAMovimientos.PedidoCon(p.IDEmpresa, p.IDPedidoEnc);

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

                var r = _DAMovimientos.PedidoDetalleCon(p.IDEmpresa, p.IDPedidoEnc );

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
    }
}