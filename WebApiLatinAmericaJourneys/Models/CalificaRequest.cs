using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiLatinAmericaJourneys.Models
{

    public class CalificaRequest
    {
   
        public string NroPedido { get; set; }
        public string NroPropuesta { get; set; }
        public string NroVersion { get; set; }
        public string Stars { get; set; }
        public string Comment { get; set; }
            
    }
}