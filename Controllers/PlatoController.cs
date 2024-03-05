using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;
using Newtonsoft.Json;
using RestauranteEnHawai.LogicaDeNegocio;
using RestauranteEnHawai.Models;
using System.Text;

namespace RestauranteEnHawai.Controllers
{
    public class PlatoController : ConsumidorRestController
    {
        // GET: PlatoController
        public async Task<ActionResult> Index()
        {
            string respuestaJson = await clienteHttp.GetStringAsync("api/PlatoApi");
            return View(JsonConvert.DeserializeObject<List<Plato>>(respuestaJson));
        }

        // GET: PlatoController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            string respuestaJson = await clienteHttp.GetStringAsync("api/PlatoApi/"+id);
            Plato plato = JsonConvert.DeserializeObject<Plato>(respuestaJson);
            if (plato != null)
            {
                return View(plato);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
            
        }

        // GET: PlatoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlatoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection, IList<IFormFile> Imagen)
        {
            try
            {
                
                Plato plato = new Plato();
                if (string.IsNullOrEmpty(collection["Nombre"]))
                {
                    ModelState.AddModelError("Nombre", "Debe agregar el nombre del plato.");
                }
                else
                {
                    plato.Nombre = collection["Nombre"];
                }
                if (string.IsNullOrEmpty(collection["Descripcion"]))
                {
                    ModelState.AddModelError("Descripcion", "La descripción del plato es requerida");
                }
                else
                {
                    plato.Descripcion = collection["Descripcion"];
                }
                if (string.IsNullOrEmpty(collection["Precio"]))
                {
                    ModelState.AddModelError("Precio", "El precio del plato es requerido");
                }
                else
                {
                    plato.Precio = double.Parse(collection["Precio"]);
                }

                IFormFile? f = Imagen.FirstOrDefault();
                if (f != null && f.ContentType.ToLower().StartsWith("image/"))
                {
                    using(BinaryReader br = new BinaryReader(f.OpenReadStream()))
                    {

                        plato.Imagen = Convert.ToBase64String(br.ReadBytes((int)f.OpenReadStream().Length));
                    }
                }
                else
                {
                    ModelState.AddModelError("Imagen", "La imagen del plato es requerida.");
                }
                if (collection["Categoria"].Equals("LOCAL"))
                {
                    plato.Categoria = TipoPlato.LOCAL;
                }
                else
                {
                    plato.Categoria = TipoPlato.EXTRANJERO;
                }
                if (ModelState.ErrorCount == 0)
                {
                    //ServicioPlato.Agregar(plato);
                    var contenido = new StringContent(JsonConvert.SerializeObject(plato), Encoding.UTF8, "application/json");
                    await this.clienteHttp.PostAsync("api/PlatoApi", contenido);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(plato);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: PlatoController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            //Plato? plato = ServicioPlato.Obtener(id);
            string respuestaJson = await clienteHttp.GetStringAsync("api/PlatoApi/" + id);
            Plato plato = JsonConvert.DeserializeObject<Plato>(respuestaJson);
            if (plato != null)
            {
                return View(plato);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: PlatoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection, IList<IFormFile> Imagen)
        {
            try
            {
                Plato plato = new Plato();
                plato.Id = id;
                if (string.IsNullOrEmpty(collection["Nombre"]))
                {
                    ModelState.AddModelError("Nombre", "Debe agregar el nombre del plato.");
                }
                else
                {
                    plato.Nombre = collection["Nombre"];
                }
                if (string.IsNullOrEmpty(collection["Descripcion"]))
                {
                    ModelState.AddModelError("Descripcion", "La descripción del plato es requerida");
                }
                else
                {
                    plato.Descripcion = collection["Descripcion"];
                }
                if (string.IsNullOrEmpty(collection["Precio"]))
                {
                    ModelState.AddModelError("Precio", "El precio del plato es requerido");
                }
                else
                {
                    plato.Precio = double.Parse(collection["Precio"]);
                }
                IFormFile? f = Imagen.FirstOrDefault();
                if (f != null && f.ContentType.ToLower().StartsWith("image/"))
                {
                    using (BinaryReader br = new BinaryReader(f.OpenReadStream()))
                    {

                        plato.Imagen = Convert.ToBase64String(br.ReadBytes((int)f.OpenReadStream().Length));
                    }
                }
                else
                {
                    ModelState.AddModelError("Imagen", "La imagen del plato es requerida.");
                }
                if (collection["Categoria"].Equals("LOCAL"))
                {
                    plato.Categoria = TipoPlato.LOCAL;
                }
                else
                {
                    plato.Categoria = TipoPlato.EXTRANJERO;
                }
                if (ModelState.ErrorCount == 0)
                {
                    //ServicioPlato.Actualizar(plato);
                    var contenido = new StringContent(JsonConvert.SerializeObject(plato), Encoding.UTF8, "application/json");
                    await this.clienteHttp.PutAsync("api/PlatoApi/"+id, contenido);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(plato);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: PlatoController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            //Plato? plato = ServicioPlato.Obtener(id);
            string respuestaJson = await clienteHttp.GetStringAsync("api/PlatoApi/" + id);
            Plato plato = JsonConvert.DeserializeObject<Plato>(respuestaJson);
            if (plato != null)
            {
                return View(plato);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: PlatoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                //Plato? plato = ServicioPlato.Obtener(id);
                string respuestaJson = await clienteHttp.GetStringAsync("api/PlatoApi/" + id);
                Plato plato = JsonConvert.DeserializeObject<Plato>(respuestaJson);
                if (plato != null)
                {
                    //ServicioPlato.Delete(plato);
                    await this.clienteHttp.DeleteAsync("api/PlatoApi/"+id);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
                
            }
            catch
            {
                return View();
            }
        }
    }
}
