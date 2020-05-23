using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiLatinAmericaJourneys.ModelsWallet
{
    public class NuevoViajeRequest
    {
        public string User_id { get; set; }
        public string Planes { get; set; }
        public string Fecha { get; set; }
        public string Nombre { get; set; }
        public string Edad { get; set; }

        //public List<Incluye> Incluye { get; set; }
    }
    public class Incluye
    {
        public string Nombre { get; set; }
        public string Edad { get; set; }

    }
}