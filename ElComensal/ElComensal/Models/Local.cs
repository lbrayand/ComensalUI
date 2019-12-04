using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElComensal.Models
{
    public class Local
    {
        public int idLocal { get; set; }
        public string nombreLocal { get; set; }
        public string direccion { get; set; }
        public string numAforo { get; set; }
        public string numPisos { get; set; }
    }
}