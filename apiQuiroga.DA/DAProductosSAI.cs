using apiQuiroga.Models;
using apiQuiroga.Models.Catalogos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;

namespace apiQuiroga.DA
{

    public class DAProductosSAI
    {
        private readonly OleDbConnection _con = null;
        OleDbCommand cmd;

        
        public DAProductosSAI()
        {
             _con = new OleDbConnection("Provider=VFPOLEDB;Data Source=C:\\SAITABLAS\\;");
          
        }

        public Result<DataModel> ProductosSAI()
        {
            _con.Open();
            string query = "SELECT * FROM existe";
            cmd = new OleDbCommand(query, _con);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            try
            {
                var JSONresult = "";
                JSONresult = JsonConvert.SerializeObject(dt);
                var convertedList = JSONresult;
                //var convertedList = (from rw in dt.AsEnumerable()
                //                     select new ProductosSAIModel()
                //                     {
                //                         cse_prod = Convert.ToInt32(rw["cse_prod"]),
                //                         cve_prod = Convert.ToInt32(rw["cve_prod"]),
                //                         lugar = Convert.ToString(rw["lugar"]),
                //                         existencia = Convert.ToDecimal(rw["existencia"])
                //                     }).ToList();

                var r = convertedList;

                return new Result<DataModel>()
                {
                    Value = true,
                    Message = "0",
                    Data = new DataModel()
                    {
                        CodigoError = 0,
                        MensajeBitacora = "datos",
                        Data = r
                    }
                };



            }
            catch (Exception ex)
            {

                return new Result<DataModel>()
                {
                    Value = false,
                    Message = "Problemas en catalogo SAI",
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
