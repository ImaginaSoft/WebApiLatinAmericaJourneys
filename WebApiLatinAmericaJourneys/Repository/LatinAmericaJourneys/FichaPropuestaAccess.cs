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

        public IEnumerable<Programa> ObtenerListadoPropuesta(int pNroPedido, char pFlagIdioma)
        {
            try
            {
                List<Programa> lstfprograma = new List<Programa>();
                
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
                        Programa fprograma = new Programa
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

        public IEnumerable<Servicio> ObtenerListadoServiciosPropuesta(int pNroPedido, int pNroPropuesta, char pFlagIdioma)

        {
            try
            {
                List<Servicio> lstfservicio = new List<Servicio>();

                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {
                    //SqlCommand cmd = new SqlCommand("VTA_PropuestaServicio_S_GG", con);
                    SqlCommand cmd = new SqlCommand("peru4me_new.P4I_PropuestaServ_S", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("@NroPedido", SqlDbType.Int).Value = pNroPedido;
                    cmd.Parameters.Add("@NroPropuesta", SqlDbType.Int).Value = pNroPropuesta;
                    cmd.Parameters.Add("@FlagIdioma", SqlDbType.Char).Value = pFlagIdioma;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Servicio fservicio = new Servicio
                        {

                            NroServicio = Convert.ToInt32(rdr["NroServicio"]),
                            DesServicio = rdr["DesServicio"].ToString(),
                            CodTipoServicio = Convert.ToInt32(rdr["CodTipoServicio"]),
                            NroDia = rdr["NroDia"].ToString(),
                            DesServicioDet = rdr["DesServicioDet"].ToString(),
                            Ciudad = rdr["Ciudad"].ToString(),
                            HoraServicio = rdr["HoraServicio"].ToString(),
                            FchInicio = Convert.ToDateTime(rdr["FchInicio"].ToString()),
                            NombreEjecutiva = rdr["CodUsuario"].ToString(),
                            Resumen = rdr["Resumen"].ToString(),
                            ResuCaraEspe = rdr["ResuCaraEspe"].ToString(),
                            ResuComida = rdr["ResuComida"].ToString()
                            //FchInicio = (rdr["FchInicio"] =null)? string.Empty : Convert.ToDateTime(rdr["FchInicio"].ToString())

                        };

                        lstfservicio.Add(item: fservicio);
                    }

                    con.Close();
                }

                return lstfservicio;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<Servicio> ObtenerListadoServiciosPropuestaVersion(int pNroPedido, int pNroPropuesta, int pNroVersion, char pFlagIdioma)

        {
            try
            {
                List<Servicio> lstfservicio = new List<Servicio>();

                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {
                    //SqlCommand cmd = new SqlCommand("VTA_PropuestaServicio_S_GG", con);
                    SqlCommand cmd = new SqlCommand("peru4me_new.P4I_PropuestaServVersion_S", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("@NroPedido", SqlDbType.Int).Value = pNroPedido;
                    cmd.Parameters.Add("@NroPropuesta", SqlDbType.Int).Value = pNroPropuesta;
                    cmd.Parameters.Add("@NroVersion", SqlDbType.Int).Value = pNroVersion;
                    cmd.Parameters.Add("@FlagIdioma", SqlDbType.Char).Value = pFlagIdioma;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Servicio fservicio = new Servicio
                        {

                            NroServicio = Convert.ToInt32(rdr["NroServicio"]),
                            DesServicio = rdr["DesServicio"].ToString(),
                            CodTipoServicio = Convert.ToInt32(rdr["CodTipoServicio"]),
                            NroDia = rdr["NroDia"].ToString(),
                            DesServicioDet = rdr["DesServicioDet"].ToString(),
                            Ciudad = rdr["Ciudad"].ToString(),
                            HoraServicio = rdr["HoraServicio"].ToString(),
                            FchInicio = Convert.ToDateTime(rdr["FchInicio"].ToString()),
                            NombreEjecutiva = rdr["CodUsuario"].ToString(),
                            Resumen = rdr["Resumen"].ToString(),
                            ResuCaraEspe = rdr["ResuCaraEspe"].ToString(),
                            ResuComida = rdr["ResuComida"].ToString()
                            //FchInicio = (rdr["FchInicio"] =null)? string.Empty : Convert.ToDateTime(rdr["FchInicio"].ToString())

                        };

                        lstfservicio.Add(item: fservicio);
                    }

                    con.Close();
                }

                return lstfservicio;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}