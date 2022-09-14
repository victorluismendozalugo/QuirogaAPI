<<<<<<< HEAD
﻿using apiQuiroga.Models;
using apiQuiroga.Models.Catalogos;
using apiQuiroga.Models.Facturas;
using apiQuiroga.Models.Usuario;
using System;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;
using WarmPack.Extensions;

namespace apiQuiroga.DA
{
    public class DAPedidosSAI
    {
        private readonly Conexion _conexion = null;
        private readonly Conexion _conexion2 = null;

        public DAPedidosSAI()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
            _conexion2 = new Conexion(ConexionType.MSSQLServer, Globales.ConexionSecundaria);
        }

        public Result PedidoSAIGuardar(int idPedidoEnc, int idCliente, string Sucursal, string CveAlmacen)
        {
            var parametros = new ConexionParameters();
            OleDbConnection connLocal;
            OleDbConnection connRemota;
            OleDbCommand cmd;
            //OleDbTransaction transaction;
            string sql;
            int No_Ped = 0;
            int SAIAgente = 0;
            //DataTable dtPedidoDet = new DataTable();
            OleDbDataReader dr;

            try
            {  //Abrir conexion a SAI   y DFQ             
                               
                connLocal = new OleDbConnection("Provider=VFPOLEDB;Data Source=\\\\192.168.0.92\\c$\\vsai\\EMPRESAS\\prueba;");                
                connRemota = new OleDbConnection("Provider=sqloledb;Data Source=192.168.0.52;Initial Catalog=DFQ;User Id=sa;Password=3lDorado;");

                connLocal.Open();
                connRemota.Open();
            }
            catch (Exception exConexiones)
            {
                return new Result()
                {
                    Value = false,
                    Message = "Problema al abrir conexión a base de datos",
                    Data = new DataModel()
                    {
                        CodigoError = 505,
                        MensajeBitacora = exConexiones.Message,
                        Data = ""
                    }
                };
            }

            try
            { //Guardar pedido en SAI                
                
                string Año = DateTime.Now.Year.ToString();
                string Mes = "0" + DateTime.Now.Month.ToString();
                Mes = Mes.Substring((Mes.Length - 2), 2);
                string fecha = DateTime.Now.ToString("MM/dd/yyyy");

                sql = "SELECT SAIAgente from CatClientes c inner join CatAgentes a on c.id_agente = a.id_agente where c.id_cliente = " + idCliente.ToString();
                cmd = new OleDbCommand(sql, connRemota);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    SAIAgente = Convert.ToInt32(dr["SAIAgente"]);
                }

                sql = "SELECT max(no_ped) from FolPedVE where cve_suc = '" + Sucursal + "'";
                sql += " and AÑO = '" + Año + "'";
                cmd = new OleDbCommand(sql, connLocal);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    No_Ped = Convert.ToInt32(dr[0]);
                }

                //string lugar;
                string supervisor = "0";

                if (Sucursal == "CUL")
                {
                    //lugar = "1-1";
                    supervisor = "100";
                }
                else if (Sucursal == "TIJ")
                {
                    //lugar = "2-1";
                    supervisor = "300";
                }
                else
                {
                    //lugar = "3-1";
                    supervisor = "200";
                }

                sql = "insert into FolPedVE (cve_suc, no_ped, año, mes, trans, f_alta_ped, cve_age, lugar, cve_cte) ";
                sql += "values ('" + Sucursal + "', " + (No_Ped + 1) + ",'" + Año + "', '" + Mes + "', 0, DATE(), " + SAIAgente + ", '" + CveAlmacen + "', " + idCliente + " )";
                
                cmd = new OleDbCommand(sql, connLocal);
                int Resultado = cmd.ExecuteNonQuery();//Guarda folio pedido

                try
                {
                    sql = "SELECT p.CODARTICULO, p.CANTIDAD, p.PRECIO, p.IVA, p.IEPS, ca.SAIAgente, " +
                    "p.CANTIDAD * p.PRECIO AS SUBTOTAL, c.Id_ListaPrecio Lista, " +
                    "(p.CANTIDAD * p.PRECIO) * (p.IVA / 100) IMPIVA, " +
                    "(p.CANTIDAD * p.PRECIO) * (p.IEPS / 100) IMPIEPS FROM PedidosDet p " +
                    "inner join PedidosEnc pe on p.idPedidosEnc = pe.idPedidosEnc " +
                    " inner join CatClientes c " +
                    "on pe.codCliente = c.id_Cliente " +
                    "inner join catAgentes ca " +
                    "on c.id_agente = ca.id_agente" +
                    " where p.idPedidosEnc = " + idPedidoEnc.ToString();

                    cmd = new OleDbCommand(sql, connRemota);
                    dr = cmd.ExecuteReader();

                    int cont = 1;
                    decimal IVA = 0;
                    decimal IEPS = 0;
                    decimal Subtotal = 0;
                    decimal Total = 0;

                    while (dr.Read())
                    {
                        string codArt = dr["codarticulo"].ToString();
                        string cantidad = dr["cantidad"].ToString();
                        string precio = dr["precio"].ToString();
                        string iva = dr["iva"].ToString();
                        string ieps = dr["ieps"].ToString();
                        string lista = dr["lista"].ToString();                       

                        try
                        {
                            #region //Guarda pedido detalle por articulo
                            sql = "INSERT INTO PediDeta (" +
                            "no_ped, " +
                            "cve_suc, " +
                            "cve_prod, " +
                            "med_prod, " +
                            "deta_prod, " +
                            "trans, " +
                            "fol_prod, " +
                            "new_med) values (" +
                            (No_Ped + 1).ToString() + ", '" +
                            Sucursal + "', '" +
                            codArt + "', " +
                            "0, " +
                            "'', " +
                            "1, " +
                            cont.ToString() +
                            ", '')";

                            IVA += decimal.Parse(dr["IMPIVA"].ToString());
                            IEPS += decimal.Parse(dr["IMPIEPS"].ToString());
                            Subtotal += decimal.Parse(dr["SUBTOTAL"].ToString());
                            Total = Subtotal + IVA + IEPS;

                            cmd = new OleDbCommand(sql, connLocal);
                            cmd.ExecuteNonQuery();

                            cont++;

                            sql = "insert into PEDIDOD (" +
                            "NO_PED," + //N:10.0)
                            "CVE_PROD," + //C:20)
                            "CSE_PROD," + //C:10)
                            "MED_PROD," + //N:5.2)
                            "CANT_PROD," + //N:20.8)
                            "VALOR_PROD," + //N:20.8)
                            "CPM_PED," + //N:10.2)
                            "FECHA_ENT," + //D:8)
                            "STATUS1," + //C:1)
                            "SALDO," + //N:20.8)
                            "IVA_PROD," + //N:7.2)
                            "FACTOR," + //N:12.6)
                            "UNIDAD," + //C:5)
                            "CVE_SUC," + //C:3)
                            "TRANS," + //N:2.0)
                            "CONV_VAR," + //N:1.0)
                            "DCTO1," + //N:12.8)
                            "DCTO2," + //N:12.8)
                            "DCTOTOT," + //N:20.6)
                            "LOTE," + //C:30)
                            "STAT_PRO," + //C:10)
                            "FOL_PROD," + //N:6.0)
                            "LISTA_PRE," + //C:5)
                            "CVE_MON_D," + //N:10.0)
                            "POR_PITEX," + //N:5.2)
                            "CALIVA," + //C:4)
                            "PORCRETIVA," + //N:9.6)
                            "PORCENIEPS," + //N:5.2)
                            "IEPS_PROD," + //N:20.6)
                            "RETEN_IVA," + //N:20.6)
                            "NEW_MED," + //C:6)
                            "COMAGE1P," + //N:10.6)
                            "DINAGE1P," + //N:20.2)
                            "COMAGE2P," + //N:10.6)
                            "DINAGE2P," + //N:20.2)
                            "PREC_ANT," + //N:20.8)
                            "DCTO1_ANT," + //N:12.8)
                            "DCTO2_ANT," + //N:12.8)
                            "LISTA_ANT," + //C:1)
                            "FECHA_ANT," + //D:8)
                            "CLISTA1," + //C:10)
                            "CLISTA2," + //C:10)
                            "RETISRPEDP," + //N:9.6)
                            "RETISRPEDD," + //N:20.6)
                            "FACTORIEPS," + //N:12.6)
                            "IVA_IEPS," + //N:1.0)
                            "KPC_DOC," + //N:1.0)
                            "NO_PROMO," + //N:6.0)
                            "PIMPTO1," + //N:9.6)
                            "PIMPTO2," + //N:9.6)
                            "IMPTO1D," + //N:20.6)
                            "IMPTO2D," + //N:20.6)
                            "FACTO1IEPS," + //N:12.6)
                            "IVA1IEPS," + //N:1.0)
                            "FACTO2IEPS," + //N:12.6)
                            "IVA2IEPS" + //N:1.0)

                            ") VALUES(" +

                            (No_Ped + 1).ToString() + "," + //N:10.0)
                            "'" + codArt + "'," + //C:20)
                            "'99999'," + //C:10)
                            "0," + //N:5.2)
                            cantidad + "," + //N:20.8)
                            precio + "," + //N:20.8)
                            "0," + //N:10.2)
                            "{//}," + //D:8) pendiente
                            "''," + //C:1)
                            cantidad + "," + //N:20.8)
                            iva + "," + //N:7.2)
                            "1," + //N:12.6)
                            "'PIEZA'," + //C:5)
                            "'" + Sucursal + "'," + //C:3)
                            "1," + //N:2.0)
                            "0," + //N:1.0)
                            "0," + //N:12.8)
                            "0," + //N:12.8)
                            "0," + //N:20.6)
                            "''," + //C:30)
                            "''," + //C:10)
                            cont.ToString() + "," + //N:6.0)
                            "'" + lista + "'," + //C:5)
                            "1," + //N:10.0)
                            "0," + //N:5.2)
                            "'" + (iva == "0" ? "PROD" : "TIPO") + "'," + //C:4)
                            iva.ToString() + "," + //N:9.6)
                            ieps.ToString() + "," + //N:5.2)
                            ((int.Parse(cantidad) * decimal.Parse(precio)) * (decimal.Parse(ieps) / 100)).ToString() + "," + //N:20.6)
                            "0," + //N:20.6)
                            "''," + //C:6)
                            "0," + //N:10.6)
                            "0," + //N:20.2)
                            "0," + //N:10.6)
                            "0," + //N:20.2)
                            "0," + //N:20.8)
                            "0," + //N:12.8)
                            "0," + //N:12.8)
                            "''," + //C:1)
                            "{//}," + //D:8)
                            "''," + //C:10)
                            "''," + //C:10)
                            "0," + //N:9.6)
                            "0," + //N:20.6)
                            "0," + //N:12.6)
                            "0," + //N:1.0)
                            "0," + //N:1.0)
                            "0," + //N:6.0)
                            "0," + //N:9.6)
                            "0," + //N:9.6)
                            "0," + //N:20.6)
                            "0," + //N:20.6)
                            "0," + //N:12.6)
                            "0," + //N:1.0)
                            "0," + //N:12.6)
                            "0 " + //N:1.0)
                            ")";

                            cmd = new OleDbCommand(sql, connLocal);
                            cmd.ExecuteNonQuery(); //Guardar pedido detalle articulo
                            #endregion
                        }
                        catch (Exception ExPedeDeta) {
                            return new Result()
                            {
                                Value = false,
                                Message = "Problema al guardar detalle pedido",
                                Data = new DataModel()
                                {
                                    CodigoError = 505,
                                    MensajeBitacora = ExPedeDeta.Message,
                                    Data = ""
                                }
                            };
                        }
                    }

                    try
                    {
                        #region //Guarda Pedido Cabecero
                        sql = "INSERT INTO PedidoC (" +
                        "NO_PED, " +
                        "CVE_CTE, " +
                        "CVE_AGE, " +
                        "F_ALTA_PED, " +
                        "DESCUENTO, " +
                        "DESCUE, " +
                        "STATUS, " +
                        "TOTAL_PED, " +
                        "IMPR_PED, " +
                        "SUBT_PED, " +
                        "IVA, " +
                        "OBSERVA, " +
                        "CVE_MON, " +
                        "TIP_CAM, " +
                        "CVE_SUC, " +
                        "MES, " +
                        "AÑO, " +
                        "USUARIO, " +
                        "TRANS, " +
                        "FECHA_ENT, " +
                        "CIERRE, " +
                        "DATO_1, " +
                        "DATO_2, " +
                        "DATO_3, " +
                        "DATO_4, " +
                        "DATO_5," +
                        "DATO_6, " +
                        "DATO_7, " +
                        "DATO_8, " +
                        "STATUS2, " +
                        "PED_INT, " +
                        "LUGAR, " +
                        "NO_COT, " +
                        "SUC_COT, " +
                        "STAT_PRO, " +
                        "USU_AUTO, " +
                        "PESOTOT, " +
                        "CVEDIRENT, " +
                        "P_Y_D, " +
                        "DESCUE2, " +
                        "DESCUE3, " +
                        "DESCUE4, " +
                        "SALDOANTI, " +
                        "PEDREF, " +
                        "CVE_ENTRE, " +
                        "IEPS, " +
                        "RETIVAPED, " +
                        "NO_REQ, " +
                        "SUC_REQ, " +
                        "CALAGE1, " +
                        "CALAGE2, " +
                        "CVE_AGE2, " +
                        "CVE_MONM, " +
                        "HORA_PED, " +
                        "SIG_AUT, " +
                        "TCDCTO1P, " +
                        "TCDCTO2P, " +
                        "COMORI, " +
                        "RETISRPED, " +
                        "CVEDE1, " +
                        "CVEDE2, " +
                        "CVEDE3, " +
                        "CVEDE4, " +
                        "CVEDE5, " +
                        "CVEDE6, " +
                        "CVEDE7, " +
                        "CVEDE8, " +
                        "USADOMAP, " +
                        "SAVEMOVI, " +
                        "MROP, " +
                        "FOLIO, " +
                        "NMRETOT, " +
                        "IMPTO1, " +
                        "IMPTO2, " +
                        "TIMPTO1, " +
                        "TIMPTO2, " +
                        "CAM_AUTO, " +
                        "IMP1IEPS, " +
                        "IMP2IEPS" +
                        ") values (" +
                        (No_Ped + 1).ToString() + ", " + //NO_PED
                        idCliente.ToString() + ", " + //CVE_CTE
                        SAIAgente.ToString() + ", " + //CVE_AGE
                        "DATE(), " + //F_ALTA_PED
                        "0.0, " + //DESCUENTO
                        "0.0, " + //DESCUE
                        "'POR SURTIR', " + //STATUS	(C:15)
                        Total.ToString("0.00") + ", " + //TOTAL_PED	(N:20.2)
                        "0, " + //IMPR_PED
                        Subtotal.ToString("0.00") + ", " + //SUBT_PED	(N:20.6) 
                        IVA.ToString("0.00") + ", " + //IVA	(N:20.2)
                        "'', " + //OBSERVA(M: 4)
                        "1, " + //CVE_MON	(N:10.0)
                        "0.00, " + //TIP_CAM	(N:10.4)
                        "'" + Sucursal + "', " +//CVE_SUC	(C:3)
                        "'" + Mes + "', " +//MES	(C:2)
                        "'" + Año + "', " +//AÑO	(C:4)
                        "4, " +//USUARIO	(N:10.0)
                        "1, " +//TRANS	(N:2.0)	Valor default 1
                        "{//}, " + //FECHA_ENT	(D:8)	Vacío
                        "'', " +//CIERRE	(C:1)
                        "'', " +//DATO_1	(C:30)
                        "'', " +//DATO_2	(C:30)
                        "'', " +//DATO_3	(C:30)
                        "'', " +//DATO_4	(C:30)
                        "'', " +//DATO_5	(C:30)
                        "'', " +//DATO_6	(C:30)
                        "'', " +//DATO_7	(C:30)
                        "''," +//DATO_8	(C:30)
                        "'Pend. Confirmar', " +//STATUS2	(C:15)
                        "'', " +//PED_INT	(C:18)
                        "'" + CveAlmacen + "', " +//LUGAR	(C:10)	Almacén (1-1,2-1,3-1… etc.)
                        "0, " +//NO_COT	(N:10.0)	Numero cotización default = 0
                        "'', " +//SUC_COT	(C:3)	Sucursal cotización default = “” (vacío)
                        "'', " +//STAT_PRO	(C:10)	Vacío
                        "0, " +//USU_AUTO	(N:10.0)	Usuario autoriza default = 0
                        "0.00, " +//PESOTOT	(N:18.4)	Default = 0
                        "'', " +//CVEDIRENT	(C:6)	Vacío
                        "0, " +//P_Y_D	(L:1)	Vacío
                        "0.0000, " +//DESCUE2	(N:12.8)	Default = 0
                        "0.0000, " +//DESCUE3	(N:12.8)	Default = 0
                        "0.0000, " +//DESCUE4	(N:12.8)	Default = 0
                        "0.00, " +//SALDOANTI	(N:20.4)	Default = 0
                        "'', " +//PEDREF	(C:1)	Vacío
                        "'0', " + //CVE_ENTRE	(C:5)	Default = 0
                        IEPS.ToString("0.00") + ", " +//IEPS	(N:20.2)	Importe IEPS del articulo
                        "0.0000, " +//RETIVAPED	(N:20.6)	Retención IVA pedido default = 0
                        "0, " +//NO_REQ	(N:10.0)	Default = 0
                        "'', " +//SUC_REQ	(C:3)	vacio
                        "1, " +//CALAGE1	(N:1.0)	Valor default 1
                        "1, " + //CALAGE2	(N:1.0)	Valor default 1
                        supervisor + ", " +//CVE_AGE2	(N:5.0)	Cve agente supervisor (100-200-300)
                        "0, " +//CVE_MONM	(N:10.0)	Default 0
                        "'" + DateTime.Now.ToString("HH:mm:ss") + "', " +//HORA_PED	(C:8)	Hora pedido 24 horas hh:mm:ss
                        "0, " +//SIG_AUT	(N:3.0)	Default = 0
                        "0, " +//TCDCTO1P	(N:1.0)	Default = 0
                        "1, " +//TCDCTO2P	(N:1.0)	Default = 1
                        "0, " +//COMORI	(N:1.0)	Default = 0
                        "0.0000, " +//RETISRPED	(N:20.6)	Default = 0
                        "0, " +//CVEDE1	(N:5.0)	Default = 0
                        "0, " +//CVEDE2	(N:5.0)	Default = 0
                        "0, " +//CVEDE3	(N:5.0)	Default = 0
                        "0, " +//CVEDE4	(N:5.0)	Default = 0
                        "0, " +//CVEDE5	(N:5.0)	Default = 0
                        "0, " +//CVEDE6	(N:5.0)	Default = 0
                        "0, " +//CVEDE7	(N:5.0)	Default = 0
                        "0, " +//CVEDE8	(N:5.0)	Default = 0
                        "0, " +//USADOMAP	(N:10.0)	Default = 0
                        "'', " +//SAVEMOVI	(C:1)	Vacío
                        "0, " +//MROP	(N:1.0)	Default = 0
                        "0, " +//FOLIO	(N:10.0)	Default = 0
                        "'', " +//NMRETOT	(C:1)	Vacío
                        "0.0000, " +//IMPTO1	(N:20.6)	Default = 0
                        "0.0000, " +//IMPTO2	(N:20.6)	Default = 0
                        "0, " +//TIMPTO1	(N:1.0)	Default = 0
                        "0, " +//TIMPTO2	(N:1.0)	Default = 0
                        "0, " +//CAM_AUTO	(N:10.0)	Default = 0
                        "0, " +//IMP1IEPS	(N:1.0)	Default = 0
                        "0" +//IMP2IEPS	(N:1.0)	Default = 0
                        ")";

                        cmd = new OleDbCommand(sql, connLocal);
                        cmd.ExecuteNonQuery();
                        #endregion
                    }
                    catch (Exception exPedidoCabecero)
                    {
                        return new Result()
                        {
                            Value = false,
                            Message = "Problema al guardar cabecero pedido",
                            Data = new DataModel()
                            {
                                CodigoError = 505,
                                MensajeBitacora = exPedidoCabecero.Message,
                                Data = ""
                            }
                        };
                    }

                }
                catch (Exception ExPiezasDFQ)
                {
                    return new Result()
                    {
                        Value = false,
                        Message = "Error al obtner detalle pedido DFQ",
                        Data = new DataModel()
                        {
                            CodigoError = 505,
                            MensajeBitacora = ExPiezasDFQ.Message,
                            Data = ""
                        }
                    };
                }

                connLocal.Close();
                connRemota.Close();

                return new Result()
                {
                    Value = true,
                    Message = "Pedido Guardado con Exito folio: " + (No_Ped + 1).ToString(),
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = "Exito",
                        Data = ""
                    }
                };
            }
            catch (Exception ex)
            {
                connLocal.Close();
                connRemota.Close();
                return new Result()
                {
                    Value = false,
                    Message = "Problema al obtener/guardar folio pedido",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                };
            }
        }

    }
}
||||||| ed7f51b
=======
﻿using apiQuiroga.Models;
using apiQuiroga.Models.Catalogos;
using apiQuiroga.Models.Facturas;
using apiQuiroga.Models.Usuario;
using System;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;
using WarmPack.Extensions;

namespace apiQuiroga.DA
{
    public class DAPedidosSAI
    {
        private readonly Conexion _conexion = null;
        private readonly Conexion _conexion2 = null;

        public DAPedidosSAI()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
            _conexion2 = new Conexion(ConexionType.MSSQLServer, Globales.ConexionSecundaria);
        }

        public Result PedidoSAIGuardar(int idPedidoEnc, int idCliente, int idAgente, string Sucursal)
        {
            var parametros = new ConexionParameters();
            OleDbConnection connLocal = null;
            OleDbConnection connRemota = null;
            OleDbCommand cmd = null;
            OleDbTransaction transaction;
            DataTable dt;
            string sql = "";
            int No_Ped = 0;
            int conExiste = 0;
            int conClientes = 0;
            int conPreciosLista = 0;
            int conFacturaEnc = 0;
            int conFacturaDet = 0;
            int conPagos = 0;
            OleDbDataReader dr;

            try
            {  //Abrir conexion a SAI               
                connLocal = new OleDbConnection("Provider=VFPOLEDB;Data Source=\\\\192.168.0.92\\c$\\vsai\\EMPRESAS\\prueba;");
                connRemota = new OleDbConnection("Provider=sqloledb;Data Source=192.168.0.52;Initial Catalog=DFQ;User Id=sa;Password=3lDorado;");

                connLocal.Open();
                connRemota.Open();
            }
            catch (Exception exConexiones)
            {
                return new Result()
                {
                    Value = false,
                    Message = "Problema al abrir conexión a base de datos",
                    Data = new DataModel()
                    {
                        CodigoError = 505,
                        MensajeBitacora = exConexiones.Message,
                        Data = ""
                    }
                };
            }

            try
            { //Guardar pedido en SAI                
                
                string Año = DateTime.Now.Year.ToString();
                string Mes = "0" + DateTime.Now.Month.ToString();
                Mes = Mes.Substring((Mes.Length - 2), 2);
                string fecha = DateTime.Now.ToString("MM/dd/yyyy");
                sql = "SELECT max(no_ped) from FolPedVE where cve_suc = '" + Sucursal + "'";
                sql += " and AÑO = '" + Año + "'";
                cmd = new OleDbCommand(sql, connLocal);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    No_Ped = Convert.ToInt32(dr[0]);
                }

                string lugar = "";
                string supervisor = "0";

                if (Sucursal == "CUL")
                {
                    lugar = "1-1";
                    supervisor = "100";
                }
                else if (Sucursal == "TIJ")
                {
                    lugar = "2-1";
                    supervisor = "300";
                }
                else
                {
                    lugar = "3-1";
                    supervisor = "200";
                }

                sql = "insert into FolPedVE (cve_suc, no_ped, año, mes, trans, f_alta_ped, cve_age, lugar, cve_cte) ";
                sql += "values ('" + Sucursal + "', " + (No_Ped + 1) + ",'" + Año + "', '" + Mes + "', 0, DATE(), " + idAgente + ", '" + lugar + "', " + idCliente + " )";
                
                cmd = new OleDbCommand(sql, connLocal);
                int Resultado = cmd.ExecuteNonQuery();//Guarda folio pedido

                try
                {
                    sql = "SELECT p.CODARTICULO, p.CANTIDAD, p.PRECIO, p.IVA, p.IEPS, " +
                    "p.CANTIDAD * p.PRECIO AS SUBTOTAL, c.Id_ListaPrecio Lista, " +
                    "(p.CANTIDAD * p.PRECIO) * (p.IVA / 100) IMPIVA, " +
                    "(p.CANTIDAD * p.PRECIO) * (p.IEPS / 100) IMPIEPS FROM PedidosDet p " +
                    "inner join PedidosEnc pe on p.idPedidosEnc = pe.idPedidosEnc " +
                    " inner join CatClientes c " +
                    "on pe.codCliente = c.id_Cliente " +
                    "where p.idPedidosEnc = " + idPedidoEnc.ToString();

                    cmd = new OleDbCommand(sql, connRemota);
                    dr = cmd.ExecuteReader();

                    int cont = 1;
                    decimal IVA = 0;
                    decimal IEPS = 0;
                    decimal Subtotal = 0;
                    decimal Total = 0;

                    while (dr.Read())
                    {
                        string codArt = dr["codarticulo"].ToString();
                        string cantidad = dr["cantidad"].ToString();
                        string precio = dr["precio"].ToString();
                        string iva = dr["iva"].ToString();
                        string ieps = dr["ieps"].ToString();
                        string lista = dr["lista"].ToString();

                        try
                        {
                            #region //Guarda pedido detalle por articulo
                            sql = "INSERT INTO PediDeta (" +
                            "no_ped, " +
                            "cve_suc, " +
                            "cve_prod, " +
                            "med_prod, " +
                            "deta_prod, " +
                            "trans, " +
                            "fol_prod, " +
                            "new_med) values (" +
                            (No_Ped + 1).ToString() + ", '" +
                            Sucursal + "', '" +
                            codArt + "', " +
                            "0, " +
                            "'', " +
                            "1, " +
                            cont.ToString() +
                            ", '')";

                            IVA += decimal.Parse(dr["IMPIVA"].ToString());
                            IEPS += decimal.Parse(dr["IMPIEPS"].ToString());
                            Subtotal += decimal.Parse(dr["SUBTOTAL"].ToString());
                            Total = Subtotal + IVA + IEPS;

                            cmd = new OleDbCommand(sql, connLocal);
                            cmd.ExecuteNonQuery();

                            cont++;

                            sql = "insert into PEDIDOD (" +
                            "NO_PED," + //N:10.0)
                            "CVE_PROD," + //C:20)
                            "CSE_PROD," + //C:10)
                            "MED_PROD," + //N:5.2)
                            "CANT_PROD," + //N:20.8)
                            "VALOR_PROD," + //N:20.8)
                            "CPM_PED," + //N:10.2)
                            "FECHA_ENT," + //D:8)
                            "STATUS1," + //C:1)
                            "SALDO," + //N:20.8)
                            "IVA_PROD," + //N:7.2)
                            "FACTOR," + //N:12.6)
                            "UNIDAD," + //C:5)
                            "CVE_SUC," + //C:3)
                            "TRANS," + //N:2.0)
                            "CONV_VAR," + //N:1.0)
                            "DCTO1," + //N:12.8)
                            "DCTO2," + //N:12.8)
                            "DCTOTOT," + //N:20.6)
                            "LOTE," + //C:30)
                            "STAT_PRO," + //C:10)
                            "FOL_PROD," + //N:6.0)
                            "LISTA_PRE," + //C:5)
                            "CVE_MON_D," + //N:10.0)
                            "POR_PITEX," + //N:5.2)
                            "CALIVA," + //C:4)
                            "PORCRETIVA," + //N:9.6)
                            "PORCENIEPS," + //N:5.2)
                            "IEPS_PROD," + //N:20.6)
                            "RETEN_IVA," + //N:20.6)
                            "NEW_MED," + //C:6)
                            "COMAGE1P," + //N:10.6)
                            "DINAGE1P," + //N:20.2)
                            "COMAGE2P," + //N:10.6)
                            "DINAGE2P," + //N:20.2)
                            "PREC_ANT," + //N:20.8)
                            "DCTO1_ANT," + //N:12.8)
                            "DCTO2_ANT," + //N:12.8)
                            "LISTA_ANT," + //C:1)
                            "FECHA_ANT," + //D:8)
                            "CLISTA1," + //C:10)
                            "CLISTA2," + //C:10)
                            "RETISRPEDP," + //N:9.6)
                            "RETISRPEDD," + //N:20.6)
                            "FACTORIEPS," + //N:12.6)
                            "IVA_IEPS," + //N:1.0)
                            "KPC_DOC," + //N:1.0)
                            "NO_PROMO," + //N:6.0)
                            "PIMPTO1," + //N:9.6)
                            "PIMPTO2," + //N:9.6)
                            "IMPTO1D," + //N:20.6)
                            "IMPTO2D," + //N:20.6)
                            "FACTO1IEPS," + //N:12.6)
                            "IVA1IEPS," + //N:1.0)
                            "FACTO2IEPS," + //N:12.6)
                            "IVA2IEPS" + //N:1.0)

                            ") VALUES(" +

                            (No_Ped + 1).ToString() + "," + //N:10.0)
                            "'" + codArt + "'," + //C:20)
                            "'99999'," + //C:10)
                            "0," + //N:5.2)
                            cantidad + "," + //N:20.8)
                            precio + "," + //N:20.8)
                            "0," + //N:10.2)
                            "DATE()," + //D:8) pendiente
                            "''," + //C:1)
                            cantidad + "," + //N:20.8)
                            iva + "," + //N:7.2)
                            "1," + //N:12.6)
                            "'PIEZA'," + //C:5)
                            "'" + Sucursal + "'," + //C:3)
                            "1," + //N:2.0)
                            "0," + //N:1.0)
                            "0," + //N:12.8)
                            "0," + //N:12.8)
                            "0," + //N:20.6)
                            "''," + //C:30)
                            "''," + //C:10)
                            cont.ToString() + "," + //N:6.0)
                            "'" + lista + "'," + //C:5)
                            "1," + //N:10.0)
                            "0," + //N:5.2)
                            "'" + (iva == "0" ? "PROD" : "TIPO") + "'," + //C:4)
                            iva.ToString() + "," + //N:9.6)
                            ieps.ToString() + "," + //N:5.2)
                            ((int.Parse(cantidad) * decimal.Parse(precio)) * (decimal.Parse(ieps) / 100)).ToString() + "," + //N:20.6)
                            "0," + //N:20.6)
                            "''," + //C:6)
                            "0," + //N:10.6)
                            "0," + //N:20.2)
                            "0," + //N:10.6)
                            "0," + //N:20.2)
                            "0," + //N:20.8)
                            "0," + //N:12.8)
                            "0," + //N:12.8)
                            "''," + //C:1)
                            "DATE()," + //D:8)
                            "''," + //C:10)
                            "''," + //C:10)
                            "0," + //N:9.6)
                            "0," + //N:20.6)
                            "0," + //N:12.6)
                            "0," + //N:1.0)
                            "0," + //N:1.0)
                            "0," + //N:6.0)
                            "0," + //N:9.6)
                            "0," + //N:9.6)
                            "0," + //N:20.6)
                            "0," + //N:20.6)
                            "0," + //N:12.6)
                            "0," + //N:1.0)
                            "0," + //N:12.6)
                            "0 " + //N:1.0)
                            ")";

                            cmd = new OleDbCommand(sql, connLocal);
                            cmd.ExecuteNonQuery(); //Guardar pedido detalle articulo
                            #endregion
                        }
                        catch (Exception ExPedeDeta) {
                            return new Result()
                            {
                                Value = false,
                                Message = "Problema al guardar detalle pedido",
                                Data = new DataModel()
                                {
                                    CodigoError = 505,
                                    MensajeBitacora = ExPedeDeta.Message,
                                    Data = ""
                                }
                            };
                        }
                    }

                    try
                    {
                        #region //Guarda Pedido Cabecero
                        sql = "INSERT INTO PedidoC (" +
                        "NO_PED, " +
                        "CVE_CTE, " +
                        "CVE_AGE, " +
                        "F_ALTA_PED, " +
                        "DESCUENTO, " +
                        "DESCUE, " +
                        "STATUS, " +
                        "TOTAL_PED, " +
                        "IMPR_PED, " +
                        "SUBT_PED, " +
                        "IVA, " +
                        "OBSERVA, " +
                        "CVE_MON, " +
                        "TIP_CAM, " +
                        "CVE_SUC, " +
                        "MES, " +
                        "AÑO, " +
                        "USUARIO, " +
                        "TRANS, " +
                        "FECHA_ENT, " +
                        "CIERRE, " +
                        "DATO_1, " +
                        "DATO_2, " +
                        "DATO_3, " +
                        "DATO_4, " +
                        "DATO_5," +
                        "DATO_6, " +
                        "DATO_7, " +
                        "DATO_8, " +
                        "STATUS2, " +
                        "PED_INT, " +
                        "LUGAR, " +
                        "NO_COT, " +
                        "SUC_COT, " +
                        "STAT_PRO, " +
                        "USU_AUTO, " +
                        "PESOTOT, " +
                        "CVEDIRENT, " +
                        "P_Y_D, " +
                        "DESCUE2, " +
                        "DESCUE3, " +
                        "DESCUE4, " +
                        "SALDOANTI, " +
                        "PEDREF, " +
                        "CVE_ENTRE, " +
                        "IEPS, " +
                        "RETIVAPED, " +
                        "NO_REQ, " +
                        "SUC_REQ, " +
                        "CALAGE1, " +
                        "CALAGE2, " +
                        "CVE_AGE2, " +
                        "CVE_MONM, " +
                        "HORA_PED, " +
                        "SIG_AUT, " +
                        "TCDCTO1P, " +
                        "TCDCTO2P, " +
                        "COMORI, " +
                        "RETISRPED, " +
                        "CVEDE1, " +
                        "CVEDE2, " +
                        "CVEDE3, " +
                        "CVEDE4, " +
                        "CVEDE5, " +
                        "CVEDE6, " +
                        "CVEDE7, " +
                        "CVEDE8, " +
                        "USADOMAP, " +
                        "SAVEMOVI, " +
                        "MROP, " +
                        "FOLIO, " +
                        "NMRETOT, " +
                        "IMPTO1, " +
                        "IMPTO2, " +
                        "TIMPTO1, " +
                        "TIMPTO2, " +
                        "CAM_AUTO, " +
                        "IMP1IEPS, " +
                        "IMP2IEPS" +
                        ") values (" +
                        (No_Ped + 1).ToString() + ", " + //NO_PED
                        idCliente.ToString() + ", " + //CVE_CTE
                        idAgente.ToString() + ", " + //CVE_AGE
                        "DATE(), " + //F_ALTA_PED
                        "0.0, " + //DESCUENTO
                        "0.0, " + //DESCUE
                        "'POR SURTIR', " + //STATUS	(C:15)
                        Total.ToString("0.00") + ", " + //TOTAL_PED	(N:20.2)
                        "0, " + //IMPR_PED
                        Subtotal.ToString("0.00") + ", " + //SUBT_PED	(N:20.6) 
                        IVA.ToString("0.00") + ", " + //IVA	(N:20.2)
                        "'', " + //OBSERVA(M: 4)
                        "1, " + //CVE_MON	(N:10.0)
                        "0.00, " + //TIP_CAM	(N:10.4)
                        "'" + Sucursal + "', " +//CVE_SUC	(C:3)
                        "'" + Mes + "', " +//MES	(C:2)
                        "'" + Año + "', " +//AÑO	(C:4)
                        "4, " +//USUARIO	(N:10.0)
                        "1, " +//TRANS	(N:2.0)	Valor default 1
                        "DATE(), " + //FECHA_ENT	(D:8)	Vacío
                        "'', " +//CIERRE	(C:1)
                        "'', " +//DATO_1	(C:30)
                        "'', " +//DATO_2	(C:30)
                        "'', " +//DATO_3	(C:30)
                        "'', " +//DATO_4	(C:30)
                        "'', " +//DATO_5	(C:30)
                        "'', " +//DATO_6	(C:30)
                        "'', " +//DATO_7	(C:30)
                        "''," +//DATO_8	(C:30)
                        "'Pend. Confirmar', " +//STATUS2	(C:15)
                        "'', " +//PED_INT	(C:18)
                        "'" + lugar + "', " +//LUGAR	(C:10)	Almacén (1-1,2-1,3-1… etc.)
                        "0, " +//NO_COT	(N:10.0)	Numero cotización default = 0
                        "'', " +//SUC_COT	(C:3)	Sucursal cotización default = “” (vacío)
                        "'', " +//STAT_PRO	(C:10)	Vacío
                        "0, " +//USU_AUTO	(N:10.0)	Usuario autoriza default = 0
                        "0.00, " +//PESOTOT	(N:18.4)	Default = 0
                        "'', " +//CVEDIRENT	(C:6)	Vacío
                        "0, " +//P_Y_D	(L:1)	Vacío
                        "0.0000, " +//DESCUE2	(N:12.8)	Default = 0
                        "0.0000, " +//DESCUE3	(N:12.8)	Default = 0
                        "0.0000, " +//DESCUE4	(N:12.8)	Default = 0
                        "0.00, " +//SALDOANTI	(N:20.4)	Default = 0
                        "'', " +//PEDREF	(C:1)	Vacío
                        "'0', " + //CVE_ENTRE	(C:5)	Default = 0
                        IEPS.ToString("0.00") + ", " +//IEPS	(N:20.2)	Importe IEPS del articulo
                        "0.0000, " +//RETIVAPED	(N:20.6)	Retención IVA pedido default = 0
                        "0, " +//NO_REQ	(N:10.0)	Default = 0
                        "'', " +//SUC_REQ	(C:3)	vacio
                        "1, " +//CALAGE1	(N:1.0)	Valor default 1
                        "1, " + //CALAGE2	(N:1.0)	Valor default 1
                        supervisor + ", " +//CVE_AGE2	(N:5.0)	Cve agente supervisor (100-200-300)
                        "0, " +//CVE_MONM	(N:10.0)	Default 0
                        "'" + DateTime.Now.ToString("HH:mm:ss") + "', " +//HORA_PED	(C:8)	Hora pedido 24 horas hh:mm:ss
                        "0, " +//SIG_AUT	(N:3.0)	Default = 0
                        "0, " +//TCDCTO1P	(N:1.0)	Default = 0
                        "1, " +//TCDCTO2P	(N:1.0)	Default = 1
                        "0, " +//COMORI	(N:1.0)	Default = 0
                        "0.0000, " +//RETISRPED	(N:20.6)	Default = 0
                        "0, " +//CVEDE1	(N:5.0)	Default = 0
                        "0, " +//CVEDE2	(N:5.0)	Default = 0
                        "0, " +//CVEDE3	(N:5.0)	Default = 0
                        "0, " +//CVEDE4	(N:5.0)	Default = 0
                        "0, " +//CVEDE5	(N:5.0)	Default = 0
                        "0, " +//CVEDE6	(N:5.0)	Default = 0
                        "0, " +//CVEDE7	(N:5.0)	Default = 0
                        "0, " +//CVEDE8	(N:5.0)	Default = 0
                        "0, " +//USADOMAP	(N:10.0)	Default = 0
                        "'', " +//SAVEMOVI	(C:1)	Vacío
                        "0, " +//MROP	(N:1.0)	Default = 0
                        "0, " +//FOLIO	(N:10.0)	Default = 0
                        "'', " +//NMRETOT	(C:1)	Vacío
                        "0.0000, " +//IMPTO1	(N:20.6)	Default = 0
                        "0.0000, " +//IMPTO2	(N:20.6)	Default = 0
                        "0, " +//TIMPTO1	(N:1.0)	Default = 0
                        "0, " +//TIMPTO2	(N:1.0)	Default = 0
                        "0, " +//CAM_AUTO	(N:10.0)	Default = 0
                        "0, " +//IMP1IEPS	(N:1.0)	Default = 0
                        "0" +//IMP2IEPS	(N:1.0)	Default = 0
                        ")";

                        cmd = new OleDbCommand(sql, connLocal);
                        cmd.ExecuteNonQuery();
                        #endregion
                    }
                    catch (Exception exPedidoCabecero)
                    {
                        return new Result()
                        {
                            Value = false,
                            Message = "Problema al guardar cabecero pedido",
                            Data = new DataModel()
                            {
                                CodigoError = 505,
                                MensajeBitacora = exPedidoCabecero.Message,
                                Data = ""
                            }
                        };
                    }

                }
                catch (Exception ExPiezasDFQ)
                {
                    return new Result()
                    {
                        Value = false,
                        Message = "Error al obtner detalle pedido DFQ",
                        Data = new DataModel()
                        {
                            CodigoError = 505,
                            MensajeBitacora = ExPiezasDFQ.Message,
                            Data = ""
                        }
                    };
                }

                connLocal.Close();
                connRemota.Close();

                return new Result()
                {
                    Value = true,
                    Message = "Pedido Guardado con Exito folio: " + (No_Ped + 1).ToString(),
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = "Exito",
                        Data = ""
                    }
                };
            }
            catch (Exception ex)
            {
                connLocal.Close();
                connRemota.Close();
                return new Result()
                {
                    Value = false,
                    Message = "Problema al obtener/guardar folio pedido",
                    Data = new DataModel()
                    {
                        CodigoError = 101,
                        MensajeBitacora = ex.Message,
                        Data = ""
                    }
                };
            }
        }

    }
}
>>>>>>> origin/FuncionalidadPedidos
