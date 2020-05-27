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
    public class LWallet
    {
        public IEnumerable<WalletResponse> LeerWallet(string pUser_id)
        {
            string lineagg = "0";
            try
            {
                List<WalletResponse> lstWallet = new List<WalletResponse>();
                lineagg += ",1";
                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {

                    SqlCommand cmd = new SqlCommand("dbo.APP_ObtieneWalletSaldo_S", con);
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
                        WalletResponse fWallet = new WalletResponse();

                        fWallet.Saldo = rdr["saldo"].ToString();
                        fWallet.Movs = new List<Movs>();

                        var ListaMovs = LeerMovs(pUser_id);
                        fWallet.Movs.AddRange(ListaMovs);
                        lstWallet.Add(item: fWallet);
                    }

                    lineagg += ",5";
                    con.Close();
                }
                return lstWallet;
            }
            catch (Exception ex)
            {
                throw new Exception { Source = lineagg };
            }

        }
        public IEnumerable<Movs> LeerMovs(string pUser_id)
        {
            string lineagg = "0";
            try
            {
                List<Movs> lstMovs = new List<Movs>();
                lineagg += ",1";
                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {

                    SqlCommand cmd = new SqlCommand("dbo.APP_ObtieneWallet_S", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CodCliente", SqlDbType.Int).Value = pUser_id;
                    lineagg += ",2";
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    lineagg += ",3";
                    while (rdr.Read())
                    {
                        lineagg += ",5";

                        Movs fMovs = new Movs
                        {
                            Fecha = rdr["Fecha"].ToString(),
                            Tipo = rdr["Tipo"].ToString(),
                            Importe = rdr["Importe"].ToString(),
                            Concepto = rdr["Concepto"].ToString(),
                            Ref = rdr["Ref"].ToString(),
                        };

                        lstMovs.Add(item: fMovs);

                    }

                    lineagg += ",5";
                    con.Close();
                }
                return lstMovs;
            }
            catch (Exception ex)
            {
                throw new Exception { Source = lineagg };
            }

        }


    }
}