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
    public class DAUbicaciones
    {
        private readonly Conexion _Conexion = null;

        public DAUbicaciones()
        {
            _Conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }

        public Result<List<UbicacionAlmacenModel>> AlmacenesCrear(UbicacionAlmacenModel a)
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pDescripcion", ConexionDbType.VarChar, a.Descripcion);
            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);

            var r = _Conexion.ExecuteWithResults<UbicacionAlmacenModel>("QW_procCatUbicacionesAlmacenesCrear", parametros);

            return r;
        }

        public Result<List<UbicacionAlmacenModel>> AlmacenesCon()
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);

            var r = _Conexion.ExecuteWithResults<UbicacionAlmacenModel>("QW_procCatUbicacionesAlmacenesCon", parametros);

            return r;
        }

        public Result<List<UbicacionPasilloModel>> PasillosCon()
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);

            var r = _Conexion.ExecuteWithResults<UbicacionPasilloModel>("QW_procCatUbicacionesPasillosCon", parametros);

            return r;
        }

        public Result PasilloCrear(string pasillo)
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pID_Pasillo", ConexionDbType.VarChar, pasillo);
            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);

            var r = _Conexion.Execute("QW_procCatUbicacionesPasillosCrear", parametros);

            return r;

        }

        public Result PasilloEliminar(string pasillo)
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pID_Pasillo", ConexionDbType.VarChar, pasillo);
            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);

            var r = _Conexion.Execute("QW_procCatUbicacionesPasillosEliminar", parametros);

            return r;

        }

        public Result<List<UbicacionRackModel>> RacksCon()
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);

            var r = _Conexion.ExecuteWithResults<UbicacionRackModel>("QW_procCatUbicacionesRacksCon", parametros);

            return r;
        }

        public Result RacksCrear(int rack)
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pID_Rack", ConexionDbType.Int, rack);
            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);

            var r = _Conexion.Execute("QW_procCatUbicacionesRacksCrear", parametros);

            return r;

        }

        public Result RacksEliminar(int rack)
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pID_Rack", ConexionDbType.Int, rack);
            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);

            var r = _Conexion.Execute("QW_procCatUbicacionesRacksEliminar", parametros);

            return r;

        }

        public Result<List<UbicacionSeccionModel>> SeccionesCon()
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);

            var r = _Conexion.ExecuteWithResults<UbicacionSeccionModel>("QW_procCatUbicacionesSeccionesCon", parametros);

            return r;
        }

        public Result SeccionesCrear(int seccion)
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pID_Seccion", ConexionDbType.Int, seccion);
            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);

            var r = _Conexion.Execute("QW_procCatUbicacionesSeccionesCrear", parametros);

            return r;

        }

        public Result AlmacenesEliminar(int idAlmacen)
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pID_Almacen", ConexionDbType.Int, idAlmacen);
            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);

            var r = _Conexion.Execute("QW_procCatUbicacionesAlmacenesEliminar", parametros);

            return r;

        }

        public Result SeccionesEliminar(int seccion)
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pID_Seccion", ConexionDbType.Int, seccion);
            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);

            var r = _Conexion.Execute("QW_procCatUbicacionesSeccionEliminar", parametros);

            return r;

        }


        public Result<List<UbicacionNivelModel>> NivelesCon()
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);

            var r = _Conexion.ExecuteWithResults<UbicacionNivelModel>("QW_procCatUbicacionesNivelesCon", parametros);

            return r;
        }

        public Result NivelesCrear(int nivel)
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pID_Nivel", ConexionDbType.Int, nivel);
            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);

            var r = _Conexion.Execute("QW_procCatUbicacionesNivelesCrear", parametros);

            return r;

        }

        public Result NivelesEliminar(int nivel)
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pID_Nivel", ConexionDbType.Int, nivel);
            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);

            var r = _Conexion.Execute("QW_procCatUbicacionesNivelEliminar", parametros);

            return r;

        }

        public Result UbicacionCrear(UbicacionModel ubicacion)
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pID_CatUbicaciones", ConexionDbType.Int, ubicacion.ID_CatUbicacionesEnc);
            parametros.Add("@pID_Empresa", ConexionDbType.Int, ubicacion.ID_Empresa);
            parametros.Add("@pID_Ubicacion", ConexionDbType.Int, ubicacion.ID_Ubicacion);
            parametros.Add("@pID_Almacen", ConexionDbType.Int, ubicacion.ID_Almacen);
            parametros.Add("@pID_Pasillo", ConexionDbType.VarChar, ubicacion.ID_Pasillo);
            parametros.Add("@pID_Rack", ConexionDbType.Int, ubicacion.ID_Rack);
            parametros.Add("@pID_Nivel", ConexionDbType.Int, ubicacion.ID_Nivel);
            parametros.Add("@pID_Seccion", ConexionDbType.Int, ubicacion.ID_Seccion);
            parametros.Add("@pLargo", ConexionDbType.Decimal, ubicacion.Largo);
            parametros.Add("@pAncho", ConexionDbType.Decimal, ubicacion.Ancho);
            parametros.Add("@pAlto", ConexionDbType.Decimal, ubicacion.Alto);
            parametros.Add("@pCapacidad", ConexionDbType.Decimal, ubicacion.Capacidad);


            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);

            var r = _Conexion.Execute("QW_procCatUbicacionesCrear", parametros);

            return r;

        }

        public Result<List<UbicacionModel>> UbicacionesCon(int empresa, int almacen, string pasillo)
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pID_Empresa", ConexionDbType.Int, empresa);
            parametros.Add("@pID_Almacen", ConexionDbType.Int, almacen);
            parametros.Add("@pID_Pasillo", ConexionDbType.VarChar, pasillo);

            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);

            var r = _Conexion.ExecuteWithResults<UbicacionModel>("QW_procCatUbicacionesCon", parametros);

            return r;
        }

        public Result UbicacionEliminar(UbicacionModel ubicacion)
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pID_CatUbicacionesEnc", ConexionDbType.Int, ubicacion.ID_CatUbicacionesEnc);

            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);

            var r = _Conexion.Execute("QW_procCatUbicacionesEliminar", parametros);

            return r;
        }

        public Result UbicacionDetGuardar(UbicacionDetModel det)
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pID_CatUbicacionesEnc", ConexionDbType.Int, det.ID_CatUbicacionesEnc);
            parametros.Add("@pID_CatUbicacionesDet", ConexionDbType.Int, det.ID_CatUbicacionesDet);
            parametros.Add("@pID_Articulo", ConexionDbType.Int, det.ID_Articulo);
            parametros.Add("@pCodBarras", ConexionDbType.VarChar, det.CodBarras);
            parametros.Add("@pCantidad", ConexionDbType.Int, det.Cantidad);
            parametros.Add("@pLote", ConexionDbType.VarChar, det.Lote);
            parametros.Add("@pFechaCaducidad", ConexionDbType.Date, det.FechaCaducidad);

            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);

            var r = _Conexion.Execute("QW_procCatUbicacionesDetGuardar", parametros);

            return r;

        }

        public Result<List<UbicacionDetModel>> UbicacionProductosCon(int id_CatUbicacionesEnc)
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pID_CatUbicacionesEnc", ConexionDbType.Int, id_CatUbicacionesEnc);


            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);

            var r = _Conexion.ExecuteWithResults<UbicacionDetModel>("QW_procCatUbicacionesDetCon", parametros);

            return r;
        }

        public Result UbicacionDetEliminar(int id_CatUbicacionesDet)
        {
            var parametros = new ConexionParameters();            
            parametros.Add("@pID_CatUbicacionesDet", ConexionDbType.Int, id_CatUbicacionesDet);            

            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, 300, System.Data.ParameterDirection.Output, 300);

            var r = _Conexion.Execute("QW_procCatUbicacionesDetEliminar", parametros);

            return r;

        }
    }
}
