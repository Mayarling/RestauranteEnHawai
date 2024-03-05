using RestauranteEnHawai.LogicaDeNegocio;
using RestauranteEnHawai.Models;
using System.Data;
using System.Data.SqlClient;

namespace RestauranteEnHawai.DAO
{
    public class ReservaDao
    {
        /// <summary>
        /// Lista de las reservas registradas.
        /// </summary>
        private static readonly List<Reserva> ReservaRegistradas = new();

        /// <summary>
        /// Método para guardar una reserva en el repositorio de datos.
        /// </summary>
        /// <param name="reserva">La reserva que se va a guardar</param>
        /// <returns>>La reserva que se guardo en el repositorio de datos</returns>
        public static Reserva Create(Reserva reserva)
        {
            string sentenciaInsert = "INSERT INTO Reservas(fecha_hora, nombre_cliente, cantidad_personas) " +
                "VALUES(@fecha, @cliente, @cantidad)";
            using (SqlConnection conexion = AdministradorDeConexion.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(sentenciaInsert, conexion))
            {
                comando.Parameters.AddWithValue("@fecha", reserva.FechaHoraReserva);
                comando.Parameters.AddWithValue("@cliente", reserva.NombreCliente);
                comando.Parameters.AddWithValue("@cantidad", reserva.CantidadPersonas);
                conexion.Open();
                int resultado = comando.ExecuteNonQuery();
            }
            return reserva;
        }

        /// <summary>
        /// Método para obtener una reserva a través de un número de reserva dado.
        /// </summary>
        /// <param name="numeroReserva">El número de reserva con el que se va a buscar una reserva</param>
        /// <returns>La reserva que se encontró dado un número de reserva</returns>
        public static Reserva? Retrieve(int numeroReserva)
        {
            List<Reserva> reservas = new();
            DataTable dataTable = new DataTable();
            string sentenciaSql = "SELECT id, fecha_hora, nombre_cliente, cantidad_personas FROM Reservas where id = " + numeroReserva;
            using (SqlConnection conexion = AdministradorDeConexion.ObtenerConexion())
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sentenciaSql, conexion))
            {
                conexion.Open();
                dataAdapter.Fill(dataTable);
                foreach (DataRow row in dataTable.Rows)
                {
                    Reserva reserva = new Reserva
                    {
                        NumeroDeReserva = row.Field<int>("id"),
                        FechaHoraReserva = row.Field<DateTime>("fecha_hora"),
                        NombreCliente = row.Field<string>("nombre_cliente"),
                        CantidadPersonas = row.Field<int>("cantidad_personas")
                    };
                    reservas.Add(reserva);
                }
            }
            if (reservas.Count > 0)
            {
                return reservas.First<Reserva>();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Método para actaulizar los valores de una reserva.
        /// </summary>
        /// <param name="reservaActualizada">La reserva con los valores actualizados</param>
        public static void Update(Reserva reservaActualizada)
        {
            string sentenciaActualizar = "UPDATE Reservas SET fecha_hora = @fecha, nombre_cliente = @cliente, cantidad_personas = @cantidad where (id = @id)";
            using (SqlConnection conexion = AdministradorDeConexion.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(sentenciaActualizar, conexion))
            {
                comando.Parameters.AddWithValue("@fecha", reservaActualizada.FechaHoraReserva);
                comando.Parameters.AddWithValue("@cliente", reservaActualizada.NombreCliente);
                comando.Parameters.AddWithValue("@cantidad", reservaActualizada.CantidadPersonas);
                comando.Parameters.AddWithValue("@id", reservaActualizada.NumeroDeReserva);
                conexion.Open();
                int resultado = comando.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Método para borrar una reserva de la lista.
        /// </summary>
        /// <param name="reserva">La reserva a borrar</param>
        public static void Delete(Reserva reserva)
        {

            string sentenciaDeleteSql = "DELETE from Reservas where(id = @id)";
            using (SqlConnection conexion = AdministradorDeConexion.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(sentenciaDeleteSql, conexion))
            {
                comando.Parameters.AddWithValue("@id", reserva.NumeroDeReserva);
                conexion.Open();
                int resultado = comando.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Método para devolver todas las reserva registradas.
        /// </summary>
        /// <returns>La lista con todas las reservas registradas</returns>
        public static List<Reserva> RetrieveAll()
        {
            //return ReservaRegistradas;
            List<Reserva> reservas = new();
            DataTable dataTable = new();
            string sentenciaSql = "SELECT id, fecha_hora, nombre_cliente, cantidad_personas FROM Reservas ORDER BY id ASC";
            using (SqlConnection conexion = AdministradorDeConexion.ObtenerConexion())
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sentenciaSql, conexion))
            {
                conexion.Open();
                dataAdapter.Fill(dataTable);
                foreach (DataRow row in dataTable.Rows)
                {
                    Reserva reserva = new Reserva
                    {
                        NumeroDeReserva = row.Field<int>("id"),
                        FechaHoraReserva = row.Field<DateTime>("fecha_hora"),
                        NombreCliente = row.Field<string>("nombre_cliente"),
                        CantidadPersonas = row.Field<int>("cantidad_personas")
                    };
                    reservas.Add(reserva);
                }
            }
            return reservas;
        }
    }
}
