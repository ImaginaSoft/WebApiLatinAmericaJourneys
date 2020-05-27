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

                    SqlCommand cmd = new SqlCommand("dbo.APP_ObtieneBeneficio_S", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CodCliente", SqlDbType.VarChar).Value = pUser_id;
                    
                    lineagg += ",2";
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    lineagg += ",3";
                    while (rdr.Read())
                    {
                        lineagg += ",4";
                        Beneficios fBeneficio = new Beneficios();

                        fBeneficio.Titulo = rdr["TITULOBENEFICIO"].ToString();
                        fBeneficio.Descripcion = rdr["DESCRIPCION"].ToString();
                        fBeneficio.Condicion = rdr["CONDICION"].ToString();
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