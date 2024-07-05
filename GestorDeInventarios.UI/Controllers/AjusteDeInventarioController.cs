using Azure.Identity;
using GestionDeInventario.BL;
using GestionDeInventario.DA;
using GestionDeInventarios.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Bcpg;

namespace GestorDeInventario.UI.Controllers
{
    public class AjusteDeInventarioController : Controller
    {
        private readonly AdministracionDeInventario _GestionDeInventarios;
        private readonly IAdministradorDeUsuarios _administradorDeUsuarios;


        public AjusteDeInventarioController(DbGestionDeInventario conexion)
        {
            _GestionDeInventarios = new AdministracionDeInventario(conexion);
            _administradorDeUsuarios = new AdministradorDeUsuarios(conexion);
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

            var ajuste = new GestionDeInventarios.Model.AjusteDeInventario
            {

                UserId = User.Identity.Name
            };

            GestionDeInventarios.Model.AjusteDeInventario ajusteDeInventarios;

            ajusteDeInventarios = _GestionDeInventarios.ObtengaElAjustesPorIdentificacion(id);
            int idUsuario = int.Parse(ajusteDeInventarios.UserId);
            String nombre = _administradorDeUsuarios.ObtengaElNombreDelUsuarioPorID(idUsuario);
            ajusteDeInventarios.UserId = nombre;


            return View(ajusteDeInventarios);
        }

        public ActionResult DetalleDeInventario(int id)
        {


            GestionDeInventarios.Model.Inventario inventarios;

            inventarios = _GestionDeInventarios.ObtengaElInventarioPorIdentificacion(id);

            return View(inventarios);
        }
        public ActionResult ListadoDeAjustes(int id)
        {

            List<GestionDeInventarios.Model.AjusteDeInventario> laListaDelInventario;

            laListaDelInventario = _GestionDeInventarios.ObtengaLaListaDeAjustesPorIdentificacion(id);

            int _idDelUsuario = int.Parse(laListaDelInventario[0].UserId);
            String nombre = _administradorDeUsuarios.ObtengaElNombreDelUsuarioPorID(_idDelUsuario);
            ViewBag.Nombre = nombre;

            return View(laListaDelInventario);
        }

        public ActionResult CrearNuevoAjuste(int id)
        {
            var ajuste = new GestionDeInventarios.Model.AjusteDeInventario
            {

                UserId = User.Identity.Name
            };

            GestionDeInventarios.Model.Inventario inventarios;
            GestionDeInventarios.Model.AjusteDeInventario ajusteDeInventarios = new GestionDeInventarios.Model.AjusteDeInventario();


            inventarios = _GestionDeInventarios.ObtengaElInventarioPorIdentificacion(id);


            ajusteDeInventarios.Id_Inventario = id;
            ajusteDeInventarios.CantidadActual = inventarios.Cantidad;
            ajusteDeInventarios.Fecha = DateTime.Now;
            ajusteDeInventarios.UserId = ajuste.UserId;


            return View(ajusteDeInventarios);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearNuevoAjuste(AjusteDeInventario ajusteDeInventarios)
        {
            GestionDeInventarios.Model.Inventario inventarios;
            GestionDeInventarios.Model.AjusteDeInventario ajusteDeInventariosAgregar = new GestionDeInventarios.Model.AjusteDeInventario();


            try
            {
                int UserId = _GestionDeInventarios.BusqueElIdDelUsuarioPorNombre(User.Identity.Name);

                var ajuste = new GestionDeInventarios.Model.AjusteDeInventario
                {

                    UserId = UserId.ToString()
                };


                inventarios = _GestionDeInventarios.ObtengaElInventarioPorIdentificacion(ajusteDeInventarios.Id);


                ajusteDeInventariosAgregar.Id_Inventario = inventarios.Id;
                ajusteDeInventariosAgregar.CantidadActual = inventarios.Cantidad;
                ajusteDeInventariosAgregar.Ajuste = ajusteDeInventarios.Ajuste;
                ajusteDeInventariosAgregar.Tipo = ajusteDeInventarios.Tipo;
                ajusteDeInventariosAgregar.Observaciones = ajusteDeInventarios.Observaciones;
                ajusteDeInventariosAgregar.Fecha = DateTime.Now;
                ajusteDeInventariosAgregar.UserId = ajuste.UserId;

                _GestionDeInventarios.RegistreUnAjusteDeInventario(ajusteDeInventariosAgregar);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
