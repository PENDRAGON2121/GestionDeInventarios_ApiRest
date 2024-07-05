using GestionDeInventario.BL;
using GestionDeInventarios.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestionDeInventario.SI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AjusteDelInventarioController : ControllerBase
    {
        private readonly AdministracionDeInventario _gestionDeInventarios;

        public AjusteDelInventarioController(GestionDeInventario.DA.DbGestionDeInventario _contexto)
        {
            _gestionDeInventarios = new AdministracionDeInventario(_contexto);
        }

        [HttpGet("{id}")]
        public ActionResult<Inventario> ObtenerAjusteDeinventarioPorId(int id)
        {
            GestionDeInventarios.Model.AjusteDeInventario ajusteDeInventarios;
            ajusteDeInventarios = _gestionDeInventarios.ObtengaElAjustesPorIdentificacion(id);


            if (ajusteDeInventarios == null)
            {
                return NotFound();
            }
            return Ok(ajusteDeInventarios);
        }

        [HttpGet("ListaPorId/{id}")]
        public ActionResult<List<AjusteDeInventario>> ObtenerAjustesDelInventarioPorId(int id)
        {
            var inventario = _gestionDeInventarios.ObtengaLaListaDeAjustesPorIdentificacion(id);
            return Ok(inventario);
        }
        [HttpGet("ObtenerIdPorNombre/{nombre}")]
        public ActionResult<String> ObtenerIdPorNombre(String nombre)
        {
            int UserId = _gestionDeInventarios.BusqueElIdDelUsuarioPorNombre(nombre);

            if (UserId == null)
            {
                return NotFound();
            }
            return Ok(UserId);
        }

        [HttpPost]
        public ActionResult RegistrarAjusteDeInventario([FromBody] AjusteDeInventario ajusteDeInventario)
        {
            _gestionDeInventarios.RegistreUnAjusteDeInventario(ajusteDeInventario);
            return Ok();
        }


    }
}
