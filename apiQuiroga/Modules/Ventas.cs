using apiQuiroga.DA;
using apiQuiroga.Models;
using apiQuiroga.Models.Catalogos;
using apiQuiroga.Models.Ventas;
using apiQuiroga.Models.CajasPedido;
using apiQuiroga.Models.Usuario;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WarmPack.Classes;
using apiQuiroga.Models.Facturas;


namespace apiQuiroga.Modules
{
    public class Ventas : NancyModule
    {
        private readonly DAVentas _DAVentas = null;
        private readonly DAFacturasCajasEnvios _DAFacturas = null;

        public Ventas() : base("/Ventas")
        {
            _DAVentas = new DAVentas();
            _DAFacturas = new DAFacturasCajasEnvios();


            Post("/CuentasPCobrarcon", _ => CuentasPCobrarCon());
            //Post("/facturadetalle", _ => FacturaDetalleCon());

            Post("/facturas", _ => FacturasCon());
            Post("/cajas/movimiento", _ => CajasMovimientosGuardar());
            Get("/cajas/movimiento/{IDOrigen}", _ => CajasMovimientosCon());
            Post("/cajas/movimiento/guia", _ => CajasMovimientosGuiasCon());
            Get("/cajas/estatus/{IDCaja1}/{IDOrigen}", _ => CajasEstatusCon());
            Get("/guias/estatus/{Guia}", _ => CajasEstatusGuiasCon());


            Post("/CajasPedidoGuardar", _ => CajasPedidoGuardar());
            Post("/CajasPedidoActualizar", _ => CajasPedidoActualizar());
            Post("/ListadoCajasPedido", _ => ListadoCajasPedido());
            Post("/ListadoCajasAsignadas", _ => ListadoCajasAsignadas());
            Post("/ListadoCambiosCajasPedido", _ => ListadoCambiosCajasPedido());
            Post("/CajasPedidoRecibir", _ => CajasPedidoRecibir());

        }

        private object CajasMovimientosGuardar()
        {
            FacturasCajasEnviosModel p = this.Bind();
            var r = _DAFacturas.CajasMovimientosGuardar(p);
            return Response.AsJson(r);
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


        private object FacturasCon()
        {
            var dat = this.Bind<FacturasCajasEnviosModel>();
            var result = _DAFacturas.FacturasCon(dat);
            return Response.AsJson(result);
        }

        private object CajasPedidoGuardar()
        {
            try
            {
                CajasPedidoModel p = this.Bind();

                string mensaje = _DAVentas.CajasPedidoGuardar(p.numeroCaja, p.estatusCaja, p.usuario);

                return Response.AsJson(new Result<DataModel>()
                {
                    Value = mensaje == "" ? true : false,
                    Message = mensaje == "" ? "" : mensaje,
                    Data = new DataModel()
                    {
                        CodigoError = 0,
                        MensajeBitacora = "",
                        Data = mensaje
                    }
                }); ;
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result<DataModel>()
                {
                    Value = false,
                    Message = "Problemas al guardar caja",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }

        private object CajasPedidoActualizar()
        {
            try
            {
                CajasPedidoModel p = this.Bind();

                int NumeroCaja = _DAVentas.CajasPedidoActualizar(p.numeroCaja, p.estatusCaja, p.motivoCambio, p.usuario);

                return Response.AsJson(new Result<DataModel>()
                {
                    Value = true,
                    Message = "",
                    Data = new DataModel()
                    {
                        CodigoError = 0,
                        MensajeBitacora = "",
                        Data = NumeroCaja.ToString()
                    }
                }); ;
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result<DataModel>()
                {
                    Value = false,
                    Message = "Problemas al actualizar caja",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }

        private object ListadoCajasPedido()
        {
            try
            {
                var r = _DAVentas.ListadoCajasPedido();

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
                    Message = "Problemas al obtener cajas",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }

        private object CajasMovimientosCon()
        {
            var dat = this.Bind<FacturasCajasEnviosModel>();
            var result = _DAFacturas.CajasMovimientosCon(dat);
            return Response.AsJson(result);
        }
        
        private object CajasMovimientosGuiasCon()
        {
            var dat = this.Bind<FacturasCajasEnviosModel>();
            var result = _DAFacturas.CajasMovimientosGuiasCon(dat);
            return Response.AsJson(result);
        }

        private object CajasEstatusCon()
        {
            var dat = this.Bind<FacturasCajasEnviosModel>();
            var result = _DAFacturas.EstatusCajasCon(dat);
            return Response.AsJson(result);
        }
        
        private object CajasEstatusGuiasCon()
        {
            var dat = this.Bind<FacturasCajasEnviosModel>();
            var result = _DAFacturas.EstatusCajasGuiasCon(dat);
            return Response.AsJson(result);
        }

        private object ListadoCajasAsignadas()
        {
            try
            {
                CajasPedidoModel p = this.Bind();
                var r = _DAVentas.ListadoCajasAsignadas(p.idUbicacion);

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
                    Message = "Problemas al obtener cajas asignadas",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }

        private object ListadoCambiosCajasPedido()
        {
            try
            {
                CajasPedidoDetalleCambios p = this.Bind();
                var r = _DAVentas.ListadoCambiosCajasPedido(p.IdCaja);

                return Response.AsJson(new Result<DataModel>()
                {
                    Value = true,
                    Message = "",
                    Data = new DataModel()
                    {
                        CodigoError = 0,
                        MensajeBitacora = "",
                        Data = r.Data.Data
                    }
                });
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result<DataModel>()
                {
                    Value = false,
                    Message = "Problemas al obtener detalle cambios",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }

        private object CajasPedidoRecibir()
        {
            try
            {
                CajasPedidoModel p = this.Bind();
                var r = _DAVentas.CajasPedidoRecibir(p);
                return Response.AsJson(new Result<DataModel>()
                {
                    Value = r.Value,
                    Message = r.Message,
                    Data = new DataModel()
                    {
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
                    Message = "Problemas al recibir cajas de surtido",
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