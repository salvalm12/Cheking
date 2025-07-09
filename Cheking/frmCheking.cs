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
    public partial class frmCheking : Form
    {
        public frmCheking()
        {
            InitializeComponent();
        }

        private void btnPasajeros_Click(object sender, EventArgs e)
        {
            // Valida que se haya seleccionado un vuelo en el ComboBox.
            if (cbxDestino.SelectedValue is int idSeleccionado)
            {
                // Si se seleccionó, abre el formulario de pasajeros pasándole el ID del vuelo.
                frmPasajeros frmpas = new frmPasajeros();
                frmpas.id = idSeleccionado;
                frmpas.Show();
            }
            else
            {
                MessageBox.Show("Por favor seleccciona un vuelo", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// Se ejecuta al cargar el formulario. Carga los vuelos disponibles en el ComboBox.
        /// </summary>
        private void frmCheking_Load(object sender, EventArgs e)
        {
            clsVentas venta = new clsVentas();
            // Asigna la lista de vuelos al ComboBox.
            cbxDestino.DataSource = venta.ObtenerVuelos();
            // Define qué propiedad del objeto se mostrará al usuario.
            cbxDestino.DisplayMember = "Destino";
            // Define qué propiedad del objeto se usará como valor interno (el ID).
            cbxDestino.ValueMember = "id";
        }

        /// <summary>
        /// Busca un pasajero por su clave de reservación y realiza un pre-check-in.
        /// </summary>
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            int ids = -2;

            // Valida que se haya seleccionado un vuelo.
            if (cbxDestino.SelectedValue != null)
            {
                ids = (int)cbxDestino.SelectedValue;
            }
            else
            {
                MessageBox.Show("Favor de llenar todos los campos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Termina la ejecución si no hay vuelo seleccionado.
            }

            clsVentas venta = new clsVentas();
            ticket tic;
            string vl = venta.ObtenerNumeroVuelo(ids);

            // Valida que se haya ingresado una clave de reservación.
            if (!string.IsNullOrWhiteSpace(txtClave.Text) && ids >= 0)
            {
                // Verifica si la clave de reservación corresponde al vuelo seleccionado.
                if (venta.verificaridPorNumeroVuelo(txtClave.Text, vl))
                {
                    tic = venta.ObtenerTicketPorId(txtClave.Text);

                    // Valida si el asiento ya ha sido marcado como ocupado o check-in.
                    if (venta.EstaAsientoOcupado(tic.NumeroVuelo, tic.NumeroAsiento))
                    {
                        MessageBox.Show("Este asiento ya fue ocupado o está en estado de check-in.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return; // Termina si el asiento no está disponible.
                    }

                    // Carga y muestra la distribución de asientos del vuelo.
                    List<Asiento> asientos = venta.ObtenerAsientosConEstado(tic.NumeroVuelo);
                    venta.CargarAsientos(dtgAsientos, asientos);

                    // Muestra los datos del pasajero en las etiquetas correspondientes.
                    lblDestino.Text = tic.Destino;
                    lblPasajero.Text = tic.Pasajero;
                    lblAsiento.Text = tic.NumeroAsiento;

                    // Si el ticket es válido, procede a actualizar el estado.
                    if (tic != null && !string.IsNullOrWhiteSpace(tic.NumeroVuelo))
                    {
                        // Cambia el estado del ticket a "check-in" (true).
                        venta.EditarEstadoticket(txtClave.Text, true);

                        // Vuelve a cargar los asientos para reflejar el nuevo estado.
                        List<Asiento> asientos1 = venta.ObtenerAsientosConEstado(tic.NumeroVuelo);
                        venta.CargarAsientos(dtgAsientos, asientos1);
                        // Marca visualmente el asiento del pasajero en el DataGridView.
                        venta.MarcarAsientoComoSeleccionado(dtgAsientos, tic.NumeroAsiento);
                    }
                }
                else
                {
                    MessageBox.Show("La clave no coincide con ninguno, revise que sea el vuelo correcto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Favor de llenar todos los campos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Confirma el proceso de check-in para el pasajero encontrado.
        /// </summary>
        private void btnChek_Click(object sender, EventArgs e)
        {
            clsVentas venta = new clsVentas();
            // Valida que haya una clave de reservación en el campo de texto.
            if (!string.IsNullOrWhiteSpace(txtClave.Text))
            {
                ticket tic = venta.ObtenerTicketPorId(txtClave.Text);
                // Confirma el estado del ticket como 'true' (chequeado).
                venta.EditarEstadoticket(txtClave.Text, true);

                if (tic != null && !string.IsNullOrWhiteSpace(tic.NumeroVuelo))
                {
                    // Recarga el mapa de asientos para reflejar cualquier cambio.
                    List<Asiento> asientos2 = venta.ObtenerAsientosConEstado(tic.NumeroVuelo);
                    venta.CargarAsientos(dtgAsientos, asientos2);

                    // Vuelve a marcar el asiento para asegurar que esté visiblemente seleccionado.
                    venta.MarcarAsientoComoSeleccionado(dtgAsientos, tic.NumeroAsiento);
                    MessageBox.Show("Pase aceptado", "Aceptado", MessageBoxButtons.OK);
                }
            }
        }

        /// <summary>
        /// Se activa cuando el usuario selecciona un vuelo diferente en el ComboBox.
        /// </summary>
        private void cbxDestino_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Valida que el valor seleccionado sea un ID de vuelo válido.
            if (cbxDestino.SelectedValue is int idSeleccionado)
            {
                clsVentas ventas = new clsVentas();
                // Obtiene el número de vuelo a partir del ID seleccionado.
                string numeroVueloActual = ventas.ObtenerNumeroVuelo(idSeleccionado);
                // Obtiene la lista de asientos con su estado actual para ese vuelo.
                List<Asiento> asientos = ventas.ObtenerAsientosConEstado(numeroVueloActual);
                // Carga y muestra los asientos en el DataGridView.
                ventas.CargarAsientos(dtgAsientos, asientos);
            }
        }
    }
}