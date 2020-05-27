using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using WebApiLatinAmericaJourneys.Models;

namespace WebApiLatinAmericaJourneys.Repository.LatinAmericaJourneys
{
    public class LFotoConsulta
    {

        public IEnumerable<Fotos> LeeFotoConsulta(int pNroPedido, int pNroPropuesta, int pNroVersion)
        {

            string lineagg = "0";
            FotoConsultaRequest Foto = new FotoConsultaRequest();
            Foto.status = "";
            try
            {

                List<Fotos> lstfotos = new List<Fotos>();

                lineagg += ",1";
                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {

                    SqlCommand cmd = new SqlCommand("dbo.APP_ImagenAlbum_L", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pNroPedido", SqlDbType.Int).Value = pNroPedido;
                    cmd.Parameters.Add("@pNroPropuesta", SqlDbType.Int).Value = pNroPropuesta;
                    cmd.Parameters.Add("@pNroVersion", SqlDbType.Int).Value = pNroVersion;
                    cmd.Parameters.Add("@MsgTrans", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;
                    lineagg += ",2";
                    con.Open();
                    cmd.ExecuteNonQuery();
                    
                    Foto.status = cmd.Parameters["@MsgTrans"].Value.ToString();

                    SqlDataReader rdr = cmd.ExecuteReader();
                    lineagg += ",3";
                    while (rdr.Read())
                    {
                        lineagg += ",4";


                        Fotos fPlantillaTour = new Fotos
                        {

                            IdFoto = rdr["IdFoto"].ToString(),
                            Url = rdr["Url"].ToString(),
                            Titulo = rdr["Titulo"].ToString(),
                            Comentario = rdr["Comentario"].ToString(),
                            Linkshare = rdr["Linkshare"].ToString()

                        };

                        lstfotos.Add(item: fPlantillaTour);



                    }
                    lineagg += ",5";
                    con.Close();
                }

                return lstfotos;

            }
            catch (Exception ex)
            {

                throw new Exception { Source = lineagg };

            }


        }


    }
}