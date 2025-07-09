using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cheking
{
    public partial class frmPasajeros : Form
    {
        public int id { get; set; }

        public frmPasajeros()
        {
            InitializeComponent();
        }     
        private void frmPasajeros_Load(object sender, EventArgs e)
        {
            clsVentas venta = new clsVentas();
            string vl;
            // Obtiene el número de vuelo usando el ID recibido del formulario anterior.
            vl = venta.ObtenerNumeroVuelo(id);

            // Obtiene la lista de tickets (pasajeros) para ese número de vuelo.
            List<ticket> pasajeros = venta.ObtenerListaDeTicketsPorNumeroVuelo(vl);

            // Proyecta la lista de pasajeros a un formato anónimo para mostrarlo en el DataGridView.
            // Cambia el estado booleano a un texto más descriptivo ("Check" o "Unchek").
            var listaP = pasajeros.Select(t => new
            {
                Pasajero = t.Pasajero,
                NumeroAsiento = t.NumeroAsiento,
                Estado = t.Estado ? "Check" : "Unchek"
            }).ToList();

            // Asigna la lista procesada como fuente de datos para el DataGridView.
            dtgPasajeros.DataSource = listaP;
        }
        private void btnRegresar_Click(object sender, EventArgs e)
        {
            // Crea una nueva instancia del formulario de Cheking y la muestra.
            frmCheking cheking = new frmCheking();
            cheking.Show();
            this.Close();
        }
    }
}