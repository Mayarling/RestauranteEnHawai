using RestauranteEnHawai.DAO;
using RestauranteEnHawai.Models;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace RestauranteEnHawai.LogicaDeNegocio
{
    public class BSVentas
    {
        /// <summary>
        /// Metodo para agregar una venta.
        /// </summary>
        /// <param name="venta">La venta que se va agregar</param>
        /// <returns>La venta agregada</returns>
        public Venta Agregar(Venta venta)
        {
            VentaDao.Create(venta);
            return venta;
        }

        /// <summary>
        /// Metodo para obetener una venta.
        /// </summary>
        /// <param name="numeroOrden">El número de orden con el que se va obtener la venta</param>
        /// <returns>La venta que se obtuvo dado el número de orden o null sino encuentra nada</returns>
        public Venta? Obtener(int numeroOrden)
        {
            return VentaDao.Retrieve(numeroOrden);

        }

        /// <summary>
        /// Metodo para obetener todas las ventas registradas.
        /// </summary>
        /// <returns>Todas las ventas registradas</returns>
        public List<Venta> ObtenerVentas()
        {
            return VentaDao.RetrieveAll();
        }

        /// <summary>
        /// Método para actualizar una venta.
        /// </summary>
        /// <param name="venta">La venta a actualizar</param>
        public void Actualizar(Venta venta)
        {
            VentaDao.Update(venta);
        }

        /// <summary>
        /// Método para borrar una venta.
        /// </summary>
        /// <param name="venta">La venta que se va borrar</param>
        public void Delete(Venta venta)
        {
            VentaDao.Delete(venta);
        }

        /// <summary>
        /// Método para obtener el reporte de ventas entre 2 fechas.
        /// </summary>
        /// <param name="fechaInicio">La fecha de inicio</param>
        /// <param name="fechaFin">La fecha de fin</param>
        /// <returns>La lista con las ventas realizadas entre 2 fechas</returns>
        public List<Venta> ReporteDeVentas(DateTime fechaInicio, DateTime fechaFin, bool usarMes)
        {
            List<Venta> todasLasVentas = VentaDao.RetrieveAll();
            List<Venta> ventasDeReporte = new();
            foreach (Venta venta in todasLasVentas)
            {
                if (usarMes)
                {
                    if (venta.FechaHora.Year >= fechaInicio.Year
                        && venta.FechaHora.Month >= fechaInicio.Month
                        && venta.FechaHora.Year <= fechaFin.Year
                        && venta.FechaHora.Month <= fechaFin.Month
                        )
                    {
                        ventasDeReporte.Add(venta);
                    }
                }
                else
                {
                    if (venta.FechaHora.Year >= fechaInicio.Year
                        && venta.FechaHora.Month >= fechaInicio.Month
                        && venta.FechaHora.Day >= fechaInicio.Day
                        && venta.FechaHora.Year <= fechaFin.Year
                        && venta.FechaHora.Month <= fechaFin.Month
                        && venta.FechaHora.Day <= fechaFin.Day
                    )
                    {
                        ventasDeReporte.Add(venta);
                    }
                }
            }
            return ventasDeReporte;
        }

        /// <summary>
        /// Método para obtener el reporte de los platos más vendidos en un mes.
        /// </summary>
        /// <param name="fecha">La fecha en la que se quiere el reporte</param>
        /// <returns>La lista con los platos más venidos entre 2 fechas</returns>
        public List<EstadisticaVentas> ReporteDePlatosMasVendidos(DateTime fecha)
        {
            List<Venta> todasLasVentas = VentaDao.RetrieveAll();
            List<EstadisticaVentas> platosvendidoReporte = new();
            Dictionary<int, EstadisticaVentas> ventasDelMes = new();

            foreach (Venta venta in todasLasVentas)
            {
                if (venta.FechaHora.Year == fecha.Year && venta.FechaHora.Month == fecha.Month)
                {
                    EstadisticaVentas estadisticaDelPlato = ventasDelMes.GetValueOrDefault(
                        venta.PlatoVendido.Id,
                        new EstadisticaVentas { totalVentas = 0, nombrePlato = venta.PlatoVendido.Nombre}
                    );
                    estadisticaDelPlato.totalVentas = estadisticaDelPlato.totalVentas + venta.CantidadVendida;
                    if (ventasDelMes.ContainsKey(venta.PlatoVendido.Id))
                    {
                        ventasDelMes.Remove(venta.PlatoVendido.Id);
                    }
                    ventasDelMes.Add(venta.PlatoVendido.Id, estadisticaDelPlato);
                }
            }
            platosvendidoReporte = ventasDelMes.Values.OrderByDescending(estadistica => estadistica.totalVentas).ToList();
            return platosvendidoReporte;
        }
    }
}
