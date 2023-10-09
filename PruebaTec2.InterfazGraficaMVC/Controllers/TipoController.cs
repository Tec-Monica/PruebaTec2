using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTec2.EntidadesDeNegocio;
using PruebaTec2.LogicaDeNegocio;

namespace PruebaTec2.InterfazGraficaMVC.Controllers
{
    public class TipoController : Controller
    {
        TipoBL tipoBL = new TipoBL();

        // GET: Accion que muestra la pagina principal de roles
        public async Task<IActionResult> Index(Tipo pTipo = null)
        {
            if (pTipo == null)
                pTipo = new Tipo();

            if (pTipo.top_aux == 0)
                pTipo.top_aux = 10;
            else if (pTipo.top_aux == -1)
                pTipo.top_aux = 0;

            var tipos = await tipoBL.BuscarAsync(pTipo);
            ViewBag.Top = pTipo.top_aux;
            return View(tipos);
        }

        // GET: Accion que muestra el detalle de un registro
        public async Task<IActionResult> Details(int id)
        {
            var tipo = await tipoBL.ObtenerPorIdAsync(new Tipo { Id = id });
            return View(tipo);
        }

        // GET: Accion que muestra el formulario para crear un nuevo rol
        public async Task<IActionResult> Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // POST: TipoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tipo pTipo)
        {
            try
            {
                int result = await tipoBL.CrearAsync(pTipo);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: Accion que muestra el formulario con los datos cargados para modificarlos
        public async Task<IActionResult> Edit(Tipo pTipo)
        {
            var tipo = await tipoBL.ObtenerPorIdAsync(pTipo);
            ViewBag.Error = "";
            return View(tipo);
        }

        // POST: Accion que recibe los datos modificados y los envia a la bd
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tipo pTipo)
        {
            try
            {
                int result = await tipoBL.ModificarAsync(pTipo);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pTipo);
            }
        }

        // GET: Accion que muestra los datos del registro para confirmar la eliminacion
        public async Task<IActionResult> Delete(Tipo pTipo)
        {
            var tipo = await tipoBL.ObtenerPorIdAsync(pTipo);
            ViewBag.Error = "";
            return View(tipo);
        }

        // POST: Accion que recibe la confirmacion para eliminar el registro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Tipo pTipo)
        {
            try
            {
                int result = await tipoBL.EliminarAsync(pTipo);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pTipo);
            }
        }
    }
}
