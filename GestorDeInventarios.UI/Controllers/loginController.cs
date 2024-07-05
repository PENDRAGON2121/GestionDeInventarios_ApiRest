using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using GestionDeInventarios.Model;
using GestionDeInventario.BL;
using GestorDeInventarios.UI.Models;
using Microsoft.VisualBasic;

namespace GestorDeInventario.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAdministradorDeUsuarios _administradorDeUsuarios;

        public LoginController(GestionDeInventario.DA.DbGestionDeInventario _contexto)
        {
            _administradorDeUsuarios = new AdministradorDeUsuarios(_contexto);
        }

        // GET: LoginController
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registro()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registro(UsuarioRegistro _usuario)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Verifique que los datos estén correctos.");
                return View();
            }

            var usuario = _administradorDeUsuarios.CreeUnNuevoUsuario(_usuario);

            if (usuario == null)
            {
                ModelState.AddModelError(string.Empty, "El usuario ya existe");
                return View();
            }

            return RedirectToAction("Index", "login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(UserLogin usuario)
        {
            Usuario? _usuario = _administradorDeUsuarios.ObtengaElInicioDeSesionDelUsuario(usuario.Email, usuario.Password);


            if (_usuario.Email is null)
            {

                ModelState.AddModelError(string.Empty, "ERROR EN EL INGRESO");
                ModelState.AddModelError(string.Empty, "INTENTOS FALLIDOS:" + _usuario.LoginAttempts);
                if (_usuario.IsBlocked is true)
                {
                    ModelState.AddModelError(string.Empty, "BLOQUEADO HASTA: " + _usuario.BlockedUntil);
                }

                return View();
            }
            String role = _usuario.Role.ToString();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _usuario.Name),
                new Claim(ClaimTypes.Email, _usuario.Email),
                new Claim(ClaimTypes.Role, role),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult ActualizarDatosDelUsuario()
        {
            var username = User.Identity.Name;

            ViewBag.Username = username;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ActualizarDatosDelUsuario(UsuarioActualizado _usuarioActualizado)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Verifique que los datos estén correctos.");
                return View();
            }

            var email = User.FindFirstValue(ClaimTypes.Email);
            var username = User.Identity.Name;


            _administradorDeUsuarios.ActualiceLosDatosDelUsuario(_usuarioActualizado, email, username);

            return RedirectToAction("Logout");
        }

        //Controlador de sesion con Google
        public async Task LoginGoogle()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action("GoogleResponse")
                }
            );
        }

        //Respuesta de Google
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value
            });

            UsuarioRegistroOAuth _usuario = new UsuarioRegistroOAuth
            {
                IdOauth = result.Principal.FindFirstValue(ClaimTypes.NameIdentifier),
                Name = result.Principal.FindFirstValue(ClaimTypes.Name),
                Email = result.Principal.FindFirstValue(ClaimTypes.Email)
            };

            _administradorDeUsuarios.InicieLaSesionConOAuth(_usuario);

            //return Json(claims);

            return RedirectToAction("Index", "Home");
        }

        // Controlador de sesion con Facebook
        public async Task LoginFacebook()
        {
            await HttpContext.ChallengeAsync(FacebookDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action("FacebookResponse")
                }
            );
        }

        //Respuesta de facebook
        public async Task<IActionResult> FacebookResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value
            });

            UsuarioRegistroOAuth _usuario = new UsuarioRegistroOAuth
            {
                IdOauth = result.Principal.FindFirstValue(ClaimTypes.NameIdentifier),
                Name = result.Principal.FindFirstValue(ClaimTypes.Name),
                Email = result.Principal.FindFirstValue(ClaimTypes.Email)
            };

            _administradorDeUsuarios.InicieLaSesionConOAuth(_usuario);


            //return Json(claims);

            return RedirectToAction("Index", "Home");
        }

        //Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "login");
        }
    }
}
