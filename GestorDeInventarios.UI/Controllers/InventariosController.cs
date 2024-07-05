using Microsoft.AspNetCore.Mvc;
using GestionDeInventario.BL;
using GestionDeInventario.DA;
using GestionDeInventarios.Model;
using Newtonsoft.Json;

namespace GestorDeInventario.UI.Controllers
{

    public class InventariosController : Controller
    {

        private readonly AdministracionDeInventario _GestionDeInventarios;

        public InventariosController(DbGestionDeInventario conexion)
        {

            _GestionDeInventarios = new AdministracionDeInventario(conexion);
        }

        public ActionResult Index(string nombre)
        {

            if (nombre is null)
            {
                List<GestionDeInventarios.Model.Inventario> laListaDelInventario;
                laListaDelInventario = _GestionDeInventarios.ObtengaLaLista();

                return View(laListaDelInventario);
            }
            else
            {
                List<GestionDeInventarios.Model.Inventario> laListaDelInventarioPorNombre;
                laListaDelInventarioPorNombre = _GestionDeInventarios.ObtengaLaListaPorNombre(nombre);
                return View(laListaDelInventarioPorNombre);

            }
        }

        public ActionResult Details(int id)
        {

            GestionDeInventarios.Model.Inventario inventarios;

            inventarios = _GestionDeInventarios.ObtengaElInventarioPorIdentificacion(id);
            List<GestionDeInventarios.Model.HistorialDeInventario> historialDeCambios;
            historialDeCambios = _GestionDeInventarios.ObtengaElHistorialDeCambios(id);

            ViewBag.Historial = historialDeCambios;

            return View(inventarios);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GestionDeInventarios.Model.Inventario inventarios)
        {
            try
            {

                _GestionDeInventarios.RegistreUnInventario(inventarios, User.Identity.Name);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {

            GestionDeInventarios.Model.Inventario inventarios;
            GestionDeInventarios.Model.InventariosEditar inventariosEditar = new GestionDeInventarios.Model.InventariosEditar();

            inventarios = _GestionDeInventarios.ObtengaElInventarioPorIdentificacion(id);

            inventariosEditar.Id = inventarios.Id;
            inventariosEditar.Nombre = inventarios.Nombre;
            inventariosEditar.Categoria = inventarios.Categoria;
            inventariosEditar.Precio = inventarios.Precio;

            return View(inventariosEditar);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(GestionDeInventarios.Model.Inventario inventario)
        {
            try
            {
                var httpClient = new HttpClient();

                // Crear el contenido JSON para el inventario
                string json = JsonConvert.SerializeObject(new
                {
                    inventario.Id,
                    inventario.Nombre,
                    inventario.Categoria,
                    inventario.Precio
                });

                // Añadir el nombre de identidad a los headers
                httpClient.DefaultRequestHeaders.Add("NombreIdentidad", User.Identity.Name);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                var response = await httpClient.PutAsync("https://localhost:7218/api/Inventarios", byteContent);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}
