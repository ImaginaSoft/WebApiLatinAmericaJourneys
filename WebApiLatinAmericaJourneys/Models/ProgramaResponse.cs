﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiLatinAmericaJourneys.Models
{
    public class ProgramaResponse
    {

        public DateTime FchSys { get; set; }
        public string NroPrograma { get; set; }

        public string StsPrograma { get; set; }

        public string DesPrograma { get; set; }

        public int CantDias { get; set; }

        public string KeyReg { get; set; }

        public string Resumen { get; set; }

        public string ResumenComida { get; set; }


        //Propuesta Precio

        public string DesOrden { get; set; }
        public decimal PrecioxPersona { get; set; }
        public int CantPersonas { get; set; }
        public decimal PrecioTotal { get; set; }
        public string EmailVendedor { get; set; }
        public string NombreVendedor { get; set; }
    }
}