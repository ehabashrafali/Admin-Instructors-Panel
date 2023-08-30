using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Admin_Panel_ITI.Controllers
{
    public class TrackController : Controller
    {
        // GET: TrackController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TrackController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TrackController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TrackController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TrackController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TrackController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TrackController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TrackController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
