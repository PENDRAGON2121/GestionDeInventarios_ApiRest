using GestionDeInventario.BL;
using GestionDeInventarios.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestionDeInventario.SI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly GestionDeLasVentas _gestionDeLasVentas;

        public VentasController(GestionDeInventario.DA.DbGestionDeInventario _contexto)
        {
            _gestionDeLasVentas = new GestionDeLasVentas(_contexto);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Ventas>> ObtenerListaVentas()
        {
            List<GestionDeInventarios.Model.Ventas> laListaDeVentas;
            laListaDeVentas = _gestionDeLasVentas.ObtengaLaListaDeVentas();

            return Ok(laListaDeVentas);
        }



    }
}
