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
    public class LNuevoViaje
    {
        public IEnumerable<NUevoViajeResponse> LeerNuevoViaje(string pUser_id,string pPlanes, string pFecha, string pNombre,string pEdad )
        {
            string lineagg = "0";
            try
            {
                List<NUevoViajeResponse> lstNuevoViaje = new List<NUevoViajeResponse>();
                lineagg += ",1";
                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {

                    SqlCommand cmd = new SqlCommand("latinamericajourneys.LAJ_Encuesta_S", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@User_id", SqlDbType.VarChar).Value = pUser_id;
                    cmd.Parameters.Add("@Planes", SqlDbType.VarChar).Value = pPlanes;
                    cmd.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = pFecha;
                    cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = pNombre;
                    cmd.Parameters.Add("@Edad", SqlDbType.VarChar).Value = pEdad;
                    lineagg += ",2";
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    lineagg += ",3";
                    while (rdr.Read())
                    {
                        lineagg += ",4";
                        NUevoViajeResponse fNuevoViaje = new NUevoViajeResponse();

                        fNuevoViaje.Status = rdr["Status"].ToString();
                        fNuevoViaje.Idsol = rdr["Msg"].ToString();
                        lstNuevoViaje.Add(item: fNuevoViaje);
                    }

                    lineagg += ",5";
                    con.Close();
                }
                return lstNuevoViaje;
            }
            catch (Exception ex)
            {
                throw new Exception { Source = lineagg };
            }

        }
      
    }
}