using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiLatinAmericaJourneys.Models
{

    public class ClienteResponse
    {
        public bool LoginSuccess { get; set; }
        public string CodCliente { get; set; }
        public string NomCliente { get; set; }
        public string ApePaterno { get; set; }
        public string ApeMaterno { get; set; }
        public string EmailCliente { get; set; }
        public char TipoIdioma { get; set; }
        public string ClaveCliente { get; set; }
        public string IDCliente { get; set; }
        
    }
}