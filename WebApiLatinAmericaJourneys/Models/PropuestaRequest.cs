using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiLatinAmericaJourneys.Models
{
    public class PropuestaRequest
    {
        public string EmailCliente { get; set; }
        public string PasswordCliente { get; set; }
        public string ZontaVenta { get; set; }
        public string CodCliente { get; set; }

    }
}