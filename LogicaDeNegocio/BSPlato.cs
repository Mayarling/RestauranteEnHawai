using RestauranteEnHawai.DAO;
using RestauranteEnHawai.Models;

namespace RestauranteEnHawai.LogicaDeNegocio
{
    public class BSPlato
    {
        /// <summary>
        /// Método para agregar un plato.
        /// </summary>
        /// <param name="plato">El plato que se va agregar</param>
        /// <returns>El plato agregado</returns>
        public Plato Agregar(Plato plato)
        {
            PlatoDao.Create(plato);
            return plato;
        }

        /// <summary>
        /// Método para obetener un plato.
        /// </summary>
        /// <param name="id">El id con el que se va obtener el plato</param>
        /// <returns>El plato que se obtuvo dado un id o null sino encuentra nada</returns>
        public Plato? Obtener(int id)
        {
            return PlatoDao.Retrieve(id);

        }

        /// <summary>
        /// Método para obtener todos los platos registrados.
        /// </summary>
        /// <returns>Todos los platos registrados</returns>
        public List<Plato> ObtenerTodos()
        {
            return PlatoDao.RetrieveAll();
        }

        /// <summary>
        /// Metodo para actualizar un plato.
        /// </summary>
        /// <param name="plato">El plato a actualizar</param>
        public void Actualizar(Plato plato)
        {
            PlatoDao.Update(plato);
        }

        /// <summary>
        /// Método para borrar un plato.
        /// </summary>
        /// <param name="plato">El plato que se va borrar</param>
        public void Delete(Plato plato)
        {
            PlatoDao.Delete(plato);
        }
    }
}
