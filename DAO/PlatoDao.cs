using RestauranteEnHawai.LogicaDeNegocio;
using RestauranteEnHawai.Models;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace RestauranteEnHawai.DAO
{
    public class PlatoDao
    {
        /// <summary>
        /// Método para guardar un plato en el repositorio de datos
        /// </summary>
        /// <param name="plato">El plato que se va a guardar</param>
        /// <returns>El plato que se guardo en el repositorio de datos</returns>
        public static Plato Create(Plato plato)
        {
            string sentenciaInsert = "INSERT INTO Platos(nombre, descripcion, precio, categoria, imagen) " +
                "VALUES(@nombre, @descripcion, @precio, @categoria, @imagen)";
            using (SqlConnection conexion = AdministradorDeConexion.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(sentenciaInsert, conexion))
            {
                comando.Parameters.AddWithValue("@nombre", plato.Nombre);
                comando.Parameters.AddWithValue("@descripcion", plato.Descripcion);
                comando.Parameters.AddWithValue("@precio", plato.Precio);
                comando.Parameters.AddWithValue("@categoria", plato.Categoria);
                comando.Parameters.AddWithValue("@imagen", plato.Imagen);
                conexion.Open();
                int resultado = comando.ExecuteNonQuery();
            }
            return plato;
        }

        /// <summary>
        /// Método para obtener un plato a través de un Id dado.
        /// </summary>
        /// <param name="id">El id con el que se va a buscar un plato</param>
        /// <returns>El plato que se encontró con el Id</returns>
        public static Plato? Retrieve(int id)
        {
            List<Plato> platos = new();
            DataTable dataTable = new DataTable();
            string sentenciaSql = "SELECT id, nombre, descripcion, precio, categoria, imagen FROM Platos where id = " + id;
            using (SqlConnection conexion = AdministradorDeConexion.ObtenerConexion())
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sentenciaSql, conexion))
            {
                conexion.Open();
                dataAdapter.Fill(dataTable);
                foreach (DataRow row in dataTable.Rows)
                {
                    Plato plato = new Plato
                    {
                        Id = row.Field<int>("id"),
                        Nombre = row.Field<string>("nombre"),
                        Descripcion = row.Field<string>("descripcion"),
                        Precio = row.Field<double>("precio"),
                        Imagen = row.Field<string>("imagen")
                    };
                    if (row.Field<int>("categoria") == 0)
                    {
                        plato.Categoria = TipoPlato.LOCAL;
                    } 
                    else
                    {
                        plato.Categoria = TipoPlato.EXTRANJERO;
                    }
                    platos.Add(plato);
                }
            }
            if (platos.Count > 0)
            {
                return platos.First<Plato>();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Método para actaulizar los valores de un plato.
        /// </summary>
        /// <param name="platoActualizado">El plato con los valores actualizados</param>
        public static void Update(Plato platoActualizado)
        {
            string sentenciaActualizar = "UPDATE Platos SET nombre = @nombre, descripcion = @descripcion, precio = @precio, categoria = @categoria where (id = @id)";
            using (SqlConnection conexion = AdministradorDeConexion.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(sentenciaActualizar, conexion))
            {
                comando.Parameters.AddWithValue("@nombre", platoActualizado.Nombre);
                comando.Parameters.AddWithValue("@descripcion", platoActualizado.Descripcion);
                comando.Parameters.AddWithValue("@precio", platoActualizado.Precio);
                comando.Parameters.AddWithValue("@categoria", platoActualizado.Categoria);
                comando.Parameters.AddWithValue("@imagen", platoActualizado.Imagen);
                comando.Parameters.AddWithValue("@id", platoActualizado.Id);
                conexion.Open();
                int resultado = comando.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Método para borrar un plato de la lista.
        /// </summary>
        /// <param name="plato">El plato a borrar</param>
        public static void Delete(Plato plato)
        {
            string sentenciaDeleteSql = "DELETE from Platos where(id = @id)";
            using (SqlConnection conexion = AdministradorDeConexion.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(sentenciaDeleteSql, conexion))
            {
                comando.Parameters.AddWithValue("@id", plato.Id);
                conexion.Open();
                int resultado = comando.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Método para devolver todos los platos registrados.
        /// </summary>
        /// <returns>La lista con todos los platos registrados</returns>
        public static List<Plato> RetrieveAll()
        {
            //return PlatosRegistrados;
            List<Plato> platos = new();
            DataTable dataTable = new();
            string sentenciaSql = "SELECT id, nombre, descripcion, precio, categoria, imagen FROM Platos ORDER BY id ASC";
            using (SqlConnection conexion = AdministradorDeConexion.ObtenerConexion())
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sentenciaSql, conexion))
            {
                conexion.Open();
                dataAdapter.Fill(dataTable);
                foreach (DataRow row in dataTable.Rows)
                {
                    Plato plato = new Plato
                    {
                        Id = row.Field<int>("id"),
                        Nombre = row.Field<string>("nombre"),
                        Descripcion = row.Field<string>("descripcion"),
                        Precio = row.Field<double>("precio"),
                        Imagen = row.Field<string>("imagen")
                    };
                    if (row.Field<int>("categoria") == 0)
                    {
                        plato.Categoria = TipoPlato.LOCAL;
                    }
                    else
                    {
                        plato.Categoria = TipoPlato.EXTRANJERO;
                    }
                    platos.Add(plato);
                }
            }
            return platos;
        }
    }
}
