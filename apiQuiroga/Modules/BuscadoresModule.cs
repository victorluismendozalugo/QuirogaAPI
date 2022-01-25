using apiQuiroga.DA;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WarmPack.Classes;

namespace apiQuiroga.Modules
{
    public class BuscadoresModule : NancyModule
    {
        private readonly DABuscadores _DABuscadores = null;

        public BuscadoresModule(): base("/buscadores")
        {
            Post("/productos", _ => PostProductos());

            _DABuscadores = new DABuscadores();
        }

        private object PostProductos()
        {
            try
            {
                var p = this.BindModel();
                string buscar = p.buscar;

                var r = _DABuscadores.Productos(buscar);

                return Response.AsJson(r);
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result(ex));
            }
        }
    }
}