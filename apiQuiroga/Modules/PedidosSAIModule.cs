using apiQuiroga.DA;
using apiQuiroga.Models;
using apiQuiroga.Models.Pedidos;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WarmPack.Classes;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

namespace apiQuiroga.Modules
{
    public class PedidosSAIModule : NancyModule
    {
        private readonly DAPedidosSAI _DAPedidosSAI = null;
        XmlDocument xmldocumento = new XmlDocument();

        public PedidosSAIModule() : base("/PedidosSAI")
        {
            _DAPedidosSAI = new DAPedidosSAI();
            Post("/pedidoSAIGuardar", _ => PedidoSAIGuardar());

        }

        private object PedidoSAIGuardar()
        {
            try
            {
                PedidoModel p = this.Bind();

                var r = _DAPedidosSAI.PedidoSAIGuardar(p.idPedidoEnc, p.idCliente, p.cve_Sucursal, p.cveAlmacen);

                return Response.AsJson(new Result<DataModel>()
                {
                    Value = r.Value,
                    Message = r.Message,
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = r.Message,
                        Data = ""
                    }
                });
            }
            catch (Exception ex)
            {
                return Response.AsJson(new Result<DataModel>()
                {
                    Value = false,
                    Message = "Problemas al guardar pedido SAI",
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