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


        public IEnumerable<ItinerarioViaje> LeerItinerarioViaje(string pNroPedido, string pNroPropuesta, string pNroVersion)
        {

            string lineagg = "0";

            try
            {

                List<ItinerarioViaje> lstItinerario = new List<ItinerarioViaje>();
                List<Lugares> lstLugares = new List<Lugares>();
                List<Actividades> lstActividades = new List<Actividades>();

                lineagg += ",1";
                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {

                    SqlCommand cmd = new SqlCommand("latinamericajourneys.LAJ_Itinerario_fecha_S", con);

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

                        ItinerarioViaje fitinerario = new ItinerarioViaje();

                        fitinerario.AnioInicio = rdr["Anio"].ToString();
                        fitinerario.MesInicio = rdr["Mes"].ToString();
                        fitinerario.DiaInicio = rdr["Dia"].ToString();
                        fitinerario.Lugares = new List<Lugares>();

                        var ListaLugares = LeerItinerarioLugar(Int32.Parse(pNroPedido), Int32.Parse(pNroPropuesta), Int32.Parse(pNroVersion), Int32.Parse(fitinerario.AnioInicio), Int32.Parse(fitinerario.MesInicio), Int32.Parse(fitinerario.DiaInicio));
                        fitinerario.Lugares.AddRange(ListaLugares);

                        foreach (var item in fitinerario.Lugares)
                        {
                            var strLugar = item.Lugar;

                            item.Actividades = new List<Actividades>();

                            var ListaActividades = LeerItinerarioActividad(Int32.Parse(pNroPedido), Int32.Parse(pNroPropuesta), Int32.Parse(pNroVersion), Int32.Parse(fitinerario.AnioInicio), Int32.Parse(fitinerario.MesInicio), Int32.Parse(fitinerario.DiaInicio), strLugar);
                            item.Actividades.AddRange(ListaActividades);
                        }

                        lstItinerario.Add(item: fitinerario);

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

        public IEnumerable<Lugares> LeerItinerarioLugar(int pNroPedido, int pNroPropuesta, int pNroVersion, int pAnio, int pMes, int pDia)
        {

            string lineagg = "0";

            try
            {

                List<ItinerarioViaje> lstItinerario = new List<ItinerarioViaje>();
                List<Lugares> lstLugares = new List<Lugares>();
                List<Actividades> lstActividades = new List<Actividades>();

                lineagg += ",1";
                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {

                    SqlCommand cmd = new SqlCommand("latinamericajourneys.LAJ_Itinerario_Lugar_S", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@NroPedido", SqlDbType.Int).Value = pNroPedido;
                    cmd.Parameters.Add("@NroPropuesta", SqlDbType.Int).Value = pNroPropuesta;
                    cmd.Parameters.Add("@NroVersion", SqlDbType.Int).Value = pNroVersion;
                    cmd.Parameters.Add("@Anio", SqlDbType.Int).Value = pAnio;
                    cmd.Parameters.Add("@Mes", SqlDbType.Int).Value = pMes;
                    cmd.Parameters.Add("@Dia", SqlDbType.Int).Value = pDia;

                    lineagg += ",2";
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    lineagg += ",3";
                    while (rdr.Read())
                    {
                        lineagg += ",5";

                        Lugares flugar = new Lugares
                        {
                            Lugar = rdr["Lugar"].ToString(),
                        };

                        lstLugares.Add(item: flugar);

                    }

                    lineagg += ",5";
                    con.Close();
                }

                return lstLugares;

            }
            catch (Exception ex)
            {
                throw new Exception { Source = lineagg };
            }

        }
        public IEnumerable<Actividades> LeerItinerarioActividad(int pNroPedido, int pNroPropuesta, int pNroVersion, int pAnio, int pMes, int pDia, string pLugar)
        {

            string lineagg = "0";

            try
            {
                List<Actividades> lstActividades = new List<Actividades>();

                lineagg += ",1";
                using (SqlConnection con = new SqlConnection(Data.Data.StrCnx_WebsSql))
                {

                    SqlCommand cmd = new SqlCommand("latinamericajourneys.LAJ_Itinerario_Actividad_S", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@NroPedido", SqlDbType.Int).Value = pNroPedido;
                    cmd.Parameters.Add("@NroPropuesta", SqlDbType.Int).Value = pNroPropuesta;
                    cmd.Parameters.Add("@NroVersion", SqlDbType.Int).Value = pNroVersion;
                    cmd.Parameters.Add("@Anio", SqlDbType.Int).Value = pAnio;
                    cmd.Parameters.Add("@Mes", SqlDbType.Int).Value = pMes;
                    cmd.Parameters.Add("@Dia", SqlDbType.Int).Value = pDia;
                    cmd.Parameters.Add("@Lugar", SqlDbType.VarChar).Value = pLugar;


                    lineagg += ",2";
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    lineagg += ",3";
                    while (rdr.Read())
                    {
                        lineagg += ",6";

                        Actividades fActividad = new Actividades
                        {
                            Hora = rdr["Hora"].ToString(),
                            Descripcion = rdr["Descripcion"].ToString(),
                        };

                        lstActividades.Add(item: fActividad);

                    }

                    return lstActividades;

                }

            }
            catch (Exception ex)
            {
                throw new Exception { Source = lineagg };
            }

        }
    }
}