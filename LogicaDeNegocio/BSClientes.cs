using RestauranteEnHawai.DAO;
using RestauranteEnHawai.Models;

namespace RestauranteEnHawai.LogicaDeNegocio
{
    /// <summary>
    /// Objeto que contiene la logica de negocio de los clientes
    /// </summary>
    public class BSClientes
    {
        /// <summary>
        /// Método de negocio para agregar clientes
        /// </summary>
        /// <param name="cliente">El cliente a agregar</param>
        /// <returns>El cliente agregado</returns>
        public Cliente Agregar(Cliente cliente)
        {
            cliente = ClienteDao.Create(cliente);
            return cliente;
        }

        /// <summary>
        /// Método para obtener un cliente dado su id
        /// </summary>
        /// <param name="id">El id del cliente</param>
        /// <returns>El cliente asociado al id, o null si no hay ningun cliente con ese id.</returns>
        public Cliente? Obtener(int id)
        {
            return ClienteDao.Retrieve(id);
        }

        /// <summary>
        /// Método para obtener todos los clientes registrados
        /// </summary>
        /// <returns>La lista de clientes registrados</returns>
        public List<Cliente> ObtenerTodos()
        {
            return ClienteDao.RetrieveAll();
        }

        /// <summary>
        /// Método para actualizar un cliente
        /// </summary>
        /// <param name="cliente">El cliente con los valores actualizados.</param>
        public void Actualizar(Cliente cliente)
        {
            ClienteDao.Update(cliente);
        }

        /// <summary>
        /// Método para eliminar un cliente dado.
        /// </summary>
        /// <param name="cliente">El cliente a eliminar.</param>
        public void Eliminar(Cliente cliente)
        {
            ClienteDao.Delete(cliente);
        }
    }
}
