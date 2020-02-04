using System.Linq;
using System.Web.Http;
using WebApiLatinAmericaJourneys.Models;
using Newtonsoft.Json;
using WebApiLatinAmericaJourneys.Repository.LatinAmericaJourneys;
using System.Net.Http;
using System.Net;
using System;
using System.Collections.Generic;

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

        [HttpGet]
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


        [HttpGet]
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

        [HttpGet]
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


    }
}
