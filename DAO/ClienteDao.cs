using RestauranteEnHawai.LogicaDeNegocio;
using RestauranteEnHawai.Models;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace RestauranteEnHawai.DAO
{
    /// <summary>
    /// Clase que representa un cliente
    /// </summary>
    public class ClienteDao : AdministradorDeConexion
    {
        /// <summary>
        /// Método para guardar un cliente en el repositorio de datos
        /// </summary>
        /// <param name="cliente">El cliente que se va a guardar</param>
        /// <returns>El cliente que se guardo en el repositorio de datos</returns>
        public static Cliente Create(Cliente cliente)
        {
            string sentenciaInsert = "INSERT INTO Clientes(nombre, direccion, telefono, correo, administrador) " +
                "VALUES(@nombre, @direccion, @telefono, @correo, @administrador)";
            using (SqlConnection conexion = AdministradorDeConexion.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(sentenciaInsert, conexion))
            {
                comando.Parameters.AddWithValue("@nombre", cliente.Nombre);
                comando.Parameters.AddWithValue("@direccion", cliente.Direccion);
                comando.Parameters.AddWithValue("@telefono", cliente.Telefono);
                comando.Parameters.AddWithValue("@correo", cliente.CorreoElectronico);
                comando.Parameters.AddWithValue("@administrador", cliente.Administrador);
                conexion.Open();
                int resultado = comando.ExecuteNonQuery();
            }
            return cliente;
        }

        /// <summary>
        /// Método para obtener un cliente a través de un Id dado.
        /// </summary>
        /// <param name="id">El id con el que se va a buscar un cliente</param>
        /// <returns>El Cliente que se encontró con el Id</returns>
        public static Cliente? Retrieve(int id)
        {
            List<Cliente> clientes = new();
            DataTable dataTable = new DataTable();
            string sentenciaSql = "SELECT id, nombre, direccion, telefono, correo, administrador FROM Clientes where id = " + id;
            using (SqlConnection conexion = AdministradorDeConexion.ObtenerConexion())
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sentenciaSql, conexion))
            {
                conexion.Open();
                dataAdapter.Fill(dataTable);
                foreach (DataRow row in dataTable.Rows)
                {
                    Cliente cliente = new Cliente
                    {
                        Id = row.Field<int>("id"),
                        Nombre = row.Field<string>("nombre"),
                        Direccion = row.Field<string>("direccion"),
                        Telefono = row.Field<string>("telefono"),
                        CorreoElectronico = row.Field<string>("correo"),
                        Administrador = (row.Field<int>("administrador") == 0)? false : true
                    };
                    clientes.Add(cliente);
                }
            }
            if (clientes.Count > 0)
            {
                return clientes.First<Cliente>();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Método para actaulizar los valores de un cliente.
        /// </summary>
        /// <param name="clienteActualizado">El cliente con los valores actualizados</param>
        public static void Update(Cliente clienteActualizado)
        {
            string sentenciaActualizar = "UPDATE Clientes SET nombre = @nombre, direccion = @direccion, telefono = @telefono, correo = @correo, administrador = @administrador where (id = @id)";
            using (SqlConnection conexion = AdministradorDeConexion.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(sentenciaActualizar, conexion))
            {
                comando.Parameters.AddWithValue("@nombre", clienteActualizado.Nombre);
                comando.Parameters.AddWithValue("@direccion", clienteActualizado.Direccion);
                comando.Parameters.AddWithValue("@telefono", clienteActualizado.Telefono);
                comando.Parameters.AddWithValue("@correo", clienteActualizado.CorreoElectronico);
                comando.Parameters.AddWithValue("@administrador", clienteActualizado.Administrador);
                comando.Parameters.AddWithValue("@id", clienteActualizado.Id);
                conexion.Open();
                int resultado = comando.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Método para borrar un cliente de la lista.
        /// </summary>
        /// <param name="cliente">El cliente a borrar</param>
        public static void Delete(Cliente cliente)
        {
            string sentenciaDeleteSql = "DELETE from Clientes where(id = @id)";
            using (SqlConnection conexion = AdministradorDeConexion.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(sentenciaDeleteSql, conexion))
            {
                comando.Parameters.AddWithValue("@id", cliente.Id);
                conexion.Open();
                int resultado = comando.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Método para devolver todos los clientes registrados.
        /// </summary>
        /// <returns>La lista con todos los clientes registrados</returns>
        public static List<Cliente> RetrieveAll()
        {
            List<Cliente> clientes = new();
            DataTable dataTable = new();
            string sentenciaSql = "SELECT id, nombre, direccion, telefono, correo, administrador FROM Clientes ORDER BY id ASC";
            using (SqlConnection conexion = AdministradorDeConexion.ObtenerConexion())
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sentenciaSql, conexion))
            {
                conexion.Open();
                dataAdapter.Fill(dataTable);
                foreach (DataRow row in dataTable.Rows)
                {
                    Cliente cliente = new Cliente
                    {
                        Id = row.Field<int>("id"),
                        Nombre = row.Field<string>("nombre"),
                        Telefono = row.Field<string>("telefono"),
                        Direccion = row.Field<string>("direccion"),
                        CorreoElectronico = row.Field<string>("correo"),
                        Administrador = (row.Field<int>("administrador") == 0) ? false : true
                    };
                    clientes.Add(cliente);
                }
            }
            return clientes;
        }
    }
}
