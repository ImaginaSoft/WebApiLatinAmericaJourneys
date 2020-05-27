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
    public class Llogin
    {
        //public IEnumerable<PropuestaResponse> LeerPropuesta(int pCodCliente, string pZonaVenta)
        //{
        //    string lineagg = "0";

        //    try
        //    {
        //        List<PropuestaResponse> lstPropuesta = new List<PropuestaResponse>();
        //        lineagg += ",1";
        //        using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
        //        {

        //            SqlCommand cmd = new SqlCommand("latinamericajourneys.LAJ_Propuesta_S", con);

        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add("@CodCliente", SqlDbType.Int).Value = pCodCliente;
        //            cmd.Parameters.Add("@CodZonaVta", SqlDbType.Char).Value = pZonaVenta;

        //            lineagg += ",2";
        //            con.Open();
        //            cmd.ExecuteNonQuery();
        //            SqlDataReader rdr = cmd.ExecuteReader();
        //            lineagg += ",3";
        //            while (rdr.Read())
        //            {
        //                lineagg += ",4";
                        
        //                    PropuestaResponse fPropuesta = new PropuestaResponse
        //                    {
        //                        FchSys         = rdr["FchSys"].ToString(),
        //                        FchInicio      = rdr["FchInicio"].ToString(),
        //                        NroPrograma    = rdr["NroPrograma"].ToString(),
        //                        StsPrograma    = rdr["StsPrograma"].ToString(),
        //                        DesPrograma    = rdr["DesPrograma"].ToString(),
        //                        CantDias       = rdr["CantDias"].ToString(),
        //                        EmailVendedor  = rdr["EmailVendedor"].ToString(),
        //                        KeyReg         = rdr["KeyReg"].ToString(),
        //                        NroPedido      = rdr["NroPedido"].ToString(),
        //                        NroPropuesta   = rdr["NroPropuesta"].ToString(),
        //                        NroVersion     = rdr["NroVersion"].ToString(),
        //                    };

        //                lstPropuesta.Add(item: fPropuesta);

                         
        //            }
        //            lineagg += ",5";
        //            con.Close();
        //        }
        //        return lstPropuesta;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception { Source = lineagg };
        //    }

        //}


        //public CalificaResponse    RegistrarCalificaViaje(int pNropedido,int pNroProuesta,int pNroVersion,int pStars, string pComentario)
        //{
        //    string lineagg = "0";
           
        //    CalificaResponse Cali = new CalificaResponse(); 
        //    Cali.Registro = 0;

        //    try
        //    {
        //        List<CalificaRequest> lstPropuesta = new List<CalificaRequest>();
        //        lineagg += ",1";
        //        using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
        //        {

        //            SqlCommand cmd = new SqlCommand("latinamericajourneys.LAJ_CalificaViaje_I", con);

        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add("@NroPedido", SqlDbType.Int).Value = pNropedido;
        //            cmd.Parameters.Add("@NroPropuesta", SqlDbType.Int).Value = pNroProuesta;
        //            cmd.Parameters.Add("@NroVersion", SqlDbType.Int).Value = pNroVersion;
        //            cmd.Parameters.Add("@Stars", SqlDbType.Int).Value = pStars;
        //            cmd.Parameters.Add("@Comment", SqlDbType.VarChar).Value = pComentario;
        //            cmd.Parameters.Add("@MsgTrans", SqlDbType.VarChar,100).Direction=ParameterDirection.Output;
                    
        //            lineagg += ",2";
        //            con.Open();

        //            Cali.Registro = cmd.ExecuteNonQuery();
        //            Cali.Status = cmd.Parameters["@MsgTrans"].Value.ToString();

        //            con.Close();
        //        }
        //            lineagg += ",5";
                    
                
        //        return Cali;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception (ex.Message);
        //    }

        //}

        //public IEnumerable<LoginResponse> LeerUsuario(string pUid, string pPass)
        //{

        //    string lineagg = "0";

        //    try
        //    {

        //        List<LoginResponse> lstCliente = new List<LoginResponse>();
        //        lineagg += ",1";
        //        using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
        //        {

        //            SqlCommand cmd = new SqlCommand("latinamericajourneys.LAJ_LeeCliente_S", con);

        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add("@CorreoCliente", SqlDbType.VarChar).Value = pUid;
        //            cmd.Parameters.Add("@PasswordCliente", SqlDbType.VarChar).Value = pPass;

        //            lineagg += ",2";
        //            con.Open();
        //            cmd.ExecuteNonQuery();
        //            SqlDataReader rdr = cmd.ExecuteReader();
        //            lineagg += ",3";
        //            while (rdr.Read())
        //            {
        //                lineagg += ",4";
        //                if (rdr["Email"].ToString().Trim() == pUid.Trim() && rdr["ClaveCliente"].ToString().Trim() == pPass.Trim())
        //                {

        //                    LoginResponse fcliente = new LoginResponse
        //                    {

        //                        CodCliente = rdr["CodCliente"].ToString(),
        //                        NomCliente = rdr["NomCliente"].ToString(),
        //                        ApePaterno = rdr["Paterno"].ToString(),
        //                        ApeMaterno = rdr["Materno"].ToString(),
        //                        EmailCliente = rdr["Email"].ToString(),
        //                        TipoIdioma = Convert.ToChar(rdr["TipoIdioma"])

        //                    };

        //                    lstCliente.Add(item: fcliente);

        //                }

        //            }
        //            lineagg += ",5";
        //            con.Close();
        //        }

        //        return lstCliente;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception { Source = lineagg };

        //    }

        //}

        public IEnumerable<LoginWResponse> LeerUsuario(string pUid, string pPass)
        {
            string lineagg = "0";
            try
            {
                List<LoginWResponse> lstLogin = new List<LoginWResponse>();
                //List<Lugares> lstLugares = new List<Lugares>();
                //List<Actividades> lstActividades = new List<Actividades>();

                lineagg += ",1";
                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {

                    SqlCommand cmd = new SqlCommand("dbo.APP_ObtieneAccesos_S", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CodUsuario", SqlDbType.VarChar).Value = pUid;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = pPass;
                   
                    lineagg += ",2";
                    con.Open();
                    cmd.ExecuteNonQuery();

                    SqlDataReader rdr = cmd.ExecuteReader();
                    lineagg += ",3";
                    while (rdr.Read())
                    {
                        lineagg += ",4";
                        LoginWResponse fLogin = new LoginWResponse();

                        fLogin.Status = rdr["pStatus"].ToString();
                        fLogin.Msg = rdr["msg"].ToString();
                        fLogin.Perfil = new List<Perfil>();

                        var ListaPerfiles = LeerPerfil(pUid, pPass);
                        fLogin.Perfil.AddRange(ListaPerfiles);
                        lstLogin.Add(item: fLogin);
                    }

                    lineagg += ",5";
                    con.Close();
                }
                return lstLogin;
            }
            catch (Exception ex)
            {
                throw new Exception { Source = lineagg };
            }

        }

        public IEnumerable<Perfil> LeerPerfil(string pUid, string pPass)
        {
            string lineagg = "0";
            try
            {
                List<Perfil> lstPerfil = new List<Perfil>();
                lineagg += ",1";
                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {

                    SqlCommand cmd = new SqlCommand("dbo.APP_ObtieneAccesos_S", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CodUsuario", SqlDbType.VarChar).Value = pUid;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = pPass;
                
                    lineagg += ",2";
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    lineagg += ",3";
                    while (rdr.Read())
                    {
                        lineagg += ",5";

                        Perfil fPerfil = new Perfil
                        {
                            Id = rdr["ID"].ToString(),
                            Nombre = rdr["Nombre"].ToString(),
                            Apellidos = rdr["Apellidos"].ToString(),
                            Saldo = rdr["Saldo"].ToString(),
                        };

                        lstPerfil.Add(item: fPerfil);

                    }

                    lineagg += ",5";
                    con.Close();
                }

                return lstPerfil;

            }
            catch (Exception ex)
            {
                throw new Exception { Source = lineagg };
            }

        }
    }
}