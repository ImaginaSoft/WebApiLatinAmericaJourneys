using System.Linq;
using System.Web.Http;
using WebApiLatinAmericaJourneys.Models;

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
        public IHttpActionResult GetAll()
        {
            var customersFake = new string[] { "customer-1", "customer-2", "customer-3", "customer-4" };
            return Ok(customersFake);
        }

        [HttpGet]
        [Route("login")]
        public IHttpActionResult LoginCliente(Cliente gg) {

            LoginAccess objLogin = new LoginAccess();
            var lstCliente = objLogin.LeerCliente(gg.EmailCliente, gg.PasswordCliente);
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
    }
}
