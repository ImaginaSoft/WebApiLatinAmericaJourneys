using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiLatinAmericaJourneys.ModelsWallet
{
    public class BeneficiosResponse
    {
        public List<Beneficios> Beneficios { get; set; }
    }

    public class Beneficios
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Condicion { get; set; }

    }
}