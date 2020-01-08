using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using WebApiLatinAmericaJourneys.Models;
using WebApiLatinAmericaJourneys.Utility;


namespace WebApiLatinAmericaJourneys.Repository.LatinAmericaJourneys
{
    public class FichaPropuestaAccess
    {

        public IEnumerable<ProgramaResponse> ObtenerListadoPropuesta(int pNroPedido, char pFlagIdioma)
        {
            try
            {
                List<ProgramaResponse> lstfprograma = new List<ProgramaResponse>();

                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {
                    SqlCommand cmd = new SqlCommand();

                    if (pFlagIdioma.Equals(ConstantesWeb.CHR_IDIOMA_INGLES))
                    {
                        cmd = new SqlCommand("peru4me_new.P4I_Publica_S", con);

                    }
                    else
                    {
                        cmd = new SqlCommand("peru4me_new.P4E_Publica_S", con);
                    }

                    //SqlCommand cmd = new SqlCommand("VTA_PropuestaServicio_S_GG", con);
                    //SqlCommand cmd = new SqlCommand("P4E_Publica_S", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("@CodZonaVta", SqlDbType.Char, 3).Value = "PER";
                    cmd.Parameters.Add("@NroPedido", SqlDbType.Int).Value = pNroPedido;
                    //cmd.Parameters.Add("@NroPropuesta", SqlDbType.Int).Value = 6;

                    //cmd.Parameters.AddWithValue("@NroPedido", 162436);
                    //cmd.Parameters.AddWithValue("@NroPropuesta", 8);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        ProgramaResponse fprograma = new ProgramaResponse
                        {

                            FchSys = Convert.ToDateTime(rdr["FchSys"].ToString()),
                            NroPrograma = rdr["NroPrograma"].ToString().Trim(),
                            StsPrograma = rdr["StsPrograma"].ToString().Trim(),
                            DesPrograma = rdr["DesPrograma"].ToString().Trim(),
                            CantDias = Convert.ToInt32(rdr["CantDias"]),
                            KeyReg = rdr["KeyReg"].ToString(),
                            EmailVendedor = rdr["EmailVendedor"].ToString(),
                            //NombreVendedor = rdr["NombreVendedor"].ToString()

                        };

                        lstfprograma.Add(item: fprograma);
                    }

                    con.Close();
                }

                return lstfprograma;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}