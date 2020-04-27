using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using WebApiLatinAmericaJourneys.Models;


namespace WebApiLatinAmericaJourneys.Repository.LatinAmericaJourneys
{
    public class LCalifica
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

        public CalificaResponse    RegistrarCalificaViaje(int pNropedido,int pNroProuesta,int pNroVersion,int pStars, string pComentario)
        {
            string lineagg = "0";
           
            CalificaResponse Cali = new CalificaResponse(); 
            Cali.Registro = 0;

            try
            {
                List<CalificaRequest> lstPropuesta = new List<CalificaRequest>();
                lineagg += ",1";
                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {

                    SqlCommand cmd = new SqlCommand("latinamericajourneys.LAJ_CalificaViaje_I", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@NroPedido", SqlDbType.Int).Value = pNropedido;
                    cmd.Parameters.Add("@NroPropuesta", SqlDbType.Int).Value = pNroProuesta;
                    cmd.Parameters.Add("@NroVersion", SqlDbType.Int).Value = pNroVersion;
                    cmd.Parameters.Add("@Stars", SqlDbType.Int).Value = pStars;
                    cmd.Parameters.Add("@Comment", SqlDbType.VarChar).Value = pComentario;
                    cmd.Parameters.Add("@MsgTrans", SqlDbType.VarChar,100).Direction=ParameterDirection.Output;
                    
                    lineagg += ",2";
                    con.Open();

                    Cali.Registro = cmd.ExecuteNonQuery();
                    Cali.Status = cmd.Parameters["@MsgTrans"].Value.ToString();

                    con.Close();
                }
                    lineagg += ",5";
                    
                
                return Cali;
            }
            catch (Exception ex)
            {
                throw new Exception (ex.Message);
            }

        }

    }
}