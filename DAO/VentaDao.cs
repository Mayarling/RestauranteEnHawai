using RestauranteEnHawai.LogicaDeNegocio;
using RestauranteEnHawai.Models;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace RestauranteEnHawai.DAO
{
    public class VentaDao
    {

        /// <summary>
        /// Método para guardar una venta en el repositorio de datos.
        /// </summary>
        /// <param name="venta">La venta que se va a guardar</param>
        /// <returns>La venta que se guardo en el repositorio de datos</returns>
        public static Venta Create(Venta venta)
        {
            string sentenciaInsert = "INSERT INTO Ventas(id_plato, fecha_hora, cantidad_vendida) " +
                "VALUES(@idPlato, @fecha, @cantidad)";
            using (SqlConnection conexion = AdministradorDeConexion.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(sentenciaInsert, conexion))
            {
                comando.Parameters.AddWithValue("@idPlato", venta.PlatoVendido.Id);
                comando.Parameters.AddWithValue("@fecha", venta.FechaHora);
                comando.Parameters.AddWithValue("@cantidad", venta.CantidadVendida);
                conexion.Open();
                int resultado = comando.ExecuteNonQuery();
            }
            return venta;
        }

        /// <summary>
        /// Metodo para obtener una venta a través de su número de orden dado.
        /// </summary>
        /// <param name="numeroOrden">El número de orden con el que se va a buscar una venta</param>
        /// <returns>La venta que se encontró con el número de orden</returns>
        public static Venta? Retrieve(int numeroOrden)
        {
            List<Venta> ventas = new();
            DataTable dataTable = new DataTable();
            string sentenciaSql = "SELECT id, id_plato, fecha_hora, cantidad_vendida FROM Ventas where id = " + numeroOrden;
            using (SqlConnection conexion = AdministradorDeConexion.ObtenerConexion())
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sentenciaSql, conexion))
            {
                conexion.Open();
                dataAdapter.Fill(dataTable);
                foreach (DataRow row in dataTable.Rows)
                {
                    Venta venta = new Venta
                    {
                        NumeroOrden = row.Field<int>("id"),
                        FechaHora = row.Field<DateTime>("fecha_hora"),
                        CantidadVendida = row.Field<int>("cantidad_vendida")
                    };
                    int id_plato = row.Field<int>("id_plato");
                    Plato platoVendido = PlatoDao.Retrieve(id_plato);
                    venta.PlatoVendido = platoVendido;
                    ventas.Add(venta);
                }
            }
            if (ventas.Count > 0)
            {
                return ventas.First<Venta>();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Metodo para actaulizar los valores de una venta.
        /// </summary>
        /// <param name="ventaActualizada">La venta con los valores actualizados</param>
        public static void Update(Venta ventaActualizada)
        {
            string sentenciaActualizar = "UPDATE Ventas SET id_plato = @idPlato, fecha_hora = @fecha, cantidad_vendida = @cantidad where (id = @id)";
            using (SqlConnection conexion = AdministradorDeConexion.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(sentenciaActualizar, conexion))
            {
                comando.Parameters.AddWithValue("@fecha", ventaActualizada.FechaHora);
                comando.Parameters.AddWithValue("@cantidad", ventaActualizada.CantidadVendida);
                comando.Parameters.AddWithValue("@idPlato", ventaActualizada.PlatoVendido.Id);
                comando.Parameters.AddWithValue("@id", ventaActualizada.NumeroOrden);
                conexion.Open();
                int resultado = comando.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Método para borrar una venta de la lista.
        /// </summary>
        /// <param name="venta">La venta a borrar</param>
        public static void Delete(Venta venta)
        {
            string sentenciaDeleteSql = "DELETE from Ventas where(id = @id)";
            using (SqlConnection conexion = AdministradorDeConexion.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(sentenciaDeleteSql, conexion))
            {
                comando.Parameters.AddWithValue("@id", venta.NumeroOrden);
                conexion.Open();
                int resultado = comando.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Metodo para devolver todas lss ventas registradas.
        /// </summary>
        /// <returns>La lista con todas las ventas registradas</returns>
        public static List<Venta> RetrieveAll()
        {
            //return ReservaRegistradas;
            List<Venta> ventas = new();
            DataTable dataTable = new();
            string sentenciaSql = "SELECT id, id_plato, fecha_hora, cantidad_vendida FROM Ventas ORDER BY id ASC";
            using (SqlConnection conexion = AdministradorDeConexion.ObtenerConexion())
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sentenciaSql, conexion))
            {
                conexion.Open();
                dataAdapter.Fill(dataTable);
                foreach (DataRow row in dataTable.Rows)
                {
                    Venta venta = new Venta
                    {
                        NumeroOrden = row.Field<int>("id"),
                        FechaHora = row.Field<DateTime>("fecha_hora"),
                        CantidadVendida = row.Field<int>("cantidad_vendida")
                    };
                    int id_plato = row.Field<int>("id_plato");
                    Plato platoVendido = PlatoDao.Retrieve(id_plato);
                    venta.PlatoVendido = platoVendido;
                    ventas.Add(venta);
                }
            }
            return ventas;
        }
    }
}
