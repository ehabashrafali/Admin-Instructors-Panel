using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using Microsoft.AspNetCore.Mvc.Rendering;
using Admin_Panel_ITI.Repos;
using Admin_Panel_ITI.Models;
using NuGet.DependencyResolver;
using Microsoft.AspNetCore.Identity;
using Admin_Panel_ITI.Repos.Interfaces;

namespace Admin_Panel_ITI.Controllers
{
    public class TrackController : Controller
    {
        private readonly ITrackRepository trackRepositry;
        public IIntakeRepository intakeRepository { get; }
        private readonly UserManager<AppUser> _userManager;
        private readonly IInstructorRepository instructorRepository;

        public TrackController(ITrackRepository trackRepositry, IIntakeRepository intakeRepository, UserManager<AppUser> userManager, IInstructorRepository instructorRepository)
        {
            this.trackRepositry = trackRepositry;
            this.intakeRepository = intakeRepository;
            _userManager = userManager;
            this.instructorRepository = instructorRepository;
        }
        // GET: TrackController
        public ActionResult Index(int pageNumber)
        {
            var Tracks = trackRepositry.getTracks(pageNumber, 10);
            return View(Tracks);
        }

        // GET: TrackController/Details/5
        public ActionResult Details(int id)
        {
            return View(trackRepositry.getTrackbyID(id));
        }

        public ActionResult GetTrackByInakeId(int id, int pageNumber)
        {
            var tracks = trackRepositry.getTrackbyIntakeID(id, pageNumber, 10);
            return View(tracks);
        }

        // GET: TrackController/Create
        public ActionResult Create()
        {

            var instructors = instructorRepository.GetInstructors();
            ViewBag.AllInstructors = new SelectList(instructors, "Id", "FullName");
            return View();
        }

        // POST: TrackController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        async public Task<ActionResult> Create(Track track)
        {
            var instructors = instructorRepository.GetInstructors();
            ViewBag.AllInstructors = new SelectList(instructors, "Id", "FullName");
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                string userId = user.Id;
                track.AdminID = "admin1";
            }
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
            var instructors = instructorRepository.GetInstructors();
            ViewBag.AllInstructors = new SelectList(instructors, "Id", "FullName");
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
