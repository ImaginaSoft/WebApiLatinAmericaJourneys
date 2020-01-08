using System.Linq;
using System.Web.Http;
using WebApiLatinAmericaJourneys.Models;
using WebApiLatinAmericaJourneys.Repository.LatinAmericaJourneys;
using System.Web.Script.Serialization;
using System;
using Newtonsoft.Json;

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
        public IHttpActionResult LoginCliente(ClienteRequest objClienteRQ) {

            ClienteResponse objClienteRS = new ClienteResponse();
            LoginAccess objLogin = new LoginAccess();
            var lstCliente = objLogin.LeerCliente(objClienteRQ.EmailCliente, objClienteRQ.PasswordCliente);

            if (lstCliente.Count() > 0)
            {
                objClienteRS.LoginSuccess = true;
                objClienteRS.CodCliente = lstCliente.FirstOrDefault().CodCliente;
                objClienteRS.EmailCliente = lstCliente.FirstOrDefault().EmailCliente.Trim();
                objClienteRS.NomCliente = lstCliente.FirstOrDefault().NomCliente;
                objClienteRS.ApePaterno = lstCliente.FirstOrDefault().ApePaterno;
                objClienteRS.ApeMaterno = lstCliente.FirstOrDefault().ApeMaterno;

            }
            else
            {
                objClienteRS.LoginSuccess = false;
      
            }


            var json = new JavaScriptSerializer().Serialize(objClienteRS);

            return Ok(json);

        }

        [HttpGet]
        [Route("getProposal")]
        public IHttpActionResult VerPropuesta(ClienteRequest objClienteRQ) {

            LoginAccess objLogin = new LoginAccess();
            FichaPropuestaAccess objFichaPropuesta = new FichaPropuestaAccess();

          

                var lstPublicacion = objLogin.LeeUltimaPublicacion( Convert.ToInt32(objClienteRQ.CodigoCliente) );
                var lstProgramaGG = objFichaPropuesta.ObtenerListadoPropuesta(lstPublicacion.FirstOrDefault().NroPedido, lstPublicacion.FirstOrDefault().FlagIdioma);
                var json = new JavaScriptSerializer().Serialize(lstProgramaGG.Where(p=>p.StsPrograma.Equals("Sold") || p.StsPrograma.Equals("Vendido")));



            return Ok(json);
        }
    }
}
