using apiQuiroga.DA;
using apiQuiroga.Models;
using apiQuiroga.Models.Catalogos;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using System;
using System.Linq;
using WarmPack.Classes;

namespace apiQuiroga.Modules
{
    public class Catalogos : NancyModule
    {
        private readonly DACatalogos _DACatalogos = null;
        private readonly DAProductosSAI _DACatalogosSAI = null;

        public Catalogos() : base("/catalogo")
        {
            Before += ctx =>
            {
                if (!ctx.Request.Headers.Keys.Contains("api-key"))
                {
                    return HttpStatusCode.Unauthorized;
                }
                else
                {
                    var apikey = ctx.Request.Headers["api-key"].FirstOrDefault() ?? string.Empty;
                    if (apikey != Globales.ApiKey)
                    {
                        return HttpStatusCode.Unauthorized;
                    }
                    else
                    {
                        return null;
                    }
                }
            };
            //this.RequiresAuthentication();

            _DACatalogos = new DACatalogos();
            _DACatalogosSAI = new DAProductosSAI();

            Post("/proveedores", _ => Proveedores());
            Post("/proveedores/guardar", _ => ProveedoresGuardar());

            Post("/familias", _ => Familias());
            Post("/familias/guardar", _ => FamiliasGuardar());

            Post("/formulas", _ => Formulas());

            Post("/genericos", _ => Genericos());

            Post("/laboratorios", _ => Laboratorios());
            Post("/laboratorios/guardar", _ => LaboratoriosGuardar());

            Post("/lineas", _ => Lineas());

            Post("/medidas", _ => Medidas());
            Post("/medidas/guardar", _ => MedidasGuardar());

            Post("/presentaciones", _ => Presentaciones());
            Post("/presentaciones/guardar", _ => PresentacionesGuardar());

            Post("/productos", _ => Productos());
            //Get("/productos", _ => Productos());

            Post("/productos/scrolling", _ => ProductosScrolling());
            Post("/productos/guardar", _ => ProductosGuardar());
            Post("/productos/temporal", _ => ProductosTemporal());
            Post("/productos/temporal/guardar", _ => ProductosTempGuardar());
            Post("/productos/codigobarras", _ => ProductosCodigoBarras());
            Post("/productos/codigobarras/guardar", _ => ProductosCodigoBarraGuardar());
            Post("/productos/codigobarras/borrar", _ => ProductosCodigoBarraBorrar());
            Post("/productos/caducidades", _ => ProductosCaducidades());

            Post("/empleados", _ => Empleados());
            Post("/empleados/guardar", _ => EmpleadosGuardar());

            Post("/clientes", _ => Clientes());

            Post("/agentes", _ => Agentes());

            Post("/medicos", _ => Medicos());
            Post("/medicos/guardar", _ => MedicosGuardar());

            Post("/paqueterias", _ => Paqueterias());

            Post("/operaciones", _ => OperacionesSupervisadas());
            Post("/productos/sai", _ => ProductosSAI());
        }

        private object ProductosSAI()
        {   
            try
            {
                var r = _DACatalogosSAI.ProductosSAI();

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
                    Message = "Problemas en catalogo de PRODUCTOS SAI",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object Proveedores()
        {
            try
            {
                ProveedoresModel p = this.Bind();

                var r = _DACatalogos.Proveedores(p.clave_proveedor, p.tipo_proveedor);

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
                    Message = "Problemas en catalogo de proveedores",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object ProveedoresGuardar()
        {
            try
            {
                //var t = this.BindToken();
                ProveedoresModel p = this.Bind();

                var r = _DACatalogos.ProveedoresGuardar(p);

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
                    Message = ex.Message, // "Problemas al guardar proveedor",
                    Data = new DataModel()
                    {

                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }

                });
            }
        }



        private object Familias()
        {
            try
            {
                FamiliasModel p = this.Bind();

                var r = _DACatalogos.Familias(p.FamiliaID);

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
                    Message = "Problemas en catalogo de familias",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object FamiliasGuardar()
        {
            try
            {
                //var t = this.BindToken();
                FamiliasModel p = this.Bind();

                var r = _DACatalogos.FamiliasGuardar(p);

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
                    Message = "Problemas al guardar familia",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object Formulas()
        {
            try
            {
                FormulasModel p = this.Bind();

                var r = _DACatalogos.Formulas(p.FormulaID);

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
                    Message = "Problemas en catalogo de formulas",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object Genericos()
        {
            try
            {
                GenericosModel p = this.Bind();

                var r = _DACatalogos.Genericos(p.GenericoID);

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
                    Message = "Problemas en catalogo de genericos",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object Laboratorios()
        {
            try
            {
                LaboratoriosModel p = this.Bind();

                var r = _DACatalogos.Laboratorios();

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
                    Message = "Problemas en catalogo de laboratorios",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object LaboratoriosGuardar()
        {
            try
            {
                //var t = this.BindToken();
                LaboratoriosModel p = this.Bind();

                var r = _DACatalogos.LaboratoriosGuardar(p);

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
                    Message = "Problemas al guardar laboratorio",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object Lineas()
        {
            try
            {
                LineasModel p = this.Bind();

                var r = _DACatalogos.Lineas(p.LineaID);

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
                    Message = "Problemas en catalogo de lineas",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object Medidas()
        {
            try
            {
                MedidasModel p = this.Bind();

                var r = _DACatalogos.Medidas(p.MedidaID);

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
                    Message = "Problemas en catalogo de medidas",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object MedidasGuardar()
        {
            try
            {
                //var t = this.BindToken();
                MedidasModel p = this.Bind();

                var r = _DACatalogos.MedidasGuardar(p);

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
                    Message = "Problemas al guardar medida",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object Presentaciones()
        {
            try
            {
                PresentacionesModel p = this.Bind();

                var r = _DACatalogos.Presentaciones(p.PresentacionID);

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
                    Message = "Problemas en catalogo de presentaciones",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object PresentacionesGuardar()
        {
            try
            {
                //var t = this.BindToken();
                PresentacionesModel p = this.Bind();

                var r = _DACatalogos.PresentacionesGuardar(p);

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
                    Message = "Problemas al guardar presentacion",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object Productos()
        {
            try
            {
                ProductosModel p = this.Bind();

                var r = _DACatalogos.Productos(p.ProductoID);

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
                    Message = "Problemas en catalogo de productos",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object ProductosScrolling()
        {
            try
            {
                ProductosModel p = this.Bind();

                var r = _DACatalogos.ProductosScrolling(p.ProductoGrupoNumero);

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
                    Message = "Problemas en catalogo de productos",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object ProductosGuardar()
        {
            try
            {
                //var t = this.BindToken();
                ProductosModel p = this.Bind();

                var r = _DACatalogos.ProductosGuardar(p);

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
                    Message = "Problemas al guardar el producto",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object ProductosTemporal()
        {
            try
            {
                ProductosTempModel p = this.Bind();

                var r = _DACatalogos.ProductosTemp(p.productoEmpresaID, p.productoUsuarioAdicion);

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
                    Message = "Problemas en catalogo de productos temporales",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object ProductosTempGuardar()
        {
            try
            {
                //var t = this.BindToken();
                ProductosTempModel p = this.Bind();

                var r = _DACatalogos.ProductosTempGuardar(p);

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
                    Message = "Problemas al guardar el producto",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object ProductosCodigoBarras()
        {
            try
            {
                ProductosModel p = this.Bind();

                var r = _DACatalogos.ProductosCodigoBarras(p.ProductoID, p.ProductoCodigoBarras);

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
                    Message = "Problemas en catalogo de productos",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object ProductosCodigoBarraGuardar()
        {
            try
            {
                //var t = this.BindToken();
                ProductosModel p = this.Bind();

                var r = _DACatalogos.ProductosCodigosdeBarraGuardar(p);

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
                    Message = "Problemas al guardar el codigo de barras",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object ProductosCodigoBarraBorrar()
        {
            try
            {
                //var t = this.BindToken();
                ProductosModel p = this.Bind();

                var r = _DACatalogos.ProductosCodigosdeBarraBorrar(p);

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
                    Message = "Problemas al borrar el codigo de barras",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object ProductosCaducidades()
        {
            try
            {
                CaducidadesModel p = this.Bind();
                var r = _DACatalogos.Caducidades(p.CadClaveEmpresa, p.CadClaveArticulo);
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
                    Message = "Problemas en catalogo de caducidades",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object Empleados()
        {
            try
            {
                //var t = this.BindToken();
                EmpleadosModel p = this.Bind();

                var r = _DACatalogos.Empleados(p.EmpID);

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
                    Message = "Problemas en catalogo empleados de mostrador",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object EmpleadosGuardar()
        {
            try
            {
                //var t = this.BindToken();
                EmpleadosModel p = this.Bind();

                var r = _DACatalogos.EmpleadosGuardar(p);

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
                    Message = "Problemas al guardar empleado de mostrador",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object Medicos()
        {
            try
            {
                //var t = this.BindToken();
                MedicosModel p = this.Bind();

                var r = _DACatalogos.Medicos(p.MedID);

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
                    Message = "Problemas en catalogo medicos",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object MedicosGuardar()
        {
            try
            {
                //var t = this.BindToken();
                MedicosModel p = this.Bind();

                var r = _DACatalogos.MedicosGuardar(p);

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
                    Message = "Problemas al guardar medico",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object Paqueterias()
        {
            try
            {
                PaqueteriasModel p = this.Bind();

                var r = _DACatalogos.Paqueterias(p.PaqueteriaID);

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
                    Message = "Problemas en catalogo de paqueterias",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object Agentes()
        {
            try
            {
                //var t = this.BindToken();
                AgentesModel p = this.Bind();

                var r = _DACatalogos.Agentes(p.Empresa, p.CodigoAgente);

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
                    Message = "Problemas en catálogo de Agentes",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }
        private object Clientes()
        {
            try
            {
                ClientesModel p = this.Bind();

                var r = _DACatalogos.Clientes(p.ClienteID);

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
                    Message = "Problemas en catalogo de clientes",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                });
            }
        }

        private object OperacionesSupervisadas()
        {
            try
            {
                //var t = this.BindToken();
                OperacionesModel p = this.Bind();

                var r = _DACatalogos.OperacionesSupervisadas(p.OperacionEmpresaID, p.OperacionURL);

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
                    Message = "Problemas al obtener el movimiento",
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