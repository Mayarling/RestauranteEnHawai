using System.Data.SqlClient;

namespace RestauranteEnHawai.DAO
{
    public class AdministradorDeConexion
    {
        /// <summary>
        /// Método que obtiene una nueva conexion a la BD.
        /// </summary>
        /// <returns>Una nueva conexion a la BD, o null si da algún error</returns>
        public static SqlConnection ObtenerConexion()
        {
            try
            {
                return new SqlConnection(@"Data Source=(local)\SQLEXPRESS; Initial Catalog=BD_RESTAURANTENHAWAI; Integrated security=True");
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine("Error: " + exception.Message);
                return null;
            }
        }
    }
}
