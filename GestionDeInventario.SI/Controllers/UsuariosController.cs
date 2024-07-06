using GestionDeInventario.BL;
using GestionDeInventarios.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestionDeInventario.SI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IAdministradorDeUsuarios _administradorDeUsuarios;

        public UsuariosController(GestionDeInventario.DA.DbGestionDeInventario _contexto)
        {
            _administradorDeUsuarios = new AdministradorDeUsuarios(_contexto);
        }
        [HttpPost("Registre")]
        public IActionResult Registre([FromBody] UsuarioRegistro _usuario)
        {
            _administradorDeUsuarios.CreeUnNuevoUsuario(_usuario);
            return Ok(_usuario);
        }

        [HttpPost("ObtengaElInicioDeSesion")]
        public Usuario ObtengaPorCredenciales([FromBody] UsuarioSesion usuario)
        {
            
            Usuario _usuario = _administradorDeUsuarios.ObtengaElInicioDeSesionDelUsuario(usuario.Correo, usuario.Clave);
            return _usuario;
        }
        [HttpPost("Actualice")]
        public IActionResult Actualice([FromBody] UsuarioActualizado _usuarioActualizado)
        {
            _administradorDeUsuarios.ActualiceLosDatosDelUsuario(_usuarioActualizado, _usuarioActualizado.Correo, _usuarioActualizado.NombreDelUsuario);
            
            return Ok();

        }

        [HttpPost("InicioDeSesionConOAuth")]
        public IActionResult SesionPorOAuth([FromBody] UsuarioRegistroOAuth _usuario)
        {
            _administradorDeUsuarios.InicieLaSesionConOAuth(_usuario);   
            return Ok();
        }

        [HttpGet("ObtenerNombreDelUsuarioPorId/{id}")]
        public ActionResult<String> ObtenerElNombreDeUsuarioPorId(int id)
        {
            String nombre = _administradorDeUsuarios.ObtengaElNombreDelUsuarioPorID(id);

            if (nombre == null)
            {
                return NotFound();
            }
            return Ok(nombre);
        }
        [HttpGet("ObtenerUsuariosSinSuscripcion")]
        public ActionResult<IEnumerable<Usuario>> ObtenerUsuarios()
        {

            var listaDeUsuarios = _administradorDeUsuarios.ObtengaLaListaDeUsuariosSinSuscripcion();
            return Ok(listaDeUsuarios);
        }
        [HttpGet("VerifiqueLaSuscripcionDelUsuario/{id}")]
        public ActionResult<String> ObtengaLaInformacionDeSuscripcion(int id)
        {

            Boolean suscripcion = _administradorDeUsuarios.ElUsuarioEstaSuscrito(id);

            String respuesta = suscripcion.ToString();
      
            return Ok(suscripcion);
        }
        [HttpGet("VerifiqueLaSuscripcionDelUsuarioOAuth/{id}")]
        public ActionResult<String> ObtengaLaInformacionDeSuscripcionOAuth(String id)
        {

            Boolean suscripcion = _administradorDeUsuarios.ElUsuarioEstaSuscrito(id);

            String respuesta = suscripcion.ToString();

            return Ok(suscripcion);
        }

        [HttpGet("SuscribirUsuarioPorId/{id}")]
        public ActionResult ActualiceLaSuscripcion(int id)
        {
            _administradorDeUsuarios.SuscribaAlUsuarioPorId(id);
            return Ok();
        }

    }
}
