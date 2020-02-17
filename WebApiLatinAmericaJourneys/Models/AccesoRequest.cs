using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiLatinAmericaJourneys.Models
{
    public class AccesoRequest
    {
        
        public string NombreEmisor { get; set; }
        public string EmailEmisor { get; set; }
        public string EmailCliente { get; set; }
        public string Asunto { get; set; }
        public string Cuerpo { get; set; }
    }
}