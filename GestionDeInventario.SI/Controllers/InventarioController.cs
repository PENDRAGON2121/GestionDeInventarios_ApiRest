using GestionDeInventario.BL;
using GestionDeInventario.DA;
using GestionDeInventarios.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestionDeInventario.SI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {

        private readonly AdministracionDeInventario _gestionDeInventarios;

        public InventarioController(GestionDeInventario.DA.DbGestionDeInventario _contexto)
        {
            _gestionDeInventarios = new AdministracionDeInventario(_contexto);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Inventario>> ObtenerInventarios()
        {

            var laListaDelInventario = _gestionDeInventarios.ObtengaLaLista();
            return Ok(laListaDelInventario);
        }

        [HttpGet("PorNombre/{nombre}")]
        public ActionResult<IEnumerable<Inventario>> ObtenerInventariosPorNombre(string nombre)
        {
            var laListaDelInventarioPorNombre = _gestionDeInventarios.ObtengaLaListaPorNombre(nombre);
            return Ok(laListaDelInventarioPorNombre);
        }

        [HttpGet("{id}")]
        public ActionResult<Inventario> ObtenerInventarioPorId(int id)
        {
            var inventario = _gestionDeInventarios.ObtengaElInventarioPorIdentificacion(id);

            if (inventario == null)
            {
                return NotFound();
            }
            return Ok(inventario);
        }

        [HttpGet("Historial/{id}")]
        public ActionResult<IEnumerable <HistorialDeInventario>> ObtenerHistorialDeCambios(int id)
        {
            List<HistorialDeInventario> historialDeCambios = _gestionDeInventarios.ObtengaElHistorialDeCambios(id);
            return Ok(historialDeCambios);
        }

        
        [HttpPost]
        public ActionResult RegistrarInventario([FromBody] Inventario inventario)
        {

            _gestionDeInventarios.RegistreUnInventario(inventario, inventario.UsuarioCreador);
            return CreatedAtAction(nameof(ObtenerInventarioPorId), new { id = inventario.Id }, inventario);
        }

        // PUT: api/Inventarios/
        [HttpPut]
        public ActionResult EditarInventario([FromBody] Inventario inventario)
        {
            _gestionDeInventarios.EditeElInventario(inventario.Id, inventario.Nombre, inventario.Categoria, inventario.Precio, inventario.UsuarioCreador);

            return Ok();
        }

        [HttpPut("Actualice")]
        public ActionResult ActualiceElInventario([FromBody] Inventario inventario)
        {
            _gestionDeInventarios.ActualiceElInventario(inventario);

            return Ok();
        }

    }
}
