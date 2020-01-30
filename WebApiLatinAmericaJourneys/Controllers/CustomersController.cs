using System.Linq;
using System.Web.Http;
using WebApiLatinAmericaJourneys.Models;
using Newtonsoft.Json;


using WebApiLatinAmericaJourneys.Repository.LatinAmericaJourneys;
using System.Net.Http;
using System.Net;

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
        [Route("Propuesta")]
        public IHttpActionResult GetPropuesta(EPropuesta Pro) 
        {
            LPropuesta objPropuesta = new LPropuesta();
            var lstPropuesta = objPropuesta.LeerPropuesta(Pro.EmailCliente, Pro.PasswordCliente,Pro.ZontaVenta);
            
            return Ok(lstPropuesta.ToList());
        
        }

        [HttpGet]
        [Route("Itinerario")]
        public IHttpActionResult GetItinerario(EItinerario Iti)
        {
            LItinerario objItinerario = new LItinerario();
            var lstItinerario = objItinerario.LeerItinerario(Iti.NroPedido, Iti.NroPropuesta,Iti.NroVersion);

            return Ok(lstItinerario.ToList());

        }


    }
}
