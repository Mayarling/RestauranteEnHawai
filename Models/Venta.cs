namespace RestauranteEnHawai.Models
{
    /// <summary>
    /// Clase que representa una venta.
    /// </summary>
    public class Venta
    {
        public int NumeroOrden { get; set; }
        public DateTime FechaHora { get; set; }
        public Plato? PlatoVendido { get; set; }
        public int CantidadVendida { get; set; }

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public Venta()
        {
            NumeroOrden = 0;
            FechaHora = DateTime.Now;
            PlatoVendido = null;
            CantidadVendida = 0;
        }

        /// <summary>
        /// Constructor por parámetros
        /// </summary>
        /// <param name="numeroOrden">El número de orden.</param>
        /// <param name="fechaHora">La fecha y hora en la que se realiza dicha venta</param>
        /// <param name="platoVendido">El objeto plato que fue vendido</param>
        /// <param name="cantidadVendida">La cantidad vendida de dicho plato</param>
        public Venta(int numeroOrden, DateTime fechaHora, Plato? platoVendido, int cantidadVendida)
        {
            NumeroOrden = numeroOrden;
            FechaHora = fechaHora;
            PlatoVendido = platoVendido;
            CantidadVendida = cantidadVendida;
        }
    }
}
