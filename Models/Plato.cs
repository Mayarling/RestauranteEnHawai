namespace RestauranteEnHawai.Models
{
    /// <summary>
    /// Clase que representa un plato.
    /// </summary>
    public class Plato
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public double Precio { get; set; }
        public TipoPlato Categoria { get; set; }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Plato()
        {
            Id = 0;
            Nombre = "";
            Descripcion = "";
            Precio = 0;
            Imagen = "";
            Categoria = TipoPlato.LOCAL;
        }

        /// <summary>
        /// Constructor por parametros
        /// </summary>
        /// <param name="id">El Id del plato</param>
        /// <param name="nombre"El nombre del plato></param>
        /// <param name="descripcion">La descripción del plato</param>
        /// <param name="imagen">La imagen del plato</param>
        /// <param name="precio">El precio del plato</param>
        /// <param name="categoria">La categoria del plato</param>
        public Plato(int id, string nombre, string descripcion, string imagen, double precio, TipoPlato categoria)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            Precio = precio;
            Imagen = imagen;
            Categoria = categoria;
        }
    }

    /// <summary>
    /// Enumeración para la categoria de un plato.
    /// </summary>
    public enum TipoPlato
    {
        LOCAL,
        EXTRANJERO
    }
}
