namespace RestauranteEnHawai.Models
{
    /// <summary>
    /// Clase que representa una reserva.
    /// </summary>
    public class Reserva
    {
        public int NumeroDeReserva { get; set; }
        public DateTime FechaHoraReserva { get; set; }
        public string? NombreCliente { get; set; }
        public int CantidadPersonas { get; set; }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Reserva()
        {
            NumeroDeReserva = 0;
            FechaHoraReserva = DateTime.Now;
            NombreCliente = "";
            CantidadPersonas = 0;
        }

        /// <summary>
        /// Constructor por parámetros.
        /// </summary>
        /// <param name="numeroDeReserva">El número de la reserva</param>
        /// <param name="fechaHoraReserva">La fecha y hora de la reserva</param>
        /// <param name="nombreCliente">El nombre del cliente que hizo la reserva</param>
        /// <param name="cantidadPersonas">La cantidad de personas para la reserva</param>
        public Reserva(int numeroDeReserva, DateTime fechaHoraReserva, string? nombreCliente, int cantidadPersonas)
        {
            NumeroDeReserva = numeroDeReserva;
            FechaHoraReserva = fechaHoraReserva;
            NombreCliente = nombreCliente;
            CantidadPersonas = cantidadPersonas;
        }
    }

    
}
