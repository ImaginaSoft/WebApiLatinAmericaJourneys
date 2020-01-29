using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using WebApiLatinAmericaJourneys.Models;


namespace WebApiLatinAmericaJourneys.Repository.LatinAmericaJourneys
{
    public class LPropuesta
    {

        public IEnumerable<EPropuesta> LeerPropuesta(string pCorreoCliente, string pPasswordCliente, string pZonaVenta)
        {
            string lineagg = "0";

            try
            {
                List<EPropuesta> lstPropuesta = new List<EPropuesta>();
                lineagg += ",1";
                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {

                    SqlCommand cmd = new SqlCommand("latinamericajourneys.LAJ_Propuesta_S", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CorreoCliente", SqlDbType.VarChar).Value = pCorreoCliente;
                    cmd.Parameters.Add("@PasswordCliente", SqlDbType.VarChar).Value = pPasswordCliente;
                    cmd.Parameters.Add("@CodZonaVta", SqlDbType.Char).Value = pZonaVenta;

                    lineagg += ",2";
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    lineagg += ",3";
                    while (rdr.Read())
                    {
                        lineagg += ",4";
                        //if (rdr["Email"].ToString().Trim() == pCorreoCliente.Trim() && rdr["ClaveCliente"].ToString().Trim() == pPasswordCliente.Trim())
                        //{
                            EPropuesta fPropuesta = new EPropuesta
                            {
                                FchSys         = rdr["FchSys"].ToString(),
                                FchInicio      = rdr["FchInicio"].ToString(),
                                NroPrograma    = rdr["NroPrograma"].ToString(),
                                StsPrograma    = rdr["StsPrograma"].ToString(),
                                DesPrograma    = rdr["DesPrograma"].ToString(),
                                CantDias       = rdr["CantDias"].ToString(),
                                EmailVendedor  = rdr["EmailVendedor"].ToString(),
                                KeyReg         = rdr["KeyReg"].ToString(),
                                NroPedido      = rdr["NroPedido"].ToString(),
                                NroPropuesta   = rdr["NroPropuesta"].ToString(),
                                NroVersion     = rdr["NroVersion"].ToString(),
                            };

                        lstPropuesta.Add(item: fPropuesta);

                        //}
                    }
                    lineagg += ",5";
                    con.Close();
                }
                return lstPropuesta;
            }
            catch (Exception ex)
            {
                throw new Exception { Source = lineagg };
            }

        }

    }
}