using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using WebApiLatinAmericaJourneys.Models;

namespace WebApiLatinAmericaJourneys.Repository.LatinAmericaJourneys
{
    public class LFotoRegistro
    {

        public FotoRegistroResponse RegistrarFoto(int pNropedido, int pNroPropuesta, int pNroVersion, string pFototitulo, string pFotocomment,  string pFotocontent)
        {
            string lineagg = "0";

            FotoRegistroResponse Foto = new FotoRegistroResponse();
            Foto.status = "";

            try
            {
                List<FotoRegistroRequest> lstFotoRegistro = new List<FotoRegistroRequest>();
                lineagg += ",1";
                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {

                    SqlCommand cmd = new SqlCommand("dbo.APP_ImagenAlbum_I", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pNroPedido", SqlDbType.Int).Value = pNropedido;
                    cmd.Parameters.Add("@pNroPropuesta", SqlDbType.Int).Value = pNroPropuesta;
                    cmd.Parameters.Add("@pNroVersion", SqlDbType.Int).Value = pNroVersion;
                    cmd.Parameters.Add("@pFotoTitulo", SqlDbType.VarChar).Value = pFototitulo;
                    cmd.Parameters.Add("@pFotoComment", SqlDbType.VarChar).Value = pFotocomment;
                    cmd.Parameters.Add("@pFotoConent", SqlDbType.VarChar).Value = pFotocontent;
                    cmd.Parameters.Add("@MsgTrans", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;

                    lineagg += ",2";
                    con.Open();

                    //Foto.Registro = cmd.ExecuteNonQuery();
                    Foto.status = cmd.Parameters["@MsgTrans"].Value.ToString();

                    con.Close();
                }
                lineagg += ",5";


                return Foto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}