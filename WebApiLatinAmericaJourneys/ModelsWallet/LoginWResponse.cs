using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiLatinAmericaJourneys.ModelsWallet
{
    public class LoginWResponse    
    {
        public string Status { get; set; }
        public string Msg { get; set; }
        public List<Perfil> Perfil { get; set; }
    }

    public class Perfil
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Saldo { get; set; }

    }


}