using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiLatinAmericaJourneys.Models
{

    public class UltimaPublicacion
    {
        public int NroPedido { get; set; }
        public int NroPropuesta { get; set; }
        public int NroVersion { get; set; }
        public char FlagIdioma { get; set; }
        public int CantPropuestas { get; set; }

    }
}