using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using Microsoft.AspNetCore.Mvc.Rendering;
using Admin_Panel_ITI.Repos;
using Admin_Panel_ITI.Models;

namespace Admin_Panel_ITI.Controllers
{
    public class TrackController : Controller
    {
        private readonly ITrackRepository trackRepositry;
        public IIntakeRepository intakeRepository { get; }

        public TrackController(ITrackRepository trackRepositry, IIntakeRepository intakeRepository)
        {
            this.trackRepositry = trackRepositry;
            this.intakeRepository = intakeRepository;
        }
        // GET: TrackController
        public ActionResult Index(int? page)
        {

            int pageSize = 30;
            int pageNumber = page ?? 1;
            var Tracks = trackRepositry.getTracks().ToPagedList(pageNumber, pageSize);
            return View(Tracks);
        }

        // GET: TrackController/Details/5
        public ActionResult Details(int id)
        {
            return View(trackRepositry.getTrackbyID(id));
        }

        // GET: TrackController/Create
        public ActionResult Create()
        {
            var Intake = intakeRepository.GetIntakes();
            ViewBag.AllIntakes = new SelectList(Intake, "ID", "Name");
            return View();
        }

        // POST: TrackController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Track track)
        {
            if (ModelState.IsValid)
            {
                trackRepositry.CreateTrack(track);
                return RedirectToAction(nameof(Index));
            }
            return View(track);
        }

        // GET: TrackController/Edit/5
        public ActionResult Edit(int id)
        {
            var track = trackRepositry.getTrackbyID(id);
            return View(track);
        }

        // POST: TrackController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Track track)
        {
            if (ModelState.IsValid)
            {
                var Intake = intakeRepository.GetIntakes();
                ViewBag.AllIntakes = new SelectList(Intake, "ID", "Name");
                trackRepositry.UpdateTrack(id, track);
                return RedirectToAction(nameof(Index));
            }
            return View(track);
        }

        // GET: TrackController/Delete/5
        public ActionResult Delete(int id)
        {
            trackRepositry.DeleteTrack(id);
            return RedirectToAction(nameof(Index));
        }

        //// POST: TrackController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
