using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiLatinAmericaJourneys.Models
{
    public class TourResponse
    {

        public List<Programa> CabeceraTour{ get; set; }
        public List<Servicio> DetalleTour { get; set; }

    }
}