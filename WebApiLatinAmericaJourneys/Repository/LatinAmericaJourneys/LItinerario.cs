using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using WebApiLatinAmericaJourneys.Models;

namespace WebApiLatinAmericaJourneys.Repository.LatinAmericaJourneys
{
    public class LItinerario
    {
        public IEnumerable<EItinerario> LeerItinerario(string pNroPedido, string pNroPropuesta, string pNroVersion)
        {

            string lineagg = "0";

            try
            {

                List<EItinerario> lstItinerario = new List<EItinerario>();
                lineagg += ",1";
                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {

                    SqlCommand cmd = new SqlCommand("latinamericajourneys.LAJ_Itinerario_S", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@NroPedido", SqlDbType.VarChar).Value = pNroPedido;
                    cmd.Parameters.Add("@NroPropuesta", SqlDbType.VarChar).Value = pNroPropuesta;
                    cmd.Parameters.Add("@NroVersion", SqlDbType.VarChar).Value = pNroVersion;

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

                            EItinerario fitinerario = new EItinerario
                            {
                                NroServicio = rdr["NroServicio"].ToString(),
                                CodTipoServicio = rdr["CodTipoServicio"].ToString(),
                                Dia = rdr["Dia"].ToString(),
                                FchInicio = rdr["FchInicio"].ToString(),
                                AnioInicio = rdr["AnioInicio"].ToString(),
                                MesInicio = rdr["MesInicio"].ToString(),
                                DiaInicio = rdr["DiaInicio"].ToString(),
                                Ciudad = rdr["Ciudad"].ToString(),
                                HoraServicio = rdr["HoraServicio"].ToString(),
                                DesServicioDet = rdr["DesServicioDet"].ToString(),
                                NroDia = rdr["NroDia"].ToString(),
                                NroOrden = rdr["NroOrden"].ToString(),
                                KeyReg = rdr["KeyReg"].ToString(),
                                CodUsuario = rdr["CodUsuario"].ToString(),
                                Notificacion = rdr["Notificacion"].ToString()
                            };

                            lstItinerario.Add(item: fitinerario);

                        //}

                    }
                    lineagg += ",5";
                    con.Close();
                }

                return lstItinerario;

            }
            catch (Exception ex)
            {
                throw new Exception { Source = lineagg };
            }

        }
    }
}