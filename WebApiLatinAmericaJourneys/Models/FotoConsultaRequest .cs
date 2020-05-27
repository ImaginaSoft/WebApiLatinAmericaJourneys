using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiLatinAmericaJourneys.Models
{
    public class FotoConsultaRequest
    {

        public string NroPedido { get; set; }
        public string NroPropuesta { get; set; }
        public string NroVersion { get; set; }
        public string status { get; set; }

    }
}