using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Admin_Panel_ITI.Controllers
{
    public class CourseDayController : Controller
    {
        // GET: CourseDayController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CourseDayController/DetailsForManager/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CourseDayController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CourseDayController/Create
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

        // GET: CourseDayController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CourseDayController/Edit/5
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

        // GET: CourseDayController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CourseDayController/Delete/5
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
