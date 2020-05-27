using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiLatinAmericaJourneys.Models
{
    public class FotoRegistroRequest
    {
        //public string CodCliente { get; set; }
        public string NroPedido { get; set; }
        public string NroPropuesta { get; set; }
        public string NroVersion { get; set; }
        public string Fototitulo { get; set; }
        public string Fotocomment { get; set; }
        public string Fotocontent { get; set; }

    }
}