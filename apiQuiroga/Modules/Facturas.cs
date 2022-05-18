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
using System.Xml;
using System.IO;
using System.Xml.Serialization;

namespace apiQuiroga.Modules
{
    public class Facturas : NancyModule
    {
        private readonly DAFacturas _DAFacturas = null;
        XmlDocument xmldocumento = new XmlDocument();

        public Facturas() : base("/Facturas")
        {
            _DAFacturas = new DAFacturas();
            Post("/facturacon", _ => FacturaCon());
            Post("/facturadetalle", _ => FacturaDetalleCon());
            Post("/facturaTimbrado", _ => FacturaTimbrado());

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

        private XmlDocument FacturaTimbrado ()
        {
            FacturaModel p = this.Bind();
            wsServifact.Comprobante comprobante = CargarComprobanteV33(p);
            //wsServifact.Comprobante comprobante = CargarComprobantePagos(RFC, Serie, Folio);
            ////wsServifact.Comprobante comprobante = CargarComprobanteComercioExterior11(RFC, Serie, Folio);
            //wsServifact.Comprobante comprobante = CargarComprobanteNomina12(RFC, Serie, Folio);
            ////wsServifact.Comprobante comprobante = CargarComprobante(RFC, Serie, Folio);
            string Usuario = "demoCFDI";
            string Password = "CPKRTQ6FTD43)~";
            string respuesta;
            respuesta = GenerarCFD(comprobante, Usuario, Password);
            //try
            //{
            //    xmldocumento.LoadXml(respuesta);
            //    return new Result<XmlDocument>(true, "", xmldocumento);
            //}
            //catch(Exception ex)
            //{
            //    throw ex;
            //}

            return xmldocumento;

        }

        private string GenerarCFD(wsServifact.Comprobante Comprobante, string Usuario, string Password)
        {
            wsServifact.Servicios Servicio = new wsServifact.Servicios();
            return Servicio.TimbrarComprobante(ref Comprobante, Usuario, Password);
        }

        private wsServifact.Comprobante CargarComprobanteV33(FacturaModel Factura)
        {
            wsServifact.Comprobante comprobante = new apiQuiroga.wsServifact.Comprobante(); //Elemento Comprobante
            wsServifact.Emisor emisor = new apiQuiroga.wsServifact.Emisor(); // Elemento Emisor
            wsServifact.Receptor receptor = new apiQuiroga.wsServifact.Receptor(); // Elemento Receptor
            List<wsServifact.Concepto> conceptos = new List<wsServifact.Concepto>(); //Listado de Conceptos
            wsServifact.CFDIRelacionados cfdisrelacionados = new apiQuiroga.wsServifact.CFDIRelacionados();//Elemento CFDIRelacionados


            //DATOS DEL COMPROBANTE
            comprobante.Version = apiQuiroga.wsServifact.Versiones.Ver_3;
            comprobante.SerieFolio = Factura.Serie; //Parte inicial o final que acompaña al  número de la factura y puede ser opcional
            comprobante.Nume_Folio = Factura.Folio; //Número consecutivo asignado al comprobante para el control interno
            comprobante.Fech_Comprobante = DateTime.Now; //Fecha de emisión del CFDI 
            comprobante.ComprobanteClase = wsServifact.Comprobante_Clase.Factura; //Clase de comprobante fiscal definidos por el SAT
            comprobante.Moneda = wsServifact.Monedas.MXN;//Medida de cambio que se emplea en la transacion comercial
            comprobante.MetodoPago = "PUE"; //Metodo por el cual se realizara el pago del comprobante
            comprobante.LugarExpedicion = "80000"; //Nombre del lugar de expedicion ;
            comprobante.CondicionesPago = "CONTADO";  //Es la condición establecida en que deberá ser liquidada la factura "CONTADO" "CREDITO"
            comprobante.FormaPago = "01"; //Forma en que debera ser liquidada de la factura "EN UNA SOLA EXHIBICION" "EN PARCIALIDADES"
            //comprobante.TipoCambio = 18.5813; //Valor que tiene la moneda que se esta utilizando en la factura en relacion al peso mexicano Ejemplo: 1 para peso mexicano, 13.02 para dolares
            comprobante.TipoComprobante = "I";//Clave del efecto del comprobante fiscal para el contribuyente emisor

            /*INFORMACION DE LOS COMPROBANTES RELACIONADOS*/
            //cfdisrelacionados.TipoRelacion = "04";//Atributo que indica la clave de la relación que existe entre éste que se esta generando y el o los CFDI previos.
            //List<wsServifact.UUIDRelacionado> listaUUIDs = new List<apiQuiroga.wsServifact.UUIDRelacionado>();//listado de UUIDS Relacionados
            //wsServifact.UUIDRelacionado uuid = new apiQuiroga.wsServifact.UUIDRelacionado();
            //uuid.UUID = "4D2A6D62-AC26-4758-9B03-1080654913D6";//folio fiscal (UUID) de un CFDI relacionado con el presente comprobante 
            //listaUUIDs.Add(uuid);
            //cfdisrelacionados.UUIDsRelacionados = listaUUIDs.ToArray();
            //comprobante.CFDIRelacionados = cfdisrelacionados;

            /*INFORMACION DE QUIEN EXPIDE LA FACTURA*/
            emisor.RFC = Factura.Rfc; //RFC de la empresa o persona que va a emitir el comprobante
            emisor.Nomb_Comercial = "XOCHILT CASAS CHAVEZ"; //Nombre con el que se conoce a la empresa o persona que emite el comprobante
            emisor.Nomb_RazonSocial = "XOCHILT CASAS CHAVEZ"; //Nombre con el que esta registrado ante hacienda quien emite el comprobante
            emisor.RegimenFiscal = "612"; //Clave del régimen del contribuyente emisor al que aplicará el efecto fiscal de este comprobante     



            /*DATOS DE LA EMPRESA A QUIEN SE EXPIDE LA FACTURA*/
            receptor.RFC = "XAXX010101000"; //RFC de la empresa o persona a quien se le va a emitir el comprobante
            receptor.Nomb_RazonSocial = "PUBLICO EN GENERAL"; //Nombre con el que esta registrado ante hacienda,  quien se le va a emitir el comprobante
            receptor.Nomb_Comercial = "PUBLICO EN GENERAL"; //Nombre con el que se le conoce a la empresa o persona que se le va a emitir el comprobante
            //receptor.Domi_Pais = "MEX";//Clave del país de residencia para efectos fiscales del receptor del comprobante
            receptor.UsoCFDI = "P01";//Clave del uso que dará a esta factura el receptor del CFDI

            /*CONCEPTOS*/
            wsServifact.Concepto concepto = new wsServifact.Concepto();
            concepto.ClaveProductoServicio = "80121704";//Clave del producto o del servicio amparado por el presente concepto.
            concepto.ClaveUnidad = "E48";//Clave de unidad de medida estandarizada aplicable para la cantidad expresada en el concepto.
            concepto.Cantidad = 150; //Cantidad de bienes o servicios del tipo particular definido por el presente concepto
            concepto.Unidad = "SERVICIO"; //Unidad de medida propia de la operación del emisor, aplicable para la cantidad expresada en el concepto.
            concepto.NoIdentificacion = "0001"; // Número de serie del bien o identificador del servicio amparado por el presente concepto
            concepto.Descripcion = "HONORARIOS POR ELABORACION DE PROTOCOLIZACION DE ACTA"; //Descripción del bien o servicio cubierto por el presente concepto
            concepto.PrecioUnitario = 6.4655; //Precio unitario del bien o servicio cubierto por el presente concepto
            //concepto.Descuento = 5.00;//Importe de los descuentos aplicables al concepto.

            /*IMPUESTOS POR CONCEPTO*/
            /*EJEMPLO IMPUESTO FEDERAL TRASLADADO */
            List<wsServifact.Impuesto> listaImpuestosConcepto = new List<apiQuiroga.wsServifact.Impuesto>();
            wsServifact.Impuesto Impuesto = new apiQuiroga.wsServifact.Impuesto();
            Impuesto.Base = 969.8250;//Base para el cálculo del impuesto
            Impuesto.ClaveSATImpuesto = "002";//Clave del tipo de impuesto trasladado aplicable al concepto
            Impuesto.TipoFactor = apiQuiroga.wsServifact.TiposFactor.Exento;//Clave del tipo de factor que se aplica a la base del impuesto
            Impuesto.Tasa = 0.0; //tasa del impuesto que se retiene o traslada por cada concepto amparado en el comprobante
            Impuesto.Nombre = "IVA"; //Especifica el nombre del impuesto (IVA,IEPS,ISR)
            Impuesto.Tipo = 1; //Especifica la naturaleza del impuesto (1 Trasladado, 2 Retenido)
            Impuesto.Federal = 1; //Especifica si el impuesto es Federal 1 o Local 2
            Impuesto.Importe = 0.00; //Importe calculado del impuesto
            listaImpuestosConcepto.Add(Impuesto);

            ///////*EJEMPLO IMPUESTO FEDERAL RETENIDO */
            //Impuesto = new apiQuiroga.wsServifact.Impuesto();
            //Impuesto.Base = 11130.7814;
            //Impuesto.ClaveSATImpuesto = "001";
            //Impuesto.TipoFactor = apiQuiroga.wsServifact.TiposFactor.Tasa;
            //Impuesto.Tasa = 0.100000;
            //Impuesto.Nombre = "ISR";
            //Impuesto.Tipo = 2;
            //Impuesto.Federal = 1;
            //Impuesto.Importe = 1113.0781;
            //listaImpuestosConcepto.Add(Impuesto);

            //Impuesto = new apiQuiroga.wsServifact.Impuesto();
            //Impuesto.Base = 11130.7814;
            //Impuesto.ClaveSATImpuesto = "002";
            //Impuesto.TipoFactor = apiQuiroga.wsServifact.TiposFactor.Tasa;
            //Impuesto.Tasa = 0.106670;
            //Impuesto.Nombre = "IVA";
            //Impuesto.Tipo = 2;
            //Impuesto.Federal = 1;
            //Impuesto.Importe = 1187.3205;
            //listaImpuestosConcepto.Add(Impuesto);
            ///////*EJEMPLO IMPUESTO LOCAL */
            //Impuesto = new apiQuiroga.wsServifact.Impuesto();
            //Impuesto.Base = 1.00;
            //Impuesto.ClaveSATImpuesto = "";
            //Impuesto.TipoFactor = apiQuiroga.wsServifact.TiposFactor.Tasa;
            //Impuesto.Tasa = 0.01;
            //Impuesto.Nombre = "CEDULAR";
            //Impuesto.Tipo = 2;
            //Impuesto.Federal = 2;
            //Impuesto.Importe = 111.31;
            //listaImpuestosConcepto.Add(Impuesto);

            concepto.Impuestos = listaImpuestosConcepto.ToArray();
            conceptos.Add(concepto);

            /*CONCEPTO 2*/
            //wsServifact.Concepto concepto2 = new wsServifact.Concepto();
            //concepto2.ClaveProductoServicio = "84111506";//Clave del producto o del servicio amparado por el presente concepto.
            //concepto2.ClaveUnidad = "ACT";//Clave de unidad de medida estandarizada aplicable para la cantidad expresada en el concepto.
            //concepto2.Cantidad = 850;//Cantidad de bienes o servicios del tipo particular definido por el presente concepto
            //concepto2.Unidad = "NO APLICA"; //Unidad de medida propia de la operación del emisor, aplicable para la cantidad expresada en el concepto.
            ////concepto2.NoIdentificacion = "0001"; // Número de serie del bien o identificador del servicio amparado por el presente concepto
            //concepto2.Descripcion = "CONCEPTO 2"; //Descripción del bien o servicio cubierto por el presente concepto
            //concepto2.PrecioUnitario = 9.4828; //Precio unitario del bien o servicio cubierto por el presente concepto
            ////concepto2.Descuento = 5.00;//Importe de los descuentos aplicables al concepto.

            //listaImpuestosConcepto = new List<apiQuiroga.wsServifact.Impuesto>();
            //Impuesto = new apiQuiroga.wsServifact.Impuesto();
            //Impuesto.Base = 8060.3800;//Base para el cálculo del impuesto
            //Impuesto.ClaveSATImpuesto = "002";//Clave del tipo de impuesto trasladado aplicable al concepto
            //Impuesto.TipoFactor = apiQuiroga.wsServifact.TiposFactor.Tasa;//Clave del tipo de factor que se aplica a la base del impuesto
            //Impuesto.Tasa = 0.16; //tasa del impuesto que se retiene o traslada por cada concepto amparado en el comprobante
            //Impuesto.Nombre = "IVA"; //Especifica el nombre del impuesto (IVA,IEPS,ISR)
            //Impuesto.Tipo = 1; //Especifica la naturaleza del impuesto (1 Trasladado, 2 Retenido)
            //Impuesto.Federal = 1; //Especifica si el impuesto es Federal 1 o Local 2
            //Impuesto.Importe = 1289.6608; //Importe calculado del impuesto
            //listaImpuestosConcepto.Add(Impuesto);

            //concepto2.Impuestos = listaImpuestosConcepto.ToArray();
            //conceptos.Add(concepto2);

            comprobante.Emisor = emisor;
            comprobante.Receptor = receptor;
            comprobante.Conceptos = conceptos.ToArray();

            return comprobante;
        }

    }

    
}