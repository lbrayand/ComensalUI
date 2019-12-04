using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElComensal.Models
{
    public class Reserva
    {
        
        public string idLocal { get; set; }
        public string idMotivo { get; set; }
        public string estadoReserva { get; set; }
        public String fechaReserva { get; set; }
        public String horaReserva { get; set; }
        public string numPersonas { get; set; }
    }
}