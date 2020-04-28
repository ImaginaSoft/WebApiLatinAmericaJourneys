using System.Linq;
using System.Web.Http;
using WebApiLatinAmericaJourneys.Models;
using WebApiLatinAmericaJourneys.ModelsWallet;
using Newtonsoft.Json;
using WebApiLatinAmericaJourneys.Repository.LatinAmericaJourneys;
using WebApiLatinAmericaJourneys.Repository.Wallet;
using WebApiLatinAmericaJourneys.Repository.Data;
using System.Net.Http;
using System.Net;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Configuration;
using ws = WebApiLatinAmericaJourneys.ws_SendGridEmail;
using System.Security.Cryptography;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using WebApiLatinAmericaJourneys.Utility;

namespace WebApiLatinAmericaJourneys.Controllers
{
    [Authorize]
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {

        FichaPropuestaAccess objPropuesta = new FichaPropuestaAccess();
        Servicio objServicio = new Servicio();

        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            var customerFake = "customer-fake";
            return Ok(customerFake);
        }

        [HttpGet]
        [Route("demo")]
        public IHttpActionResult GetAll()
        {
            var customersFake = new string[] { "customer-1", "customer-2", "customer-3", "customer-4" };
            return Ok(customersFake);
        }

        [HttpPost]
        [Route("login")]
        public IHttpActionResult LoginCliente(ClienteRequest cli) {

            ClienteResponse objClienteRS = new ClienteResponse();

            LoginAccess objLogin = new LoginAccess();
            var lstCliente = objLogin.LeerCliente(cli.EmailCliente, cli.PasswordCliente);

            if (lstCliente.Count() > 0)
            {
                objClienteRS.LoginSuccess = true;
                objClienteRS.CodCliente = lstCliente.FirstOrDefault().CodCliente;
                objClienteRS.EmailCliente = lstCliente.FirstOrDefault().EmailCliente.Trim();
                objClienteRS.NomCliente = lstCliente.FirstOrDefault().NomCliente;
                objClienteRS.ApePaterno = lstCliente.FirstOrDefault().ApePaterno;
                objClienteRS.ApeMaterno = lstCliente.FirstOrDefault().ApeMaterno;
                objClienteRS.TipoIdioma = lstCliente.FirstOrDefault().TipoIdioma;
            }
            else
            {
                objClienteRS.LoginSuccess = false;
                var message = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("No se encontro el cliente.")
                };
                throw new HttpResponseException(message);
            }

            return Ok(objClienteRS);

        }

        [HttpPost]
        [Route("GetPropuesta")]
        public IHttpActionResult GetPropuesta(PropuestaRequest Pro) 
        {
            LPropuesta objPropuesta = new LPropuesta();
            var lstPropuesta = objPropuesta.LeerPropuesta(Convert.ToInt32(Pro.CodCliente), Pro.ZontaVenta);

            if (lstPropuesta.Count() > 0)
            {

                return Ok(lstPropuesta.ToList());

            }
            else
            {
               
                var message = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("No se encontro la Propuesta.")
                };
                throw new HttpResponseException(message);
            }

            

        }

        [HttpPost]
        [Route("GetItinerario")]
        public IHttpActionResult GetItinerario(EItinerario Iti)
        {
            LItinerario objItinerario = new LItinerario();
            var lstItinerario = objItinerario.LeerItinerario(Iti.NroPedido, Iti.NroPropuesta,Iti.NroVersion);

            if (lstItinerario.Count() > 0)
            {

                return Ok(lstItinerario.ToList());

            }
            else
            {

                var message = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("No se encontro el Itinerario.")
                };

                throw new HttpResponseException(message);
            }
            

        }

        [HttpPost]
        [Route("GetURL")]
        public IHttpActionResult GetURL(ClienteRequest cli) 
        {
            string URL = Data.StrUrl;

            ClienteResponse objClienteRS = new ClienteResponse();

            LoginAccess objLogin = new LoginAccess();
            var lstCliente = objLogin.LeeIDCliente(Int32.Parse(cli.CodigoCliente));

            if (lstCliente.Count() > 0)
            {
                 
                objClienteRS.IDCliente = lstCliente.FirstOrDefault().IDCliente;
                URL = URL + "/" + objClienteRS.IDCliente;
            }
            else
            {
                
                var message = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("No se encontro el IDcliente.")
                };
                throw new HttpResponseException(message);
            }

            return Ok(URL);

        }

        [HttpPost]
        [Route("GetAcceso")]
        public IHttpActionResult GetAcceso(AccesoRequest acc)
        {
            string Mensaje;
            ClienteResponse objClienteRS = new ClienteResponse();
            LoginAccess objAcceso = new LoginAccess();
            var lstCliente = objAcceso.LeerCorreo(acc.EmailCliente);

            if (lstCliente.Count() > 0)
            {
                
                objClienteRS.ClaveCliente = lstCliente.FirstOrDefault().ClaveCliente.Trim();
                
                acc.Cuerpo = acc.Cuerpo + " " + objClienteRS.ClaveCliente;

                EnviarCorreoSendGrid(acc.NombreEmisor, acc.EmailEmisor, acc.EmailCliente, acc.Asunto, acc.Cuerpo);
                Mensaje = "OK, Se envio el Correo Satisfactoriamente";
                return Ok(Mensaje);
            }
            else
            {
                objClienteRS.LoginSuccess = false;
                var message = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("No se encontro El Correo en la Base de Datos... Verifique por favor")
                };
                throw new HttpResponseException(message);
            }
        }

        private static string Encriptar(string cadena)
        {
            using (System.Security.Cryptography.MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(cadena));

                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }

        public void EnviarCorreoSendGrid(string pNombreEmisor, string pCorreoEmisor, string pDestinatarios, string pAsunto, string pCuerpo)
        {
            var send = new ws.wsMailsSoapClient();
            var resultado = new RespuestaEmail();

            try
            {
                // *************************************************************************
                // Para envio de correos sin adjunto
                // *************************************************************************

                var enviarSinAdjunto = send.EnviarCorreo(
                    new ws.Autenticacion
                    {
                        InWebId = ws.TipoWeb.Mozart,
                        StUsuario = Encriptar(WebConfigurationManager.AppSettings["key_usuarioTk"].ToString()),
                        StClave = Encriptar(WebConfigurationManager.AppSettings["key_claveTk"].ToString())
                    },
                    new ws.Correo
                    {
                        NombreEmisor = pNombreEmisor,
                        CorreoEmisor = pCorreoEmisor,
                        Destinatarios = pDestinatarios,
                        Asunto = pAsunto,
                        CuerpoHtml = pCuerpo
                    });
                // *************************************************************************

                resultado.Valor = enviarSinAdjunto.Valor;
            }
            catch (Exception ex)
            {

                resultado.Tipo = TipoRespuesta.Error;
                resultado.Valor = ex.Message;

            }

        }

        [HttpPost]
        [Route("GetImageTour")]
        public IHttpActionResult GetImageTour(PlantillaTourRequest Pla)
        {


            PlantillaTourResponse objPlantillaTour = new PlantillaTourResponse();
            LoginAccess objPlantilla = new LoginAccess();

            var lstImagenTour = objPlantilla.LeeImageTour(Int32.Parse(Pla.NroPedido),Int32.Parse(Pla.NroPropuesta),Int32.Parse(Pla.NroVersion));

            if (lstImagenTour.Count() > 0)
            {

                return Ok(lstImagenTour.ToList());

            }
            else
            {

                var message = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("No se encontro la Plantilla para el Tour.")
                };

                throw new HttpResponseException(message);
            }

        }
        
        [HttpPost]
        [Route("GetPropuestaViaje")]
        public IHttpActionResult GetPropuestaViaje(PropuestaRequest Pro)
        {
            LPropuesta objPropuesta = new LPropuesta();
            LoginAccess objPlantilla = new LoginAccess();
            LItinerario objItinerario = new LItinerario();

            ItinerarioResponse objTourResponse = new ItinerarioResponse();

            var lstPropuesta = objPropuesta.LeerPropuestaViaje(Convert.ToInt32(Pro.CodCliente));
            var lstBanner = objPlantilla.LeeImage(Int32.Parse(lstPropuesta.FirstOrDefault().NroPedido), Int32.Parse(lstPropuesta.FirstOrDefault().NroPropuesta), Int32.Parse(lstPropuesta.FirstOrDefault().NroVersion));
            var lstItinerario = objItinerario.LeerItinerarioViaje(lstPropuesta.FirstOrDefault().NroPedido, lstPropuesta.FirstOrDefault().NroPropuesta, lstPropuesta.FirstOrDefault().NroVersion);

            if (lstPropuesta.Count() > 0)
            {
                objTourResponse.Main = lstPropuesta.ToList();
                objTourResponse.Banner = lstBanner.ToList();
                objTourResponse.Itinerario = lstItinerario.ToList();

                return Ok(objTourResponse);

            }
            else
            {

                var message = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("No se encontro la Propuesta.")
                };
                throw new HttpResponseException(message);
            }

            //var json = new JavaScriptSerializer().Serialize(objTourResponse);
            //string output =  JsonConvert.SerializeObject(objTourResponse);

            //string json = JsonConvert.SerializeObject(objTourResponse, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });


        }

        [HttpPost]
        [Route("GetCalificaViaje")]
        public IHttpActionResult GetCalificaViaje(CalificaRequest Cal)
        {
            LCalifica objCalifica = new LCalifica();
            List<CalificaResponse> lstToken = new List<CalificaResponse>();

            var Registro = objCalifica.RegistrarCalificaViaje(Int32.Parse(Cal.NroPedido), Int32.Parse(Cal.NroPropuesta), Int32.Parse(Cal.NroVersion),Int32.Parse(Cal.Stars),Cal.Comment);

            if (Registro.Registro > 0)
            {

                CalificaResponse fToken = new CalificaResponse
                {
                    Status = Registro.Status
                };

                lstToken.Add(item: fToken);

                return Ok(lstToken.FirstOrDefault());


            }
            else
            {

                var message = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    //Content = new StringContent("No se ha registro la Califica, Verifique por favor.")
                    Content= new StringContent(Registro.Status)
                };

                throw new HttpResponseException(message);
            }

        }

        //************************************************************************************************************************
        //************************************************************************************************************************
        //DIVISION PARA IDENTIFICAR  LOS METODOS PARA LA NUEVA FUNCIONALIDAD DE LA MEMBRESIA WALLET  2704/2020 JLFA DESARROLLADOR
        //************************************************************************************************************************

        [HttpPost]
        [Route("GetLogin")]
        public IHttpActionResult GetLogin(LoginWRequest Acc)
        {
            Llogin objLoginW = new Llogin();
            //List<LoginWResponse> lstLogin = new List<LoginWResponse>();

            var lstPropuesta = objLoginW.LeerUsuario(Acc.Uid,Acc.Pass);

            if (lstPropuesta.Count() > 0)
            {
                ////lstLogin = lstPropuesta.ToList();

                return Ok(lstPropuesta);

            }
            else
            {

                var message = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("No se encontro la Propuesta.")
                };
                throw new HttpResponseException(message);
            }

        }


        [HttpPost]
        [Route("GetWallet")]
        public IHttpActionResult GetWallet(WalletRequest Wall)
        {
            LWallet objWallet = new LWallet();
            
            var lstWallet = objWallet.LeerWallet(Wall.User_id);

            if (lstWallet.Count() > 0)
            {
                return Ok(lstWallet);
            }
            else
            {

                var message = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("No se encontra los movimientos registrados.")
                };
                throw new HttpResponseException(message);
            }

        }

        [HttpPost]
        [Route("GetBeneficios")]
        public IHttpActionResult GetBeneficios(BeneficiosRequest Ben)
        {
            LBeneficios objBeneficios = new LBeneficios();

            var lstBeneficios = objBeneficios.LeerBeneficios(Ben.User_id);

            if (lstBeneficios.Count() > 0)
            {
                return Ok(lstBeneficios);
            }
            else
            {

                var message = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("No se encontra Los Beneficios registrados.")
                };
                throw new HttpResponseException(message);
            }

        }

        [HttpPost]
        [Route("GetEncuesta")]
        public IHttpActionResult GetEncuesta(EncuestaRequest Enc)
        {
            LEncuesta objEncuesta = new LEncuesta();

            var lstEncuestas = objEncuesta.LeerEncuesta(Enc.User_id,Enc.Pregunta,Enc.Respuesta);

            if (lstEncuestas.Count() > 0)
            {
                return Ok(lstEncuestas);
            }
            else
            {

                var message = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("No se encontra Los Beneficios registrados.")
                };
                throw new HttpResponseException(message);
            }

        }

        [HttpPost]
        [Route("GetNuevoViaje")]
        public IHttpActionResult GetNuevoViaje(NuevoViajeRequest Nev)
        {
            LNuevoViaje objNuevoViaje = new LNuevoViaje();

            var lstNuevoViaje = objNuevoViaje.LeerNuevoViaje(Nev.User_id, Nev.Planes, Nev.Fecha,Nev.Nombre,Nev.Edad);

            if (lstNuevoViaje.Count() > 0)
            {
                return Ok(lstNuevoViaje);
            }
            else
            {

                var message = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("No se encontra Los Beneficios registrados.")
                };
                throw new HttpResponseException(message);
            }

        }































        ////--SE DESARROLLO EN UN PRINCIPIO PARA MOSTRAR EL ITINERARIO DEL CLIENTE 
        ///// -- JOSE LUIS FERNANDEZ   SE TERMINO EL 17/02/2020
        //
        //[HttpGet]
        //[Route("getTour")]
        //public IHttpActionResult VerPropuesta(ClienteRequest objClienteRQ)
        //{

        //    LoginAccess objLogin = new LoginAccess();
        //    FichaPropuestaAccess objFichaPropuesta = new FichaPropuestaAccess();
        //    List<Servicio> lstPropuestaDetalle = new List<Servicio>();

        //    TourResponse objTourResponse = new TourResponse();

        //    var lstPublicacion = objLogin.LeeUltimaPublicacion(Convert.ToInt32(objClienteRQ.CodigoCliente));
        //    var lstProgramaGG = objFichaPropuesta.ObtenerListadoPropuesta(lstPublicacion.FirstOrDefault().NroPedido, lstPublicacion.FirstOrDefault().FlagIdioma);
        //    var lstProgramaVendido = lstProgramaGG.Where(p => p.StsPrograma.Equals(ConstantesWeb.STR_ESTADO_PROGRAMA_I) || p.StsPrograma.Equals(ConstantesWeb.STR_ESTADO_PROGRAMA_E));


        //    string nroPedido = lstProgramaVendido.FirstOrDefault().KeyReg.Substring(0, 6);
        //    string nroPropuesta = lstProgramaVendido.FirstOrDefault().KeyReg.Substring(8, 2);
        //    string nroVersion = lstProgramaVendido.FirstOrDefault().KeyReg.Substring(10, 2);


        //    lstPropuestaDetalle = VerPropuestaDetalle(lstProgramaVendido.FirstOrDefault().NroPrograma, nroPedido, nroPropuesta, nroVersion, lstPublicacion.FirstOrDefault().FlagIdioma);

        //    objTourResponse.CabeceraTour = lstProgramaGG.Where(p => p.StsPrograma.Equals(ConstantesWeb.STR_ESTADO_PROGRAMA_I) || p.StsPrograma.Equals(ConstantesWeb.STR_ESTADO_PROGRAMA_E)).ToList();
        //    objTourResponse.DetalleTour = lstPropuestaDetalle;

        //    var json = new JavaScriptSerializer().Serialize(objTourResponse);
        //    string output = JsonConvert.SerializeObject(objTourResponse);


        //    return Ok(output);
        //}

        //public List<Servicio> VerPropuestaDetalle(string pNroPrograma, string pNroPedido, string pNroPropuesta, string pNroVersion, char pFlagIdioma)
        //{
        //    List<Servicio> lstPropuestaDetalle = new List<Servicio>();
        //    List<Servicio> lstPropuestaDetalleFinal = new List<Servicio>();


        //    if (pNroVersion.Trim() == "0")
        //    {

        //        lstPropuestaDetalle = objPropuesta.ObtenerListadoServiciosPropuesta(Convert.ToInt32(pNroPedido), Convert.ToInt32(pNroPrograma), pFlagIdioma).ToList();
        //    }
        //    else
        //    {
        //        lstPropuestaDetalle = objPropuesta.ObtenerListadoServiciosPropuestaVersion(Convert.ToInt32(pNroPedido), Convert.ToInt32(pNroPrograma), Convert.ToInt32(pNroVersion), pFlagIdioma).ToList();

        //    }
        //    var agrupacion = from p in lstPropuestaDetalle group p by p.NroDia into grupo select grupo;

        //    foreach (var item in agrupacion)
        //    {

        //        string nroDia = string.Empty;
        //        string servDetAgrupado = string.Empty;
        //        string desServicio = string.Empty;
        //        string ciudad = string.Empty;
        //        string horaServicio = string.Empty;
        //        //DateTime fchInicio = string.Empty;
        //        int i = 0;
        //        int cantidad = agrupacion.Count();

        //        Servicio[] arrayServicio = new Servicio[cantidad];

        //        foreach (var itemAgrupado in item)
        //        {


        //            if (itemAgrupado.CodTipoServicio == 2)
        //            {

        //                servDetAgrupado = servDetAgrupado + itemAgrupado.DesServicioDet.Trim() + "↕" + itemAgrupado.NroServicio + "|";
        //            }
        //            else
        //            {

        //                if (itemAgrupado.HoraServicio.Trim().Equals(string.Empty) || itemAgrupado.HoraServicio == null)
        //                {


        //                    servDetAgrupado = servDetAgrupado + itemAgrupado.DesServicioDet + "|";

        //                }
        //                else
        //                {
        //                    servDetAgrupado = servDetAgrupado + "<div class=\"prop-info\"><div class=\"info\"><i class=\"icon icon-time\"></i>" + itemAgrupado.HoraServicio + "</div></div>" + itemAgrupado.DesServicioDet + "|";
        //                }


        //            }


        //            if (itemAgrupado.DesServicio != "")
        //            {

        //                desServicio = itemAgrupado.DesServicio.FirstOrDefault().ToString();

        //            }
        //            objServicio.NroDia = itemAgrupado.NroDia;
        //            objServicio.DesServicio = desServicio;
        //            objServicio.DesServicioDet = servDetAgrupado;
        //            objServicio.Ciudad = itemAgrupado.Ciudad;
        //            objServicio.HoraServicio = itemAgrupado.HoraServicio;
        //            objServicio.CodTipoServicio = itemAgrupado.CodTipoServicio;
        //            objServicio.NroServicio = itemAgrupado.NroServicio;
        //            objServicio.NombreEjecutiva = itemAgrupado.NombreEjecutiva;
        //            objServicio.Resumen = itemAgrupado.Resumen;
        //            objServicio.ResuCaraEspe = itemAgrupado.ResuCaraEspe;
        //            objServicio.ResuComida = itemAgrupado.ResuComida;
        //            //objServicio.FchInicio = itemAgrupado.FchInicio;

        //        }


        //        var servicioDetAgrupado = new Servicio
        //        {

        //            NroDia = item.FirstOrDefault().NroDia,
        //            DesServicio = item.FirstOrDefault().DesServicio,
        //            DesServicioDet = servDetAgrupado,
        //            Ciudad = item.FirstOrDefault().Ciudad,
        //            HoraServicio = item.FirstOrDefault().HoraServicio,
        //            FchInicio = item.FirstOrDefault().FchInicio,
        //            NroServicio = item.FirstOrDefault().NroServicio,
        //            CodTipoServicio = item.FirstOrDefault().CodTipoServicio,
        //            NombreEjecutiva = item.FirstOrDefault().NombreEjecutiva,
        //            Resumen = item.FirstOrDefault().Resumen,
        //            ResuCaraEspe = item.FirstOrDefault().ResuCaraEspe,
        //            ResuComida = item.FirstOrDefault().ResuComida
        //        };


        //        lstPropuestaDetalleFinal.Add(servicioDetAgrupado);

        //    }


        //    return lstPropuestaDetalleFinal;

        //}
        //// -----TERMINO DEL DESARROLLO  SE TERMINO EL 17/02/2020


    }
}
