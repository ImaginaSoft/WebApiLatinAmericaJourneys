using System.Linq;
using System.Web.Http;
using WebApiLatinAmericaJourneys.Models;
using Newtonsoft.Json;
using WebApiLatinAmericaJourneys.Repository.LatinAmericaJourneys;
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

using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace WebApiLatinAmericaJourneys.Controllers
{
    [Authorize]
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {
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

    }
}
