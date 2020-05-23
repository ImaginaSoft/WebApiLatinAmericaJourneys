using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiLatinAmericaJourneys.Models
{
    public class EItinerario
    {
        public string NroServicio { get; set; }
        public string CodTipoServicio { get; set; }
        public string Dia { get; set; }
        public string FchInicio { get; set; }
        public string AnioInicio { get; set; }
        public string MesInicio { get; set; }
        public string DiaInicio { get; set; }
        public string Ciudad { get; set; }
        public string HoraServicio { get; set; }
        public string DesServicioDet { get; set; }
        public string FlagColor { get; set; }
        public string NroDia { get; set; }
        public string NroOrden { get; set; }
        public string KeyReg { get; set; }
        public string CodUsuario { get; set; }
        public string Notificacion { get; set; }
        public string NroPedido { get; set; }
        public string NroPropuesta { get; set; }
        public string NroVersion { get; set; }

    }

    public class ProgramaViaje
    {
        public string FchInicio { get; set; }
        public string NroPrograma { get; set; }
        public string DesPrograma { get; set; }
        public string CantDias { get; set; }
        public string EmailVendedor { get; set; }
        public string NroPedido { get; set; }
        public string NroPropuesta { get; set; }
        public string NroVersion { get; set; }
        public string Stars { get; set; }
    }

    public class Banner
    {
        public string StrURL { get; set; }
    }

    public class Actividades
    {
        public string Hora { get; set; }
        public string Descripcion { get; set; }
    }

    public class Lugares
    {
        public string Lugar { get; set; }
        public List<Actividades> Actividades { get; set; }
    }

    public class ItinerarioViaje
    {
        public string AnioInicio { get; set; }
        public string MesInicio { get; set; }
        public string DiaInicio { get; set; }
        public List<Lugares> Lugares { get; set; }
    }

}