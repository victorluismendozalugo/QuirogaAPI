namespace apiQuiroga.Models.Catalogos
{
    public class ClientesModel
    {
        public int ClienteID { get; set; }
        public string ClienteRazonSocial { get; set; }
        public string ClienteNombre { get; set; }
        public string ClienteApellidoP { get; set; }
        public string ClienteApellidoM { get; set; }
        public string ClienteCurp { get; set; }
        public string ClienteRfc { get; set; }
        public string ClienteTelefono { get; set; }
        public string ClienteFechaRegistro { get; set; }
        public string ClienteFechaModificacion { get; set; }
        public int ClienteGrupoClientes { get; set; }
        public string ClienteEmail { get; set; }        
        public string ClienteAdicionadoPor { get; set; }
        public string ClienteModificadoPor { get; set; }
        public int ClienteCveEstado { get; set; }
        public string ClienteEstadoDesc { get; set; }
        public int ClienteCveMunicipio { get; set; }
        public string ClienteMunicipioDesc { get; set; }     
        public int ClienteDiaVisita { get; set; }
        public int ClienteIdAgente{ get; set; }
        public decimal ClienteSaldo { get; set; }
        public string ClienteEstatus { get; set; }    
        public string Usuario { get; set; }
        public ClienteDatosFiscalesModel DatosFiscales { get; set; }

    }

    public class ClienteDatosFiscalesModel
    {
        public int ClienteID { get; set; }
        public decimal LimiteCredito { get; set; }
        public int PlazoPago { get; set; }
        public string CuentaContable { get; set; }
        public decimal LimitePorFactura { get; set; }
        public int IdBanco { get; set; }
        public string DescripcionBanco { get; set; }
        public string FormaDePago { get; set; }
        public string MetodoPago { get; set; }
        public string UsoCfdi { get; set; }
        public string TipoPersona { get; set; }
        public string ResponsableFiscal { get; set; }
        public string EmailResponsable { get; set; }

    }
}
