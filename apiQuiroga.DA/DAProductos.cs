using apiQuiroga.Models;
using apiQuiroga.Models.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;

namespace apiQuiroga.DA
{
    public class DAProductos
    {
        private readonly Conexion _conexion = null;

        public DAProductos()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);

        }

        public Result<List<ProductosModel>> ProductosOfertasCon()
        {

            var parametros = new ConexionParameters();
            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);
            parametros.Add("@pCodError", ConexionDbType.Int, System.Data.ParameterDirection.Output);

            var r = _conexion.ExecuteWithResults<ProductosModel>("procOfertasCon", parametros);

            return r;
         
        }

        public Result<List<ProductosModel>> ProductosOfertasGuardar(ProductosModel p)
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pIDProducto", ConexionDbType.Int, p.ProductoID);
            parametros.Add("@pPrecioOferta", ConexionDbType.Decimal, p.ProductoPrecioOferta);
            parametros.Add("@pFechaOferta", ConexionDbType.VarChar, p.ProdFechaOferta);
            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);

            var r = _conexion.ExecuteWithResults<ProductosModel>("procOfertasCrear", parametros);

            return r;
        }

        public Result ProductosOfertasEliminar(ProductosModel p)
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pIDProducto", ConexionDbType.Int, p.ProductoID);
            parametros.Add("@pPrecioOferta", ConexionDbType.Decimal, p.ProductoPrecioOferta);
            parametros.Add("@pFechaOferta", ConexionDbType.VarChar, p.ProdFechaOferta);
            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);

            var r = _conexion.Execute("procOfertasEliminar", parametros);

            return r;
        }
    }
}