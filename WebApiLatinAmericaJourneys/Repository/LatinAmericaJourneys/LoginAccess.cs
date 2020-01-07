using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using WebApiLatinAmericaJourneys.Models;

namespace WebApiLatinAmericaJourneys.Repository.LatinAmericaJourneys
{
    public class LoginAccess
    {

        public IEnumerable<Cliente> LeerCliente(string pCorreoCliente, string pPasswordCliente)
        {

            string lineagg = "0";

            try
            {

                List<Cliente> lstCliente = new List<Cliente>();
                lineagg += ",1";
                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {

                    SqlCommand cmd = new SqlCommand("latinamericajourneys.LAJ_LeeCliente_S", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CorreoCliente", SqlDbType.VarChar).Value = pCorreoCliente;
                    cmd.Parameters.Add("@PasswordCliente", SqlDbType.VarChar).Value = pPasswordCliente;

                    lineagg += ",2";
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    lineagg += ",3";
                    while (rdr.Read())
                    {
                        lineagg += ",4";
                        if (rdr["Email"].ToString().Trim() == pCorreoCliente.Trim() && rdr["ClaveCliente"].ToString().Trim() == pPasswordCliente.Trim())
                        {

                            Cliente fcliente = new Cliente
                            {

                                CodCliente = rdr["CodCliente"].ToString(),
                                NomCliente = rdr["NomCliente"].ToString(),
                                ApePaterno = rdr["Paterno"].ToString(),
                                ApeMaterno = rdr["Materno"].ToString(),
                                EmailCliente = rdr["Email"].ToString()

                            };

                            lstCliente.Add(item: fcliente);

                        }

                    }
                    lineagg += ",5";
                    con.Close();
                }

                return lstCliente;

            }
            catch (Exception ex)
            {
                //Bitacora.Current.Error<LoginAccess>(ex, new { lineagg });
                //return new List<Cliente> { new Cliente { EmailCliente = lineagg, NomCliente = ex.Message } };
                throw new Exception { Source = lineagg };

            }

        }



    }
}