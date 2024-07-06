using GestionDeInventario.BL;
using GestionDeInventarios.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        [HttpGet("ObtenerVentaPorId/{id}")]
        public ActionResult<Ventas> ObtenerListaVentaPorId(int id)
        {
            var venta = _gestionDeLasVentas.ObtenerVentaPorId(id);

            return Ok(venta);
        }

        [HttpPost]
        public ActionResult RegistrarVenta([FromBody] Ventas venta)
        {
            _gestionDeLasVentas.RegistrarVenta(venta);
            return Ok();
        }

        [HttpGet("ObtenerElCarrito")]
        public ActionResult<IEnumerable<Inventario>> ObtenerElCarrito()
        {
            List<GestionDeInventarios.Model.Inventario> carritoCompras;
            carritoCompras = _gestionDeLasVentas.ObtengaElCarrito();

            return Ok(carritoCompras);
        }
        [HttpPost("AgregarAlCarrito")]
        public ActionResult<IEnumerable<Inventario>> AgregarAlCarrito([FromBody] Inventario inventarios)
        {
            List<Inventario> carritoComprasTemp = _gestionDeLasVentas.AgregarItemAlCarrito(inventarios);
            
            return Ok(carritoComprasTemp);
        }

        [HttpPost("ActualiceElCarrito")]
        public ActionResult<IEnumerable<Inventario>> ActualiceElCarrito([FromBody] List<Inventario> carritoCompras)
        {
            _gestionDeLasVentas.ActualiceElCarrito(carritoCompras);

            return Ok();
        }
        [HttpPost("TerminarVenta/{id}")]
        public ActionResult<Inventario> ObtenerHistorialDeCambios(int id, List<Inventario> carritoCompra)
        {
            _gestionDeLasVentas.TerminarLaVenta(id, carritoCompra);
            return Ok();
        }

        [HttpGet("VentaPorId/{id}")]
        public ActionResult<Ventas> ObtenerHistorialDeCambios(int id)
        {
            var venta = _gestionDeLasVentas.ObtenerVentaPorId(id);
            return Ok(venta);
        }

        [HttpPut]
        public ActionResult ActualizarVenta([FromBody] Ventas venta)
        {
            _gestionDeLasVentas.ActualizarVenta(venta);

            return Ok();
        }


    }
}
