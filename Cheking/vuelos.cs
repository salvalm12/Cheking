using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cheking
{
    public class vuelos
    {
        public int id { get; set; }
        public string NumeroVuelo { get; set; }
        public DateTime fechaVuelo { get; set; }
        public TimeSpan HoraSalida { get; set; }
        public string Destino { get; set; }
        public double precio { get; set; }
    }
}
