using GestionDeInventario.BL;
using GestionDeInventarios.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestionDeInventario.SI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AperturaDeCajaController : ControllerBase
    {
        private readonly AdministracionDeInventario _gestionDeInventarios;

        public AperturaDeCajaController(GestionDeInventario.DA.DbGestionDeInventario _contexto)
        {
            _gestionDeInventarios = new AdministracionDeInventario(_contexto);
        }

        [HttpGet("TieneApertura/{nombre}")]
        public ActionResult<String> ObtenerIdPorNombre(String nombre)
        {
            var informacion = _gestionDeInventarios.TieneAperturaDeCaja(nombre);

            return Ok(informacion);
        }

        [HttpGet]
        public ActionResult<IEnumerable<AperturaDeLaCaja>> ObtenerListaDeCajas()
        {
            var laListaDeApertura = _gestionDeInventarios.ObtengaLaListaDeCajas();
            return Ok(laListaDeApertura);
        }

        [HttpPost]
        public ActionResult RegistrarApertura([FromBody] AperturaDeCajaNueva apertura)
        {
            _gestionDeInventarios.RegistreUnaAperturaDeCaja(apertura);
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<AperturaDeLaCaja> ObtenerAjustePorId(int id)
        {
            AperturaDeLaCaja apertura = _gestionDeInventarios.ObtengaLaAperturaDeCajasPorIdentificacion(id);
            return Ok(apertura);
        }

        [HttpGet("UltimaApertura")]
        public ActionResult<AperturaDeLaCaja> ObtenerUltimaApertura()
        {
            var ultimoRegistro = _gestionDeInventarios.ObtengaLaUltimaAperturaDeCaja();

            return Ok(ultimoRegistro);
        }

        [HttpGet("AcumuladoDeLaCaja/{id}")]
        public ActionResult<AcumuladoDeVentas> ObtengaElAcumuladoDeLaCaja(int id)
        {
            AcumuladoDeVentas acumulado;
            acumulado = _gestionDeInventarios.ObtengaElAcumuladoDeCaja(id);
            return Ok(acumulado);
        }

        [HttpGet("CierreDeCaja")]
        public ActionResult ObtengaElCierreDeCaja()
        {
            _gestionDeInventarios.CerrarUnaCaja();
            return Ok();
        }
        
    }
}
