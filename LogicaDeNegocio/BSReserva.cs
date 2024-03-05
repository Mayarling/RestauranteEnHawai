using RestauranteEnHawai.DAO;
using RestauranteEnHawai.Models;

namespace RestauranteEnHawai.LogicaDeNegocio
{
    public class BSReserva
    {
        /// <summary>
        /// Método para agregar una reserva.
        /// </summary>
        /// <param name="reserva">La reserva que se va agregar</param>
        /// <returns>La reserva agregada</returns>
        public Reserva Agregar(Reserva reserva)
        {
            ReservaDao.Create(reserva);
            return reserva;
        }

        /// <summary>
        /// Método para obetener una reserva.
        /// </summary>
        /// <param name="numeroReserva">El número de reserva con el que se va obtener una reserva</param>
        /// <returns>La reserva que se obtuvo dado un número de reserva o null sino encuentra nada</returns>
        public Reserva? Obtener(int numeroReserva)
        {
            return ReservaDao.Retrieve(numeroReserva);

        }

        /// <summary>
        /// Método para obetener todas las reserva registradas.
        /// </summary>
        /// <returns>Todas las reservas registradas</returns>
        public List<Reserva> ObtenerTodos()
        {
            return ReservaDao.RetrieveAll();
        }

        /// <summary>
        /// Método para actualizar una reserva.
        /// </summary>
        /// <param name="reserva">La reserva a actualizar</param>
        public void Actualizar(Reserva reserva)
        {
            ReservaDao.Update(reserva);
        }

        /// <summary>
        /// Método para borrar una reserva.
        /// </summary>
        /// <param name="reserva">La reserva que se va borrar</param>
        public void Delete(Reserva reserva)
        {
            ReservaDao.Delete(reserva);
        }
    }
}
