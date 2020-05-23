using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using WebApiLatinAmericaJourneys.Models;
using WebApiLatinAmericaJourneys.ModelsWallet;
using System.Drawing;

namespace WebApiLatinAmericaJourneys.Repository.Wallet
{
    public class LEncuesta
    {
        public IEnumerable<EncuestaResponse> LeerEncuesta(string pUser_id,string pPregunta, string pRespuesta )
        {
            string lineagg = "0";
            try
            {
                List<EncuestaResponse> lstEncuesta = new List<EncuestaResponse>();
                lineagg += ",1";
                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {

                    SqlCommand cmd = new SqlCommand("latinamericajourneys.LAJ_Encuesta_S", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@User_id", SqlDbType.VarChar).Value = pUser_id;
                    cmd.Parameters.Add("@Pregunta", SqlDbType.VarChar).Value = pPregunta;
                    cmd.Parameters.Add("@Respuesta", SqlDbType.VarChar).Value = pRespuesta;
                    lineagg += ",2";
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    lineagg += ",3";
                    while (rdr.Read())
                    {
                        lineagg += ",4";
                        EncuestaResponse fencuesta = new EncuestaResponse();

                        fencuesta.Status = rdr["Status"].ToString();
                        fencuesta.Msg = rdr["Msg"].ToString();
                        lstEncuesta.Add(item: fencuesta);
                    }

                    lineagg += ",5";
                    con.Close();
                }
                return lstEncuesta;
            }
            catch (Exception ex)
            {
                throw new Exception { Source = lineagg };
            }

        }
      
    }
}