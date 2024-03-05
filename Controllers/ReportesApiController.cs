using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using RestauranteEnHawai.LogicaDeNegocio;
using RestauranteEnHawai.Models;
using System.Globalization;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestauranteEnHawai.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesApiController : ControllerBase
    {
        private readonly BSVentas ServicioVentas = new();

        // GET: api/<ReportesApiController>/ventaspormes/20230616
        [HttpGet("ventaspormes/{fecha}")]
        public IActionResult VentasPorMes([SwaggerParameter(Description = "La fecha del reporte. Debe ser en formato yyyyMMdd", Required = true)] string fecha)
        {
            DateTime? fechaReporte = this.obtenerFechaDeString(fecha);
            if(fechaReporte != null)
            {
                return Ok(this.ServicioVentas.ReporteDeVentas((DateTime) fechaReporte, (DateTime) fechaReporte, true));
            }
            return BadRequest();
        }

        // GET api/<ReportesApiController>/ventaspordia/2023/06/25
        /// <summary>
        /// Metodo del reporte de ventas por dia
        /// </summary>
        /// <param name="fecha">La fecha en formato yyyymmdd</param>
        /// <returns>La ventas que se hicieron el dia especificado</returns>
        [HttpGet("ventaspordia/{fecha}")]
        [SwaggerOperation(Summary = "Ventas por dia.", Description = "Reporte de ventas para un día especifico.")]
        public IActionResult VentasPorDia([SwaggerParameter(Description = "La fecha del reporte. Debe ser en formato yyyyMMdd", Required = true)] string fecha)
        {
            DateTime? fechaReporte = this.obtenerFechaDeString(fecha);
            if (fechaReporte != null)
            {
                return Ok(this.ServicioVentas.ReporteDeVentas((DateTime)fechaReporte, (DateTime)fechaReporte, false));
            }
            return BadRequest();
        }

        // GET api/reportes/ventasporperiodo/20230605/20230615
        [HttpGet("ventasporperiodo/{fechaInicio}/{fechaFin}")]
        public IActionResult VentasPorPeriodo(
            [FromRoute]
            [SwaggerParameter(Description = "La fecha del reporte. Debe ser en formato yyyyMMdd", Required = true)]
            string fechaInicio,
            [FromRoute]
            [SwaggerParameter(Description = "La fecha del reporte. Debe ser en formato yyyyMMdd", Required = true)]
            string fechaFin)
        {
            DateTime? fechaInicioReporte = this.obtenerFechaDeString(fechaInicio);
            DateTime? fechaFinReporte = this.obtenerFechaDeString(fechaFin);
            if (fechaInicioReporte != null && fechaFinReporte != null)
            {
                return Ok(this.ServicioVentas.ReporteDeVentas((DateTime) fechaInicioReporte, (DateTime)fechaFinReporte, false));
            }
            return BadRequest();
        }

        // GET api/reportes/platosdelmes/2023/05
        [HttpGet("platospormes/{fecha}")]
        public IActionResult PlatosDelMes([SwaggerParameter(Description = "La fecha del reporte. Debe ser en formato yyyyMMdd", Required = true)] string fecha)
        {
            DateTime? fechaReporte = this.obtenerFechaDeString(fecha);
            if(fechaReporte != null)
            {
                return Ok(this.ServicioVentas.ReporteDePlatosMasVendidos((DateTime)fechaReporte));
            }
            return BadRequest();
        }

        /// <summary>
        /// Método que convierte un string en formato yyyymmdd a un objeto DatTime
        /// </summary>
        /// <param name="fecha">Un string en formato yyyymmdd</param>
        /// <returns>El objeto DateTime que se genera a partir del string, o null si no se puede.</returns>
        private DateTime? obtenerFechaDeString([SwaggerParameter(Description = "La fecha del reporte. Debe ser en formato yyyyMMdd", Required = true)] string fecha)
        {
            DateTime? fechaObjeto = null;
            //DateFormat format = new DateFormat();
            try
            {
                fechaObjeto = DateTime.ParseExact(fecha, "yyyyMMdd", CultureInfo.InvariantCulture);
            } catch(FormatException fe)
            {
                System.Console.Write(fe.Message);
            }
            
            return fechaObjeto;
        }
    }
}
