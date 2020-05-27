using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiLatinAmericaJourneys.ModelsWallet
{
    public class EncuestaRequest
    {
        public string User_id { get; set; }
        public string Pregunta { get; set; }
        public string Respuesta { get; set; }

        public List<Marcado> Marcado { get; set; }

    }
    public class Marcado
    {
        public string Pregunta { get; set; }
        public string Respuesta { get; set; }

    }
}