using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTec2.EntidadesDeNegocio;
using PruebaTec2.LogicaDeNegocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace PruebaTec2.InterfazGraficaMVC.Controllers
{
    public class UsuarioController : Controller
    {
        //Instancias de acceso a los métodos de las clases
        UsuarioBL usuarioBL = new UsuarioBL();
        RolBL rolBL = new RolBL();

        // GET: Acción que muestra la página principal de usuarios
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Index(Usuario pUsuario = null)
        {
            if (pUsuario == null)
                pUsuario = new Usuario();

            if (pUsuario.top_aux == 0)
                pUsuario.top_aux = 10;

            else if (pUsuario.top_aux == -1)
                pUsuario.top_aux = 0;

            var usuarios = await usuarioBL.BuscarIncluirRolesAsync(pUsuario);
            ViewBag.Top = pUsuario.top_aux;
            ViewBag.Roles = await rolBL.ObtenerTodosAsync();
            return View(usuarios);
        }

        // GET: Acción que muestra el detalle de un registro existente
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Details(int id)
        {
            var usuario = await usuarioBL.ObtenerPorIdAsync(new Usuario { Id = id });
            usuario.Rol = await rolBL.ObtenerPorIdAsync(new Rol { Id = usuario.IdRol });
            return View(usuario);
        }

        // GET: Accion que muestra el formulario para agregar un usuario nuevo
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Create()
        {
            ViewBag.Roles = await rolBL.ObtenerTodosAsync();
            ViewBag.Error = "";
            return View();
        }

        // POST: Accion que recibe los datos del formulario para enviarlos a la BD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Usuario pUsuario)
        {
            try
            {
                int result = await usuarioBL.CrearAsync(pUsuario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Roles = await rolBL.ObtenerTodosAsync();
                return View(pUsuario);
            }
        }

        // GET: Accion que muestra los datos cargados para editarlos
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Edit(Usuario pUsuario)
        {
            var usuario = await usuarioBL.ObtenerPorIdAsync(pUsuario);
            ViewBag.Roles = await rolBL.ObtenerTodosAsync();
            ViewBag.Error = "";
            return View(usuario);
        }

        // POST: Accion que recibe los datos modificados para enviarlos a la BD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Usuario pUsuario)
        {
            try
            {
                int result = await usuarioBL.ModificarAsync(pUsuario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Roles = await rolBL.ObtenerTodosAsync();
                return View(pUsuario);
            }
        }

        // GET: Accion que muestra los datos del registro para confirmar la eliminacion
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(Usuario pUsuario)
        {
            var usuario = await usuarioBL.ObtenerPorIdAsync(pUsuario);
            usuario.Rol = await rolBL.ObtenerPorIdAsync(new Rol { Id = usuario.IdRol });
            ViewBag.Error = "";
            return View(usuario);
        }

        // POST: Accion que recibe la confirmación para eliminar el registro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Usuario pUsuario)
        {
            try
            {
                int result = await usuarioBL.EliminarAsync(pUsuario);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                var usuario = await usuarioBL.ObtenerPorIdAsync(pUsuario);
                if (usuario == null)
                    usuario = new Usuario();
                if (usuario.Id > 0)
                    usuario.Rol = await rolBL.ObtenerPorIdAsync(new Rol { Id = usuario.IdRol }); 
                return View(pUsuario);
            }
        }

        // GET: Acción que muestra el formulario de inicio de sesión
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string ReturnUrl = null)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ViewBag.Url = ReturnUrl;
            ViewBag.Error = "";
            return View();
        }

        // POST: Acción que recibe los datos del usuario para realizar el proceso de inicio de sesión
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Usuario pUsuario, string pReturnUrl = null)
        {
            try
            {
                var usuario = await usuarioBL.LoginAsync(pUsuario);
                if (usuario != null && usuario.Id > 0 && pUsuario.Login == usuario.Login)
                {
                    usuario.Rol = await rolBL.ObtenerPorIdAsync(new Rol { Id = usuario.IdRol });

                    //nuevo arreglo []
                    var claims = new[] { new Claim(ClaimTypes.Name, usuario.Login), new Claim(ClaimTypes.Role, usuario.Rol.Nombre) };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                }
                else
                    throw new Exception("Credenciales incorrectas");
                //es nulo o spacios en blanco
                if (!string.IsNullOrWhiteSpace(pReturnUrl))
                    return Redirect(pReturnUrl);

                else
                    return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Url = pReturnUrl;
                ViewBag.Error = ex.Message;
                return View(new Usuario { Login = pUsuario.Login });
            }
        }

        // Accion que permite cerrar sesion en la aplicacion
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [AllowAnonymous]
        public async Task<IActionResult> CerrarSesion(string ReturnUrl = null)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Usuario");
        }

        // GET: Accion que permite al usuario poder registrarse
        public ActionResult Registrarse()
        {

            return View();
        }

        // POST: Acción que el formulario para poder registrarse
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrarse(Usuario pUsuario)
        {
            pUsuario.IdRol = (int)Usuario.EnumRol.Cliente;

            int resultado = await new UsuarioBL().CrearAsync(pUsuario);

            if (resultado > 0)
            {
                //usuario registrado correctamente
                return RedirectToAction("Login", "Usuario");
            }
            else
            {
                //Informar que el usuario no se pudo registrar
                ViewBag.msg = "Ocurrio un error al registrase, por favor intente de nuevo";
            }
            return View();
        }
    }
}
