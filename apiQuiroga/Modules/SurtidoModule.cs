using apiQuiroga.DA;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WarmPack.Classes;

namespace apiQuiroga.Modules
{
    public class SurtidoModule : NancyModule
    {
        private readonly DASurtido _DASurtido = null;

        public SurtidoModule() : base("/surtido")
        {
            Post("/pedidos", _ => PostSurtidoPedidosCon());

            _DASurtido = new DASurtido();
        }

        private object PostSurtidoPedidosCon()
        {
            try
            {
                var p = this.BindModel();

                int iD_Cedis = p.iD_Cedis;
                int iD_Cliente = p.iD_Cliente;
                int iD_Agente = p.iD_Agente;

                var r = _DASurtido.PedidosCon(iD_Cedis, iD_Agente, iD_Cliente);

                return Response.AsJson(r);
            }
            catch(Exception ex)
            {
                return Response.AsJson(new Result(ex));

            }
        }
    }
}