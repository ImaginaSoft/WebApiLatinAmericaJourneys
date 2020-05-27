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
        public IEnumerable<EncuestaResponse> LeerEncuesta(string pUser_id )
        {
            string lineagg = "0";
            string demo1 = "";
            string demo2 = "";
            try
            {
                List<EncuestaResponse> lstEncuesta = new List<EncuestaResponse>();
                lineagg += ",1";
                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {

                    SqlCommand cmd = new SqlCommand("dbo.APP_LeerRespuestaPregunta_S", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MsgTrans", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@MsgRespuesta", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@CodCliente", SqlDbType.Int).Value = pUser_id;
                   
                    lineagg += ",2";
                    con.Open();
                    cmd.ExecuteNonQuery();
                    demo1 = cmd.Parameters["@MsgTrans"].Value.ToString();
                    demo2 = cmd.Parameters["@MsgRespuesta"].Value.ToString();

                    EncuestaResponse fencuesta = new EncuestaResponse();
                    fencuesta.Status = demo1;
                    fencuesta.Msg = demo2;
                    lstEncuesta.Add(item: fencuesta);

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

        public string RegistrarEncuesta(string pUser_id, string pPregunta, string pRespuesta)
        {
           string lineagg = "0";
           string Status = "";
            try
            {
                List<CalificaRequest> lstPropuesta = new List<CalificaRequest>();
                lineagg += ",1";
                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {
                    SqlCommand cmd = new SqlCommand("dbo.APP_PreguntaRespUsuario_I", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MsgTrans", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@pCodCliente", SqlDbType.Int).Value = pUser_id;
                    cmd.Parameters.Add("@pIdPregunta", SqlDbType.Int).Value = pPregunta;
                    cmd.Parameters.Add("@pRespuesta", SqlDbType.Char).Value = pRespuesta;

                    lineagg += ",2";
                    con.Open();

                    cmd.ExecuteNonQuery();

                    Status = cmd.Parameters["@MsgTrans"].Value.ToString();

                    con.Close();
                }
                lineagg += ",5";

                return Status;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}