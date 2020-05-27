using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiLatinAmericaJourneys.Models
{
    public class FotoConsultaResponse
    {
            public List<Fotos> Fotos { get; set; }
    }

    public class Fotos
    {
        public string IdFoto { get; set; }
        public string Url { get; set; }
        public string Titulo { get; set; }
        public string Comentario { get; set; }
        public string Linkshare { get; set; }
    }
}