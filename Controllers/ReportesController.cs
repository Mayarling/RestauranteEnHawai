using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestauranteEnHawai.Models;

namespace RestauranteEnHawai.Controllers
{
    public class ReportesController : ConsumidorRestController
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ReportePlatos()
        {
            string tipoReporte = TempData["tipo-reporte"].ToString();
            string fechaInicio = TempData["fecha-inicio"].ToString();
            string respuestaJson = await clienteHttp
                .GetStringAsync("api/ReportesApi/platospormes/" + fechaInicio);
            return View(JsonConvert.DeserializeObject<List<EstadisticaVentas>>(respuestaJson));
        }
        public async Task<IActionResult> ReporteVentas()
        {
            string tipoReporte = TempData["tipo-reporte"].ToString();
            string fechaInicio = TempData["fecha-inicio"].ToString();
            
            string url = "";
            if (tipoReporte.Equals("ventas-rango"))
            {
                string fechaFin = TempData["fecha-fin"].ToString();
                url = "api/ReportesApi/ventasporperiodo/" + fechaInicio + "/" + fechaFin;
            } 
            else if (tipoReporte.Equals("ventas-mes"))
            {
                url = "api/ReportesApi/ventaspormes/" + fechaInicio;
            }
            else if (tipoReporte.Equals("ventas-dia"))
            {
                url = "api/ReportesApi/ventaspordia/" + fechaInicio;
            } 
            else
            {
                return RedirectToAction(nameof(Index));
            }
            string respuestaJson = await clienteHttp.GetStringAsync(url);
            return View(JsonConvert.DeserializeObject<List<Venta>>(respuestaJson));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GenerarReporte(IFormCollection collection)
        {
            string tipoReporte = collection["tipo-reporte"];
            if (TempData.ContainsKey("tipo-reporte"))
            {
                TempData.Remove("tipo-reporte");
            }
            TempData.Add("tipo-reporte", tipoReporte);
            DateOnly fechaInicio = DateOnly.Parse(collection["fecha-inicio"]);
            if(TempData.ContainsKey("fecha-inicio"))
            {
                TempData.Remove("fecha-inicio");
            }
            TempData.Add("fecha-inicio", fechaInicio.ToString("yyyyMMdd"));
            if (tipoReporte.Equals("ventas-rango"))
            {
                DateOnly fechaFin = DateOnly.Parse(collection["fecha-fin"]);
                if (TempData.ContainsKey("fecha-fin"))
                {
                    TempData.Remove("fecha-fin");
                }
                TempData.Add("fecha-fin", fechaFin.ToString("yyyyMMdd"));
            }

            if (tipoReporte.Equals("platos-mes"))
            {
                return RedirectToAction(nameof(ReportePlatos));
            }
            else
            {
                return RedirectToAction(nameof(ReporteVentas));
            }
        }
    }
}
