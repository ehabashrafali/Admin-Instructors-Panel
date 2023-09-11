using Admin_Panel_ITI.Repos.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Admin_Panel_ITI.Areas.InstructorsArea.Controllers
{
    [Area("InstructorsArea")] //have to be added(mandatory)
    public class CourseDayController : Controller
    {
        private readonly ICourseDayRepository courseDayRepo;
        public CourseDayController(ICourseDayRepository _courseDayRepo)
        {
            courseDayRepo = _courseDayRepo;
        }


        //id(courseID) , name(courseName)
        public ActionResult Details(int id , string name, int intakeID, int trackID, string intakeName, string trackName)
        {
            ViewBag.Id = id;    
            ViewBag.Name = name;  
            
            ViewBag.IntakeName = intakeName;    
            ViewBag.TrackName = trackName;    
            ViewBag.IntakeID = intakeID;    
            ViewBag.TrackID = trackID;    
            
            return View(courseDayRepo.GetCourseDaysByCourseID(id));
        }






        public ActionResult Create()
        {
            return View();
        }

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





        public ActionResult Edit(int id)
        {
            return View();
        }


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






        public ActionResult Delete(int id)
        {
            return View();
        }


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
