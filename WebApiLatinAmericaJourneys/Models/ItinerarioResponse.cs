using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiLatinAmericaJourneys.Models
{
    public class ItinerarioResponse
    {

        public List<ProgramaViaje> Main { get; set; }
        public List<Banner> Banner { get; set; }
        public List<ItinerarioViaje> Itinerario { get; set; }
    }
}