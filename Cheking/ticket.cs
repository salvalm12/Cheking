using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cheking
{
    public class ticket
    {
        public String id { get; set; }
        public string NumeroVuelo { get; set; }
        public DateTime fechaVuelo { get; set; }
        public TimeSpan HoraSalida { get; set; }
        public string Destino { get; set; }
        public string Origen { get; set; }
        public string Pasajero { get; set; }
        public string NumeroAsiento { get; set; }

        public Boolean Estado { get; set; }

        public double Costo { get; set; }
    }
}
