using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTec2.EntidadesDeNegocio;
using PruebaTec2.LogicaDeNegocio;

namespace PruebaTec2.InterfazGraficaMVC.Controllers
{
    public class AveController : Controller
    {
        AveBL aveBL = new AveBL();

        // GET: AveController
        public async Task<IActionResult> Index(Ave pAve = null)
        {
            if (pAve == null)
                pAve = new Ave();

            if (pAve.top_aux == 0)
                pAve.top_aux = 5;

            else if (pAve.top_aux == -1)
                pAve.top_aux = 0;

            var aves = await aveBL.BuscarAsync(pAve);
            ViewBag.Top = pAve.top_aux;
            return View(aves);
        }

        // GET: AveController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var ave = await aveBL.ObtenerPorIdAsync(new Ave { Id = id });
            return View(ave);
        }

        // GET: AveController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Aves = await aveBL.ObtenerTodosAsync();
            ViewBag.Error = "";
            return View();
        }

        // POST: AveController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ave pAve)
        {
            try
            {
                int result = await aveBL.CrearAsync(pAve);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: AveController/Edit/5
        public async Task<IActionResult> Edit(Ave pAve)
        {
            var ave = await aveBL.ObtenerPorIdAsync(pAve);
            ViewBag.Error = "";
            return View(ave);
        }

        // POST: AveController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ave pAve)
        {
            try
            {
                int result = await aveBL.ModificarAsync(pAve);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pAve);
            }
        }

        // GET: AveController/Delete/5
        public async Task<IActionResult> Delete(Ave pAve)
        {
            var ave = await aveBL.ObtenerPorIdAsync(pAve);
            ViewBag.Error = "";
            return View(ave);
        }

        // POST: AveController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Ave pAve)
        {
            try
            {
                int result = await aveBL.EliminarAsync(pAve);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pAve);
            }
        }
    }
}
