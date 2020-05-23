using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using WebApiLatinAmericaJourneys.Models;
using WebApiLatinAmericaJourneys.ModelsWallet;

namespace WebApiLatinAmericaJourneys.Repository.Wallet
{
    public class LBeneficios
    {
        public IEnumerable<Beneficios> LeerBeneficios(string pUser_id)
        {
            string lineagg = "0";
            try
            {
                List<Beneficios> lstBeneficios = new List<Beneficios>();
                lineagg += ",1";
                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {

                    SqlCommand cmd = new SqlCommand("latinamericajourneys.LAJ_Beneficios_S", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@User_id", SqlDbType.VarChar).Value = pUser_id;
                    
                    lineagg += ",2";
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    lineagg += ",3";
                    while (rdr.Read())
                    {
                        lineagg += ",4";
                        Beneficios fBeneficio = new Beneficios();

                        fBeneficio.Titulo = rdr["Titulo"].ToString();
                        fBeneficio.Descripcion = rdr["Descripcion"].ToString();
                        fBeneficio.Condicion = rdr["Condicion"].ToString();
                        lstBeneficios.Add(item: fBeneficio);
                    }

                    lineagg += ",5";
                    con.Close();
                }
                return lstBeneficios;
            }
            catch (Exception ex)
            {
                throw new Exception { Source = lineagg };
            }

        }
      
    }
}