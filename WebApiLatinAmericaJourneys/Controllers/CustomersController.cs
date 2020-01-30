using System.Linq;
using System.Web.Http;
using WebApiLatinAmericaJourneys.Models;
using Newtonsoft.Json;


using WebApiLatinAmericaJourneys.Repository.LatinAmericaJourneys;

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

            LoginAccess objLogin = new LoginAccess();
            var lstCliente = objLogin.LeerCliente(cli.EmailCliente, cli.PasswordCliente);
            bool estado;

            if (lstCliente.Count() > 0)
            {
                estado = true;

            }
            else
            {
                estado = false;
            }

            return Ok(estado);

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
