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

        [HttpGet("ListaDe​AjustesPorId/{id}")]
        public ActionResult<AjusteDeInventario> ObtenerAjustesDelInventarioPorId(int id)
        {
            var inventario = _gestionDeInventarios.ObtengaLaListaDeAjustesPorIdentificacion(id);
            if (inventario == null)
            {
                return NotFound();
            }
            return Ok(inventario);
        }

    }
}
