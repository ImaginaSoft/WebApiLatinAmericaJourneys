using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiLatinAmericaJourneys.ModelsWallet
{
    public class WalletResponse
    {
        public string Saldo { get; set; }
        public List<Movs> Movs { get; set; }
    }

    public class Movs
    {
        public string Fecha { get; set; }
        public string Tipo { get; set; }
        public string Importe { get; set; }
        public string Concepto { get; set; }
        public string Ref { get; set; }

    }
}