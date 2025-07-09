using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cheking
{
    public class clsVentas
    {
        private readonly string conexion =
       "server=localhost;database=ItsurAirline;uid=root;pwd=root;";

        /// <summary>
        /// Inserta un nuevo registro de ticket en la base de datos.
        /// </summary>
        /// <param name="ticket">Objeto 'ticket' con la información a persistir.</param>
        /// <returns>Booleano 'true' si la inserción es exitosa.</returns>
        /// <exception cref="ApplicationException">Encapsula errores de SQL o inesperados durante la operación.</exception>
        public bool InsertarTicket(ticket ticket)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd = null;
            try
            {
                conn = new MySqlConnection(conexion);
                conn.Open();
                string query = "INSERT INTO Ticket (id, numeroVuelo, fechaVuelo, HoraSalida, " +
                                 "Destino, Origen, pasajero, numeroAsiento, Costo) " +
                                 "VALUES (@id, @numeroVuelo, @fechaVuelo, @HoraSalida, " +
                                 "@Destino, @Origen, @pasajero, @numeroAsiento, @Costo)";

                cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", ticket.id);
                cmd.Parameters.AddWithValue("@numeroVuelo", ticket.NumeroVuelo);
                cmd.Parameters.AddWithValue("@fechaVuelo", ticket.fechaVuelo);
                cmd.Parameters.AddWithValue("@HoraSalida", ticket.HoraSalida);
                cmd.Parameters.AddWithValue("@Destino", ticket.Destino);
                cmd.Parameters.AddWithValue("@Origen", ticket.Origen);
                cmd.Parameters.AddWithValue("@pasajero", ticket.Pasajero);
                cmd.Parameters.AddWithValue("@numeroAsiento", ticket.NumeroAsiento);
                cmd.Parameters.AddWithValue("@Costo", ticket.Costo);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException ex)
            {
                throw new ApplicationException("Error " +
                    "al crear el ticket en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ocurrió un error inesperado " +
                    " al insertar el ticket.", ex);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
        }

        /// <summary>
        /// Obtiene una lista completa de todos los tickets registrados.
        /// </summary>
        /// <returns>Una lista de objetos 'ticket'.</returns>
        public List<ticket> Obtenertickets()
        {
            MySqlConnection conn = null;
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;
            try
            {
                conn = new MySqlConnection(conexion);
                conn.Open();
                string query = "Select * from Ticket";
                cmd = new MySqlCommand(query, conn);
                reader = cmd.ExecuteReader();

                List<ticket> tickets = new List<ticket>();
                while (reader.Read())
                {
                    ticket ticket = new ticket();
                    ticket.id = reader.GetString(0);
                    ticket.NumeroVuelo = reader.GetString(1);
                    ticket.fechaVuelo = reader.GetDateTime(2);
                    ticket.HoraSalida = reader.GetTimeSpan(3);
                    ticket.Destino = reader.GetString(4);
                    ticket.Origen = reader.GetString(5);
                    ticket.Pasajero = reader.GetString(6);
                    ticket.NumeroAsiento = reader.GetString(7);
                    ticket.Estado = reader.GetBoolean(8);
                    ticket.Costo = reader.GetDouble(9);
                    tickets.Add(ticket);
                }

                return tickets;
            }
            catch (MySqlException ex)
            {
                throw new ApplicationException("Error " +
                    "al insertar el producto en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ocurrió un error inesperado " +
                    " al insertar el producto.", ex);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
                conn.Dispose();

            }
        }

        /// <summary>
        /// Actualiza la información de un ticket existente basado en su ID.
        /// </summary>
        /// <param name="Id">Identificador del ticket a modificar.</param>
        /// <param name="ticket">Objeto 'ticket' con los datos actualizados.</param>
        /// <returns>Booleano 'true' si la actualización es exitosa.</returns>
        public bool Editarticket(string Id, ticket ticket)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd = null;
            try
            {
                conn = new MySqlConnection(conexion);
                conn.Open();
                string query = "UPDATE Ticket SET numeroVuelo=@numeroVuelo, fechaVuelo=@fechaVuelo, HoraSalida=@HoraSalida, " +
                       " Destino=@Destino, Origen=@Origen, pasajero=@pasajero, numeroAsiento=@numeroAsiento, Estado=@Estado WHERE id=@id";
                cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", Id);
                cmd.Parameters.AddWithValue("@numeroVuelo", ticket.NumeroVuelo);
                cmd.Parameters.AddWithValue("@fechaVuelo", ticket.fechaVuelo);
                cmd.Parameters.AddWithValue("@HoraSalida", ticket.HoraSalida);
                cmd.Parameters.AddWithValue("@Destino", ticket.Destino);
                cmd.Parameters.AddWithValue("@Origen", ticket.Origen);
                cmd.Parameters.AddWithValue("@pasajero", ticket.Pasajero);
                cmd.Parameters.AddWithValue("@numeroAsiento", ticket.NumeroAsiento);
                cmd.Parameters.AddWithValue("@Estado", ticket.Estado);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException ex)
            {
                throw new ApplicationException("Error " +
                    "al actualizar el producto en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ocurrió un error inesperado " +
                    " al actualizar el producto.", ex);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
        }

        /// <summary>
        /// Obtiene una lista de tickets que coinciden con un ID específico.
        /// </summary>
        /// <param name="id">El ID del ticket a buscar.</param>
        /// <returns>Una lista de objetos 'ticket'.</returns>
        public List<ticket> ObtenerticketsPorID(int id)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;
            try
            {
                conn = new MySqlConnection(conexion);
                conn.Open();
                string query = "Select * from Ticket WHERE id = @Id ";
                cmd = new MySqlCommand(query, conn);
                reader = cmd.ExecuteReader();

                List<ticket> tickets = new List<ticket>();
                while (reader.Read())
                {
                    ticket ticket = new ticket();
                    ticket.id = reader.GetString(0);
                    ticket.NumeroVuelo = reader.GetString(1);
                    ticket.fechaVuelo = reader.GetDateTime(2);
                    ticket.HoraSalida = reader.GetTimeSpan(3);
                    ticket.Destino = reader.GetString(4);
                    ticket.Origen = reader.GetString(5);
                    ticket.Pasajero = reader.GetString(6);
                    ticket.NumeroAsiento = reader.GetString(7);
                    ticket.Estado = reader.GetBoolean(8);
                    ticket.Costo = reader.GetDouble(9);

                    tickets.Add(ticket);
                }

                return tickets;
            }
            catch (MySqlException ex)
            {
                throw new ApplicationException("Error " +
                    "al insertar el producto en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ocurrió un error inesperado " +
                    " al insertar el producto.", ex);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
                conn.Dispose();

            }
        }

        /// <summary>
        /// Elimina un ticket de la base de datos por su ID.
        /// </summary>
        /// <param name="Id">ID del ticket a eliminar.</param>
        /// <returns>Booleano 'true' si la eliminación es exitosa.</returns>
        public bool EliminarTicket(int Id)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd = null;
            try
            {
                conn = new MySqlConnection(conexion);
                conn.Open();
                string query = "DELETE from Ticket where id=@id";
                cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", Id);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException ex)
            {
                throw new ApplicationException("Error " +
                    "al insertar el producto en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ocurrió un error inesperado " +
                    " al insertar el producto.", ex);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }


        }

        /// <summary>
        /// Recupera los asientos ocupados para un número de vuelo específico.
        /// </summary>
        /// <param name="numeroVuelo">El número de vuelo a consultar.</param>
        /// <returns>Una lista de strings con los nombres de los asientos ocupados.</returns>
        public List<string> ObtenerAsientosOcupadosPorNumeroVuelo(String numeroVuelo)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd = null;
            List<string> asientosOcupados = new List<string>();

            try
            {
                conn = new MySqlConnection(conexion);
                conn.Open();
                string query = "SELECT numeroAsiento FROM Ticket WHERE numeroVuelo = @numeroVuelo";
                cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@numeroVuelo", numeroVuelo);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        asientosOcupados.Add(reader.GetString(0));
                    }
                }

                return asientosOcupados;
            }
            catch (MySqlException ex)
            {
                throw new ApplicationException("Error al obtener los asientos ocupados.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ocurrió un error inesperado al obtener los asientos ocupados.", ex);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
        }

        /// <summary>
        /// Obtiene un objeto 'ticket' completo a partir de su ID.
        /// </summary>
        /// <param name="Id">ID del ticket a buscar.</param>
        /// <returns>Un objeto 'ticket' o null si no se encuentra.</returns>
        public ticket ObtenerTicketPorId(string Id)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;

            try
            {
                conn = new MySqlConnection(conexion);
                conn.Open();

                string query = "SELECT * FROM Ticket WHERE id = @Id";
                cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", Id);

                reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                    return null;

                ticket ticket = new ticket();

                while (reader.Read())
                {
                    ticket.id = reader.GetString(0);
                    ticket.NumeroVuelo = reader.GetString(1);
                    ticket.fechaVuelo = reader.GetDateTime(2);
                    ticket.HoraSalida = reader.GetTimeSpan(3);
                    ticket.Destino = reader.GetString(4);
                    ticket.Origen = reader.GetString(5);
                    ticket.Pasajero = reader.GetString(6);
                    ticket.NumeroAsiento = reader.GetString(7);
                    ticket.Estado = reader.IsDBNull(8) ? false : reader.GetBoolean(8);
                    ticket.Costo = reader.GetDouble(9);

                }

                return ticket;
            }
            catch (MySqlException ex)
            {
                throw new ApplicationException("Error al consultar el ticket en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ocurrió un error inesperado al consultar el ticket.", ex);
            }
            finally
            {
                reader?.Close();
                cmd?.Dispose();
                conn?.Close();
                conn?.Dispose();
            }
        }

        /// <summary>
        /// Actualiza únicamente el campo 'Estado' de un ticket específico.
        /// </summary>
        /// <param name="Id">ID del ticket a modificar.</param>
        /// <param name="estado">Nuevo valor booleano para el estado.</param>
        /// <returns>Booleano 'true' si la actualización es exitosa.</returns>
        public bool EditarEstadoticket(String Id, bool estado)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd = null;
            try
            {
                conn = new MySqlConnection(conexion);
                conn.Open();
                string query = "UPDATE Ticket SET Estado=@Estado WHERE id=@id";
                cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", Id);
                cmd.Parameters.AddWithValue("@Estado", estado);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException ex)
            {
                throw new ApplicationException("Error " +
                    "al actualizar el producto en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ocurrió un error inesperado " +
                    " al actualizar el producto.", ex);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
        }

        /// <summary>
        /// Obtiene una lista de asientos con su estado actual para un vuelo específico.
        /// </summary>
        /// <param name="numeroVuelo">El número de vuelo a consultar.</param>
        /// <returns>Una lista de objetos 'Asiento' que contienen el nombre y el estado.</returns>
        public List<Asiento> ObtenerAsientosConEstado(string numeroVuelo)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;
            List<Asiento> lista = new List<Asiento>();

            try
            {
                conn = new MySqlConnection(conexion);
                conn.Open();

                string query = "SELECT NumeroAsiento, Estado FROM Ticket WHERE NumeroVuelo = @numeroVuelo";
                cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@numeroVuelo", numeroVuelo);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Asiento asiento = new Asiento
                    {
                        Nombre = reader.GetString("NumeroAsiento"),
                        Estado = reader.GetInt32("Estado")
                    };
                    lista.Add(asiento);
                }
            }
            catch (MySqlException ex)
            {
                throw new ApplicationException("Error al consultar los asientos en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ocurrió un error inesperado al obtener el estado de los asientos.", ex);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }

            return lista;
        }

        /// <summary>
        /// Renderiza la distribución de asientos en un DataGridView, pintando según su estado.
        /// </summary>
        /// <param name="dtgAsiento">El control DataGridView a poblar.</param>
        /// <param name="asientos">La lista de asientos con su estado a representar.</param>
        public void CargarAsientos(DataGridView dtgAsiento, List<Asiento> asientos)
        {
            dtgAsiento.Rows.Clear();
            dtgAsiento.Columns.Clear();
            dtgAsiento.AllowUserToAddRows = false;
            dtgAsiento.RowHeadersVisible = false;
            dtgAsiento.SelectionMode = DataGridViewSelectionMode.CellSelect;

            for (int col = 0; col < 7; col++)
            {
                dtgAsiento.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = $"Col{col + 1}",
                    Width = 50,
                    SortMode = DataGridViewColumnSortMode.NotSortable
                });
            }

            for (int fila = 0; fila < 5; fila++)
            {
                var nuevaFila = new DataGridViewRow { Height = 50 };
                nuevaFila.CreateCells(dtgAsiento);

                for (int col = 0; col < 7; col++)
                {
                    string nombreAsiento = (fila + 1).ToString() + (char)('A' + col);

                    if (col == 3)
                    {
                        nuevaFila.Cells[col].Value = "";
                        nuevaFila.Cells[col].ReadOnly = true;
                        nuevaFila.Cells[col].Style.BackColor = Color.LightGray;
                        nuevaFila.Cells[col].Style.SelectionBackColor = Color.LightGray;
                        nuevaFila.Cells[col].Style.SelectionForeColor = Color.LightGray;
                    }
                    else
                    {
                        nuevaFila.Cells[col].Value = nombreAsiento;

                        Asiento asiento = asientos.FirstOrDefault(a => a.Nombre == nombreAsiento);

                        if (asiento != null)
                        {
                            nuevaFila.Cells[col].ReadOnly = true;

                            if (asiento.Estado == 1) // Vendido
                            {
                                nuevaFila.Cells[col].Style.BackColor = Color.OrangeRed;
                                nuevaFila.Cells[col].Style.SelectionBackColor = Color.DarkOrange;
                                nuevaFila.Cells[col].Style.SelectionForeColor = Color.White;
                            }
                            else if (asiento.Estado == 2) // Check-in
                            {
                                nuevaFila.Cells[col].Style.BackColor = Color.LightGreen;
                                nuevaFila.Cells[col].Style.SelectionBackColor = Color.Green;
                                nuevaFila.Cells[col].Style.SelectionForeColor = Color.White;
                            }
                        }
                        else
                        {
                            // Disponible
                            nuevaFila.Cells[col].Style.BackColor = Color.LightBlue;
                            nuevaFila.Cells[col].Style.SelectionBackColor = Color.DodgerBlue;
                            nuevaFila.Cells[col].Style.SelectionForeColor = Color.Black;
                        }
                    }
                }

                dtgAsiento.Rows.Add(nuevaFila);
            }
        }

        /// <summary>
        /// Obtiene el número de vuelo desde la tabla de vuelos a partir de su ID.
        /// </summary>
        /// <param name="idVuelo">ID del vuelo a consultar.</param>
        /// <returns>String con el número de vuelo, o vacío si no se encuentra.</returns>
        public String ObtenerNumeroVuelo(int idVuelo)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd = null;
            String NVuelo = "";

            try
            {
                conn = new MySqlConnection(conexion);
                conn.Open();
                string query = "SELECT NumeroVuelo FROM VuelosD WHERE id = @id";
                cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", idVuelo);

                object resultado = cmd.ExecuteScalar();

                if (resultado != null && resultado != DBNull.Value)
                {
                    NVuelo = (string)resultado;
                }

                return NVuelo;
            }
            catch (MySqlException ex)
            {
                throw new ApplicationException("Error al obtener la fecha del vuelo.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ocurrió un error inesperado al obtener la fecha del vuelo.", ex);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
        }

        /// <summary>
        /// Obtiene todos los tickets asociados a un número de vuelo.
        /// </summary>
        /// <param name="numeroVuelo">El número de vuelo a filtrar.</param>
        /// <returns>Una lista de objetos 'ticket' para el vuelo especificado.</returns>
        public List<ticket> ObtenerListaDeTicketsPorNumeroVuelo(string numeroVuelo)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;
            List<ticket> listaTickets = new List<ticket>();

            try
            {
                conn = new MySqlConnection(conexion);
                conn.Open();

                string query = "SELECT * FROM Ticket WHERE NumeroVuelo = @numeroVuelo";
                cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@numeroVuelo", numeroVuelo);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ticket t = new ticket
                    {
                        id = reader.GetString("id"),
                        NumeroVuelo = reader.GetString("NumeroVuelo"),
                        fechaVuelo = reader.GetDateTime("fechaVuelo"),
                        HoraSalida = reader.GetTimeSpan("HoraSalida"),
                        Destino = reader.GetString("Destino"),
                        Origen = reader.GetString("Origen"),
                        Pasajero = reader.GetString("Pasajero"),
                        NumeroAsiento = reader.GetString("NumeroAsiento"),
                        Estado = reader.IsDBNull(reader.GetOrdinal("Estado")) ? false : reader.GetBoolean("Estado"),
                        Costo = reader.GetDouble("Costo")
                    };

                    listaTickets.Add(t);
                }

                return listaTickets;
            }
            catch (MySqlException ex)
            {
                throw new ApplicationException("Error al consultar los tickets del vuelo.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ocurrió un error inesperado al consultar los tickets del vuelo.", ex);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
        }

        /// <summary>
        /// Verifica si un ID de ticket corresponde a un número de vuelo específico.
        /// </summary>
        /// <param name="id">ID del ticket a verificar.</param>
        /// <param name="NumeroVuelo">Número de vuelo a verificar.</param>
        /// <returns>Booleano 'true' si la consulta se ejecuta.</returns>
        public bool verificaridPorNumeroVuelo(string id, string NumeroVuelo)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd = null;
            try
            {
                conn = new MySqlConnection(conexion);
                conn.Open();
                string query = "SELECT 1 FROM Ticket WHERE id = @id AND NumeroVuelo = @NumeroVuelo LIMIT 1";
                cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@NumeroVuelo", NumeroVuelo);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException ex)
            {
                throw new ApplicationException("Error " +
                    "al actualizar el producto en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ocurrió un error inesperado " +
                    " al actualizar el producto.", ex);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
        }

        /// <summary>
        /// Cambia el color de fondo de una celda específica en un DataGridView para marcarla como seleccionada.
        /// </summary>
        /// <param name="dtgAsiento">El control DataGridView que contiene los asientos.</param>
        /// <param name="numeroAsiento">El nombre del asiento a marcar.</param>
        public void MarcarAsientoComoSeleccionado(DataGridView dtgAsiento, string numeroAsiento)
        {
            foreach (DataGridViewRow fila in dtgAsiento.Rows)
            {
                foreach (DataGridViewCell celda in fila.Cells)
                {
                    if (celda.Value != null && celda.Value.ToString() == numeroAsiento)
                    {
                        celda.Style.BackColor = Color.Aquamarine;
                        return;
                    }
                }
            }

            MessageBox.Show("Asiento no encontrado.");
        }

        /// <summary>
        /// Obtiene una lista completa de todos los vuelos disponibles.
        /// </summary>
        /// <returns>Una lista de objetos 'vuelos'.</returns>
        public List<vuelos> ObtenerVuelos()
        {
            MySqlConnection conn = null;
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;
            try
            {
                conn = new MySqlConnection(conexion);
                conn.Open();
                string query = "Select * from VuelosD";
                cmd = new MySqlCommand(query, conn);
                reader = cmd.ExecuteReader();

                List<vuelos> VuelosD = new List<vuelos>();
                while (reader.Read())
                {
                    vuelos vuelos = new vuelos();
                    vuelos.id = reader.GetInt32(0);
                    vuelos.NumeroVuelo = reader.GetString(1);
                    vuelos.fechaVuelo = reader.GetDateTime(2);
                    vuelos.HoraSalida = reader.GetTimeSpan(3);
                    vuelos.Destino = reader.GetString(4);
                    vuelos.precio = reader.GetDouble(5);
                    VuelosD.Add(vuelos);
                }

                return VuelosD;
            }
            catch (MySqlException ex)
            {
                throw new ApplicationException("Error " +
                    "al insertar el producto en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ocurrió un error inesperado " +
                    " al insertar el producto.", ex);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
                conn.Dispose();

            }
        }

        /// <summary>
        /// Verifica si un asiento está ocupado (vendido o con checkin).
        /// </summary>
        /// <param name="numeroVuelo">Número del vuelo a consultar.</param>
        /// <param name="nombreAsiento">Nombre del asiento a verificar.</param>
        /// <returns>Booleano 'true' si el asiento tiene estado 1 o 2.</returns>
        public bool EstaAsientoOcupado(string numeroVuelo, string nombreAsiento)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;

            try
            {
                conn = new MySqlConnection(conexion);
                conn.Open();

                string query = "SELECT Estado FROM Ticket WHERE NumeroVuelo = @numeroVuelo AND NumeroAsiento = @nombreAsiento";
                cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@numeroVuelo", numeroVuelo);
                cmd.Parameters.AddWithValue("@nombreAsiento", nombreAsiento);

                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    int estado = reader.GetInt32("Estado");
                    return estado == 1 || estado == 2; // vendido o check-in
                }

                return false; // No hay registro => asiento libre
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al verificar el estado del asiento.", ex);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
        }

    }
}