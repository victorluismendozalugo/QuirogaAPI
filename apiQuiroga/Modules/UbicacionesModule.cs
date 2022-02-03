using apiQuiroga.DA;
using apiQuiroga.Models.Catalogos;
using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WarmPack.Classes;

namespace apiQuiroga.Modules
{
    public class UbicacionesModule : NancyModule
    {
        private readonly DAUbicaciones _DAUbicaciones = null;

        public UbicacionesModule() : base("/ubicaciones")
        {
            Post("/", _ => PostUbicacion());
            Post("/buscar", _ => GetUbicacion());
            Post("/eliminar", _ => DeleteUbicacion());
            Post("/agregar-articulo", _ => PostAgregarArticulo());
            Post("/eliminar-articulo", _ => PostEliminarArticulo());
            Post("/productos", _ => PostProductos());

            Get("/almacenes", _ => GetAlmacenes());
            Post("/almacenes", _ => PostAlmacenes());
            Post("/almacenes/eliminar", _ => DeleteAlmacenes());

            Get("/pasillos", _ => GetPasillos());
            Post("/pasillos", _ => PostPasillos());
            Post("/pasillos/eliminar", _ => DeletePasillos());

            Get("/racks", _ => GetRacks());
            Post("/racks", _ => PostRacks());
            Post("/racks/eliminar", _ => DeleteRacks());

            Get("/niveles", _ => GetNiveles());
            Post("/niveles", _ => PostNiveles());
            Post("/niveles/eliminar", _ => DeleteNiveles());

            Get("/secciones", _ => GetSecciones());
            Post("/secciones", _ => PostSecciones());
            Post("/secciones/eliminar", _ => DeleteSecciones());

            _DAUbicaciones = new DAUbicaciones();
        }

        private object DeleteAlmacenes()
        {
            try
            {
                var p = this.BindModel();
                int idAlmacen = p.iD_Almacen;

                var r = _DAUbicaciones.AlmacenesEliminar(idAlmacen);

                return Response.AsJson(r);
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result(ex));
            }
        }

        private object PostAlmacenes()
        {
            try
            {
                var p = this.Bind<UbicacionAlmacenModel>();

                var r = _DAUbicaciones.AlmacenesCrear(p);

                return Response.AsJson(r);
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result(ex));
            }
        }

        private object PostEliminarArticulo()
        {
            try
            {
                var p = this.BindModel();
                int id_CatUbicacionesDet = p.iD_CatUbicacionesDet;

                var r = _DAUbicaciones.UbicacionDetEliminar(id_CatUbicacionesDet);

                return Response.AsJson(r);
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result(ex));
            }
        }

        private object PostProductos()
        {
            try
            {
                var p = this.BindModel();
                int iD_CatUbicacionesEnc = p.iD_CatUbicacionesEnc;

                var r = _DAUbicaciones.UbicacionProductosCon(iD_CatUbicacionesEnc);

                return Response.AsJson(r);
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result(ex));
            }
        }

        private object PostAgregarArticulo()
        {
            try
            {
                var p = this.Bind<UbicacionDetModel>();

                var r = _DAUbicaciones.UbicacionDetGuardar(p);

                return Response.AsJson(r);
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result(ex));
            }
        }

        private object DeleteSecciones()
        {
            try
            {
                var p = this.BindModel();
                int seccion = p.seccion;

                var r = _DAUbicaciones.SeccionesEliminar(seccion);

                return Response.AsJson(r);
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result(ex));
            }
        }

        private object DeleteNiveles()
        {
            try
            {
                var p = this.BindModel();
                int nivel = p.nivel;

                var r = _DAUbicaciones.NivelesEliminar(nivel);

                return Response.AsJson(r);
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result(ex));
            }
        }

        private object DeleteRacks()
        {
            try
            {
                var p = this.BindModel();
                int rack = p.rack;

                var r = _DAUbicaciones.RacksEliminar(rack);

                return Response.AsJson(r);
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result(ex));
            }
        }

        private object DeletePasillos()
        {
            try
            {
                var p = this.BindModel();
                string pasillo = p.pasillo;

                var r = _DAUbicaciones.PasilloEliminar(pasillo);

                return Response.AsJson(r);
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result(ex));
            }
        }

        private object DeleteUbicacion()
        {
            try
            {
                var p = this.Bind<UbicacionModel>();

                var r = _DAUbicaciones.UbicacionEliminar(p);

                return Response.AsJson(r);
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result(ex));
            }
        }

        private object GetUbicacion()
        {
            try
            {
                var p = this.BindModel();
                string pasillo = p.pasillo;
                int empresa = p.empresa;
                int almacen = p.almacen;

                var r = _DAUbicaciones.UbicacionesCon(empresa, almacen, pasillo);

                return Response.AsJson(r);
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result(ex));
            }
        }

        private object PostUbicacion()
        {
            try
            {
                var p = this.Bind<UbicacionModel>();                

                var r = _DAUbicaciones.UbicacionCrear(p);

                return Response.AsJson(r);
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result(ex));
            }
        }

        private object PostNiveles()
        {
            try
            {
                var p = this.BindModel();
                int nivel = p.nivel;

                var r = _DAUbicaciones.NivelesCrear(nivel);

                return Response.AsJson(r);
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result(ex));
            }
        }

        private object GetNiveles()
        {
            try
            {
                var r = _DAUbicaciones.NivelesCon();

                return Response.AsJson(r);
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result(ex));
            }
        }

        private object PostSecciones()
        {
            try
            {
                var p = this.BindModel();
                int seccion = p.seccion;

                var r = _DAUbicaciones.SeccionesCrear(seccion);

                return Response.AsJson(r);
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result(ex));
            }
        }

        private object GetSecciones()
        {
            try
            {
                var r = _DAUbicaciones.SeccionesCon();

                return Response.AsJson(r);
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result(ex));
            }
        }

        private object PostRacks()
        {
            try
            {
                var p = this.BindModel();
                int rack = p.rack;

                var r = _DAUbicaciones.RacksCrear(rack);

                return Response.AsJson(r);
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result(ex));
            }
        }

        private object GetRacks()
        {
            try
            {
                var r = _DAUbicaciones.RacksCon();

                return Response.AsJson(r);
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result(ex));
            }
        }

        private object PostPasillos()
        {
            try
            {
                var p = this.BindModel();
                string pasillo = p.pasillo;

                var r = _DAUbicaciones.PasilloCrear(pasillo);

                return Response.AsJson(r);
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result(ex));
            }
        }

        private object GetPasillos()
        {
            try
            {
                var r = _DAUbicaciones.PasillosCon();

                return Response.AsJson(r);
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result(ex));
            }
        }

        private object GetAlmacenes()
        {
            try
            {
                var r = _DAUbicaciones.AlmacenesCon();

                return Response.AsJson(r);
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result(ex));
            }
            
        }
    }
}