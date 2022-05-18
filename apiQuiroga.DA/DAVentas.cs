using apiQuiroga.Models;
using apiQuiroga.Models.Catalogos;
using apiQuiroga.Models.Ventas;
using apiQuiroga.Models.CajasPedido;
using apiQuiroga.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;
using WarmPack.Extensions;
using System.Data.SqlClient;

namespace apiQuiroga.DA
{
    public class DAVentas
    {
        private readonly Conexion _conexion = null;
        private readonly Conexion _conexion2 = null;

        public DAVentas()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
            _conexion2 = new Conexion(ConexionType.MSSQLServer, Globales.ConexionSecundaria);
        }

        public Result<DataModel> CuentasPCobrarCon(int IDEmpresa, int Factura, int IDCliente, string FechaInicio, string FechaFin, string Estatus)
        {
            var parametros = new ConexionParameters();
            try
            {
                parametros.Add("@pIDEmpresa", ConexionDbType.Int, IDEmpresa);
                parametros.Add("@pFactura", ConexionDbType.Int, Factura);
                parametros.Add("@pIDCliente", ConexionDbType.Int, IDCliente);
                parametros.Add("@pFechaInicio", ConexionDbType.VarChar, FechaInicio);
                parametros.Add("@pFechaFin", ConexionDbType.VarChar, FechaFin);
                parametros.Add("@pEstatus", ConexionDbType.VarChar, Estatus);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);
                parametros.Add("@pCodError", ConexionDbType.Int, System.Data.ParameterDirection.Output);

                var r = _conexion2.ExecuteWithResults<CuentasPCobrarModel    >("QW_procCuentasPCobrarCon", parametros);

                return new Result<DataModel>()
                {
                    Value = parametros.Value("@pResultado").ToBoolean(),
                    Message = parametros.Value("@pMsg").ToString(),
                    Data = new DataModel()
                    {
                        CodigoError = parametros.Value("@pCodError").ToInt32(),
                        MensajeBitacora = parametros.Value("@pMsg").ToString(),
                        Data = r.Data
                    }
                };
            }
            catch (Exception ex)
            {
                return new Result<DataModel>()
                {
                    Value = false,
                    Message = "Problemas al obtener cuentas por cobrar",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                };
            }
        }

        public string CajasPedidoGuardar(int NumeroCaja, string EstatusCaja, string Usuario)
        {
            string Resultado = "";
            SqlConnection con = new SqlConnection(Globales.ConexionSecundaria);
            con.Open();
            try
            {

                SqlCommand cmd = new SqlCommand("QW_procCajasGuardar", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NumeroCaja", NumeroCaja);
                cmd.Parameters.AddWithValue("@EstatusCaja", EstatusCaja);
                cmd.Parameters.AddWithValue("@Usuario", Usuario);
                cmd.Parameters.Add("@Msg", System.Data.SqlDbType.VarChar, 300);
                cmd.Parameters["@Msg"].Direction = System.Data.ParameterDirection.Output;

                SqlDataReader dr = cmd.ExecuteReader();

                if(cmd.Parameters["@Msg"].Value.ToString() != "")
                {
                    Resultado = cmd.Parameters["@Msg"].Value.ToString();
                }


            }
            catch (Exception Error)
            {
                con.Close();
                throw Error;
            }

            con.Close();
            return Resultado;
        }

        public int CajasPedidoActualizar(int NumeroCaja, string EstatusCaja, string MotivoCambio, string Usuario)
        {
            int Resultado = 0;
            SqlConnection con = new SqlConnection(Globales.ConexionSecundaria);
            con.Open();
            try
            {

                SqlCommand cmd = new SqlCommand("QW_procCajasActualizar", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NumeroCaja", NumeroCaja);
                cmd.Parameters.AddWithValue("@EstatusCaja", EstatusCaja);
                cmd.Parameters.AddWithValue("@MotivoCambio", MotivoCambio);
                cmd.Parameters.AddWithValue("@Usuario", Usuario);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Resultado = int.Parse(dr["NumeroCaja"].ToString());
                    }
                }

            }
            catch (Exception Error)
            {
                con.Close();
                throw Error;
            }

            con.Close();
            return Resultado;
        }

        public Result<DataModel> ListadoCajasPedido()
        {
            var parametros = new ConexionParameters();
            try
            {
                var r = _conexion2.ExecuteWithResults<CajasPedidoModel>("QW_procListadoCajasPedido");

                return new Result<DataModel>()
                {
                    Value = true,
                    Message = "",
                    Data = new DataModel()
                    {
                        CodigoError = 0,
                        MensajeBitacora = "",
                        Data = r.Data
                    }
                };
            }
            catch (Exception ex)
            {
                return new Result<DataModel>()
                {
                    Value = false,
                    Message = "Problemas al obtener listado de cajas para pedido",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                };
            }
        }

        public Result<DataModel> ListadoCajasAsignadas(int IdUbicacion)
        {
            var parametros = new ConexionParameters();
            try
            {
                parametros.Add("@idUbicacion", ConexionDbType.Int, IdUbicacion);
                var r = _conexion2.ExecuteWithResults<CajasPedidoMovimientosModel>("QW_procListadoCajasAsignadas", parametros);

                return new Result<DataModel>()
                {
                    Value = true,
                    Message = "",
                    Data = new DataModel()
                    {
                        CodigoError = 0,
                        MensajeBitacora = "",
                        Data = r.Data
                    }
                };
            }
            catch (Exception ex)
            {
                return new Result<DataModel>()
                {
                    Value = false,
                    Message = "Problemas al obtener cajas asignadas",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                };
            }
        }
       
        public Result<DataModel> ListadoCambiosCajasPedido(int IdCaja)
        {
            var parametros = new ConexionParameters();
            try
            {
                parametros.Add("@idCaja", ConexionDbType.Int, IdCaja);
                var r = _conexion2.ExecuteWithResults<CajasPedidoDetalleCambios>("QW_procListadoCambiosCaja", parametros);

                return new Result<DataModel>()
                {
                    Value = true,
                    Message = "",
                    Data = new DataModel()
                    {
                        CodigoError = 0,
                        MensajeBitacora = "",
                        Data = r.Data
                    }
                };
            }
            catch (Exception ex)
            {
                return new Result<DataModel>()
                {
                    Value = false,
                    Message = "Problemas al obtener cajas asignadas",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                };
            }
        }

        public Result<DataModel> CajasPedidoRecibir(CajasPedidoModel CajasPedido)
        {
            var parametros = new ConexionParameters();
            var xml = CajasPedido.ToXml("root");
            try
            {
                parametros.Add("@CajasXML", ConexionDbType.Xml, xml);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);

                var r = _conexion2.Execute("QW_procCajasRecibir", parametros);

                return new Result<DataModel>()
                {
                    Value = parametros.Value("@pResultado").ToBoolean(),
                    Message = parametros.Value("@pMsg").ToString(),
                    Data = new DataModel()
                    {
                        MensajeBitacora = parametros.Value("@pMsg").ToString()
                    }
                };
            }
            catch (Exception ex)
            {
                return new Result<DataModel>()
                {
                    Value = false,
                    Message = "Problemas al recibir cajas de surtido",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                };
            }
        }
        //public Result<DataModel> FacturaDetalleCon(int IDEmpresa, int IDPedido)
        //{
        //    var parametros = new ConexionParameters();
        //    try
        //    {
        //        parametros.Add("@pIDEmpresa", ConexionDbType.Int, IDEmpresa);
        //        parametros.Add("@pIDFactura", ConexionDbType.VarChar, IDPedido);
        //        parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
        //        parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);
        //        parametros.Add("@pCodError", ConexionDbType.Int, System.Data.ParameterDirection.Output);

        //        var r = _conexion2.ExecuteWithResults<FacturaDetalleModel>("QW_procFacturaDetalleCon", parametros);

        //        return new Result<DataModel>()
        //        {
        //            Value = parametros.Value("@pResultado").ToBoolean(),
        //            Message = parametros.Value("@pMsg").ToString(),
        //            Data = new DataModel()
        //            {
        //                CodigoError = parametros.Value("@pCodError").ToInt32(),
        //                MensajeBitacora = parametros.Value("@pMsg").ToString(),
        //                Data = r.Data
        //            }
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Result<DataModel>()
        //        {
        //            Value = false,
        //            Message = "Problemas al obtener detalle factura",
        //            Data = new DataModel()
        //            {
        //                CodigoError = 101,
        //                MensajeBitacora = ex.Message,
        //                Data = ""
        //            }
        //        };
        //    }
        //}
    }
}
