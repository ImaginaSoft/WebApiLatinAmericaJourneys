using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using WebApiLatinAmericaJourneys.Models;

namespace WebApiLatinAmericaJourneys.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        [HttpGet]
        [Route("echoping")]
        public IHttpActionResult EchoPing()
        {
            return Ok(true);
        }

        [HttpGet]
        [Route("echouser")]
        public IHttpActionResult EchoUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            return Ok($" IPrincipal-user: {identity.Name} - IsAuthenticated: {identity.IsAuthenticated}");
        }

        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(LoginRequest login)
        {

            List<TokenResponse> lstToken = new List<TokenResponse>();


            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            bool isCredentialValid = (login.Password == "Pentagrama2020$" || login.Password=="PentagramaLocal$");
            if (isCredentialValid)
            {
                var token = TokenGenerator.GenerateTokenJwt(login.Username);

                TokenResponse fToken = new TokenResponse
                {
                    tokenJWT = token
                };

                lstToken.Add(item: fToken);



                return Ok(lstToken.FirstOrDefault());
            }
            else
            {
                return Unauthorized();
            }
        }



    }
}
