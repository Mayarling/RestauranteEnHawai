using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestauranteEnHawai.Models;
using System.Text;

namespace RestauranteEnHawai.Controllers
{
    public class ClienteController : ConsumidorRestController
    {
        // GET: ClienteController
        public async Task<ActionResult> Index()
        {
            string respuestaJson = await clienteHttp.GetStringAsync("api/ClienteApi");
            return View(JsonConvert.DeserializeObject<List<Cliente>>(respuestaJson));
        }

        // GET: ClienteController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            string respuestaJson = await clienteHttp.GetStringAsync("api/ClienteApi/" + id);
            Cliente cliente = JsonConvert.DeserializeObject<Cliente>(respuestaJson);
            if (cliente != null)
            {
                return View(cliente);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: ClienteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                Cliente cliente = new Cliente();
                if (string.IsNullOrEmpty(collection["Nombre"]))
                {
                    ModelState.AddModelError("Nombre", "Debe agregar el nombre del cliente.");
                }
                else
                {
                    cliente.Nombre = collection["Nombre"];
                }
                if (string.IsNullOrEmpty(collection["Direccion"]))
                {
                    ModelState.AddModelError("Direccion", "La direccion del cliente es requerida");
                }
                else
                {
                    cliente.Direccion = collection["Direccion"];
                }
                if (string.IsNullOrEmpty(collection["Telefono"]))
                {
                    ModelState.AddModelError("Telefono", "El número de teléfono del cliente es requerido");
                }
                else
                {
                    cliente.Telefono = collection["Telefono"];
                }
                if (string.IsNullOrEmpty(collection["CorreoElectronico"]))
                {
                    ModelState.AddModelError("CorreoElectronico", "El correo electronico del cliente es requerido");
                }
                else
                {
                    cliente.CorreoElectronico = collection["CorreoElectronico"];
                }
                if (!string.IsNullOrEmpty(collection["Administrador"]))
                {
                    cliente.Administrador = collection["Administrador"].Contains("true");
                }
                if (ModelState.ErrorCount == 0)
                {
                    var contenido = new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json");
                    await this.clienteHttp.PostAsync("api/ClienteApi", contenido);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(cliente);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: ClienteController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            string respuestaJson = await clienteHttp.GetStringAsync("api/ClienteApi/" + id);
            Cliente cliente = JsonConvert.DeserializeObject<Cliente>(respuestaJson);
            if(cliente != null)
            {
                return View(cliente);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
            
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            Cliente cliente = new Cliente();
            try
            {
                cliente.Id = id;
                if (string.IsNullOrEmpty(collection["Nombre"]))
                {
                    ModelState.AddModelError("Nombre", "Debe agregar el nombre del cliente.");
                }
                else
                {
                    cliente.Nombre = collection["Nombre"];
                }
                if (string.IsNullOrEmpty(collection["Direccion"]))
                {
                    ModelState.AddModelError("Direccion", "La direccion del cliente es requerida");
                }
                else
                {
                    cliente.Direccion = collection["Direccion"];
                }
                if (string.IsNullOrEmpty(collection["Telefono"]))
                {
                    ModelState.AddModelError("Telefono", "El número de teléfono del cliente es requerido");
                }
                else
                {
                    cliente.Telefono = collection["Telefono"];
                }
                if (string.IsNullOrEmpty(collection["CorreoElectronico"]))
                {
                    ModelState.AddModelError("CorreoElectronico", "El correo electronico del cliente es requerido");
                }
                else
                {
                    cliente.CorreoElectronico = collection["CorreoElectronico"];
                }
                if (!string.IsNullOrEmpty(collection["Administrador"]))
                {
                    cliente.Administrador = collection["Administrador"].Contains("true");
                }
                if (ModelState.ErrorCount == 0)
                {
                    var contenido = new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json");
                    await this.clienteHttp.PutAsync("api/ClienteApi/"+id, contenido);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(cliente);
                }
            }
            catch
            {
                return View(cliente);
            }
        }

        // GET: ClienteController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            string respuestaJson = await clienteHttp.GetStringAsync("api/ClienteApi/" + id);
            Cliente cliente = JsonConvert.DeserializeObject<Cliente>(respuestaJson);
            if (cliente != null)
            {
                return View(cliente);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: ClienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                string respuestaJson = await clienteHttp.GetStringAsync("api/ClienteApi/" + id);
                Cliente cliente = JsonConvert.DeserializeObject<Cliente>(respuestaJson);
                if (cliente != null)
                {
                    await this.clienteHttp.DeleteAsync("api/ClienteApi/" + id);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(cliente);
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
