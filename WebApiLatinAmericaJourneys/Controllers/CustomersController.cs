using System.Linq;
using System.Web.Http;
using WebApiLatinAmericaJourneys.Models;
using WebApiLatinAmericaJourneys.Repository.LatinAmericaJourneys;
using WebApiLatinAmericaJourneys.Utility;
using System.Web.Script.Serialization;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using CustomLog;


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
        public IHttpActionResult GetAll()
        {
            var customersFake = new string[] { "customer-1", "customer-2", "customer-3", "customer-4" };
            return Ok(customersFake);
        }

        [HttpGet]
        [Route("login")]
        public IHttpActionResult LoginCliente(ClienteRequest objClienteRQ)
        {

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
                var message = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("No se encontro el cliente.")
                };
                throw new HttpResponseException(message);
            }


            var json = new JavaScriptSerializer().Serialize(objClienteRS);

            return Ok(json);

        }

        [HttpGet]
        [Route("getTour")]
        public IHttpActionResult VerPropuesta(ClienteRequest objClienteRQ)
        {

            LoginAccess objLogin = new LoginAccess();
            FichaPropuestaAccess objFichaPropuesta = new FichaPropuestaAccess();
            List<Servicio> lstPropuestaDetalle = new List<Servicio>();

            TourResponse objTourResponse = new TourResponse();

            try {
                var lstPublicacion = objLogin.LeeUltimaPublicacion(Convert.ToInt32(objClienteRQ.CodigoCliente));
                var lstProgramaGG = objFichaPropuesta.ObtenerListadoPropuesta(lstPublicacion.FirstOrDefault().NroPedido, lstPublicacion.FirstOrDefault().FlagIdioma);
                var lstProgramaVendido = lstProgramaGG.Where(p => p.StsPrograma.Equals(ConstantesWeb.STR_ESTADO_PROGRAMA_I) || p.StsPrograma.Equals(ConstantesWeb.STR_ESTADO_PROGRAMA_E));

                if (lstProgramaVendido.Count() <=0) {
                    var message = new HttpResponseMessage(HttpStatusCode.BadRequest) {

                        Content = new StringContent("No hay un programa vendido.")

                    };
                    throw new HttpResponseException(message);
                }

                string nroPedido = lstProgramaVendido.FirstOrDefault().KeyReg.Substring(0, 6);
                string nroPropuesta = lstProgramaVendido.FirstOrDefault().KeyReg.Substring(8, 2);
                string nroVersion = lstProgramaVendido.FirstOrDefault().KeyReg.Substring(10, 2);


                lstPropuestaDetalle = VerPropuestaDetalle(lstProgramaVendido.FirstOrDefault().NroPrograma, nroPedido, nroPropuesta, nroVersion, lstPublicacion.FirstOrDefault().FlagIdioma);

                objTourResponse.CabeceraTour = lstProgramaGG.Where(p => p.StsPrograma.Equals(ConstantesWeb.STR_ESTADO_PROGRAMA_I) || p.StsPrograma.Equals(ConstantesWeb.STR_ESTADO_PROGRAMA_E)).ToList();
                objTourResponse.DetalleTour = lstPropuestaDetalle;

                var json = new JavaScriptSerializer().Serialize(objTourResponse);
                string output = JsonConvert.SerializeObject(objTourResponse);


                return Ok(output);
            }
            catch (HttpResponseException ex) {

                //Bitacora.Current.Error<CustomersController>(ex, new { "g" });
                throw;
                
            }



        }

        public List<Servicio> VerPropuestaDetalle(string pNroPrograma, string pNroPedido, string pNroPropuesta, string pNroVersion, char pFlagIdioma)
        {

            List<Servicio> lstPropuestaDetalle = new List<Servicio>();
            List<Servicio> lstPropuestaDetalleFinal = new List<Servicio>();


            if (pNroVersion.Trim() == "0")
            {

                lstPropuestaDetalle = objPropuesta.ObtenerListadoServiciosPropuesta(Convert.ToInt32(pNroPedido), Convert.ToInt32(pNroPrograma), pFlagIdioma).ToList();
            }
            else
            {
                lstPropuestaDetalle = objPropuesta.ObtenerListadoServiciosPropuestaVersion(Convert.ToInt32(pNroPedido), Convert.ToInt32(pNroPrograma), Convert.ToInt32(pNroVersion), pFlagIdioma).ToList();

            }
            var agrupacion = from p in lstPropuestaDetalle group p by p.NroDia into grupo select grupo;

            foreach (var item in agrupacion)
            {

                string nroDia = string.Empty;
                string servDetAgrupado = string.Empty;
                string desServicio = string.Empty;
                string ciudad = string.Empty;
                string horaServicio = string.Empty;
                //DateTime fchInicio = string.Empty;
                int i = 0;
                int cantidad = agrupacion.Count();

                Servicio[] arrayServicio = new Servicio[cantidad];

                foreach (var itemAgrupado in item)
                {


                    if (itemAgrupado.CodTipoServicio == 2)
                    {

                        servDetAgrupado = servDetAgrupado + itemAgrupado.DesServicioDet.Trim() + "↕" + itemAgrupado.NroServicio + "|";
                    }
                    else
                    {

                        if (itemAgrupado.HoraServicio.Trim().Equals(string.Empty) || itemAgrupado.HoraServicio == null)
                        {


                            servDetAgrupado = servDetAgrupado + itemAgrupado.DesServicioDet + "|";

                        }
                        else
                        {
                            servDetAgrupado = servDetAgrupado + "<div class=\"prop-info\"><div class=\"info\"><i class=\"icon icon-time\"></i>" + itemAgrupado.HoraServicio + "</div></div>" + itemAgrupado.DesServicioDet + "|";
                        }


                    }


                    if (itemAgrupado.DesServicio != "")
                    {

                        desServicio = itemAgrupado.DesServicio.FirstOrDefault().ToString();

                    }
                    objServicio.NroDia = itemAgrupado.NroDia;
                    objServicio.DesServicio = desServicio;
                    objServicio.DesServicioDet = servDetAgrupado;
                    objServicio.Ciudad = itemAgrupado.Ciudad;
                    objServicio.HoraServicio = itemAgrupado.HoraServicio;
                    objServicio.CodTipoServicio = itemAgrupado.CodTipoServicio;
                    objServicio.NroServicio = itemAgrupado.NroServicio;
                    objServicio.NombreEjecutiva = itemAgrupado.NombreEjecutiva;
                    objServicio.Resumen = itemAgrupado.Resumen;
                    objServicio.ResuCaraEspe = itemAgrupado.ResuCaraEspe;
                    objServicio.ResuComida = itemAgrupado.ResuComida;
                    //objServicio.FchInicio = itemAgrupado.FchInicio;

                }


                var servicioDetAgrupado = new Servicio
                {

                    NroDia = item.FirstOrDefault().NroDia,
                    DesServicio = item.FirstOrDefault().DesServicio,
                    DesServicioDet = servDetAgrupado,
                    Ciudad = item.FirstOrDefault().Ciudad,
                    HoraServicio = item.FirstOrDefault().HoraServicio,
                    FchInicio = item.FirstOrDefault().FchInicio,
                    NroServicio = item.FirstOrDefault().NroServicio,
                    CodTipoServicio = item.FirstOrDefault().CodTipoServicio,
                    NombreEjecutiva = item.FirstOrDefault().NombreEjecutiva,
                    Resumen = item.FirstOrDefault().Resumen,
                    ResuCaraEspe = item.FirstOrDefault().ResuCaraEspe,
                    ResuComida = item.FirstOrDefault().ResuComida
                };


                lstPropuestaDetalleFinal.Add(servicioDetAgrupado);

            }


            return lstPropuestaDetalleFinal;


        }
    }
}
