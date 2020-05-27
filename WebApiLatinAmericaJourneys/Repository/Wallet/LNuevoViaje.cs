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
        public IEnumerable<NuevoViajeResponse> RegistrarViajeros(string pIdViaje, string pNombre, string pApPaterno, string pApMaterno, string pEdad )
        {
            string lineagg = "0";
            string demo1 = "";

            try
            {
                List<NuevoViajeResponse> lstNuevoViaje = new List<NuevoViajeResponse>();
                lineagg += ",1";
                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {

                    SqlCommand cmd = new SqlCommand("dbo.APP_NuevoViajePasajero_I", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MsgTrans", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@pIdViaje", SqlDbType.Int).Value = pIdViaje;
                    cmd.Parameters.Add("@pNombre", SqlDbType.VarChar).Value = pNombre;
                    cmd.Parameters.Add("@pApPaterno", SqlDbType.VarChar).Value = pApPaterno;
                    cmd.Parameters.Add("@pApMaterno", SqlDbType.VarChar).Value = pApMaterno;
                    cmd.Parameters.Add("@pEdad", SqlDbType.Int).Value = pEdad;
                    lineagg += ",2";
                    con.Open();
                    cmd.ExecuteNonQuery();

                    demo1 = cmd.Parameters["@MsgTrans"].Value.ToString();
                 
                    NuevoViajeResponse fencuesta = new NuevoViajeResponse();

                    fencuesta.Status = demo1;
                    fencuesta.Idsol = pIdViaje;

                    lstNuevoViaje.Add(item: fencuesta);

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


        public int RegistrarNuevoViaje(string pUser_id, string pPlanes, string pFechaViaje)
        {
            string lineagg = "0";
            string Status = " ";
            int CodViaje = 0;

            try
            {
                List<NuevoViajeResponse> lstNuevoViaje = new List<NuevoViajeResponse>();
                lineagg += ",1";
                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {
                    SqlCommand cmd = new SqlCommand("dbo.APP_NuevoViaje_I", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MsgTrans", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@CodViaje", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@pCodCliente", SqlDbType.Int).Value = pUser_id;
                    cmd.Parameters.Add("@pPlanes", SqlDbType.VarChar).Value = pPlanes;
                    cmd.Parameters.Add("@pFechaViaje", SqlDbType.DateTime).Value = pFechaViaje;

                    lineagg += ",2";
                    con.Open();

                    cmd.ExecuteNonQuery();

                    Status = cmd.Parameters["@MsgTrans"].Value.ToString();
                    CodViaje = Convert.ToInt32(cmd.Parameters["@CodViaje"].Value.ToString());

                    //NuevoViajeResponse fencuesta = new NuevoViajeResponse();

                    //fencuesta.Status = Status;
                    //fencuesta.Idsol = CodViaje;

                    //lstNuevoViaje.Add(item: fencuesta);

                    con.Close();
                }
                lineagg += ",5";

                return CodViaje;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}