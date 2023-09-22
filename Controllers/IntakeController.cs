using Admin_Panel_ITI.Models;
using Admin_Panel_ITI.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Admin_Panel_ITI.Controllers
{
    public class IntakeController : Controller
    {
        private readonly IIntakeRepository intakeRepository;
        private readonly UserManager<AppUser> userManager;
        private readonly ITrackRepository trackRepository;
        private readonly ICourseRepository courseRepository;

        public IntakeController(IIntakeRepository _intakeRepository, UserManager<AppUser> _userManager, ITrackRepository trackRepository, ICourseRepository courseRepository)
        {
            intakeRepository = _intakeRepository;
            userManager = _userManager;
            this.trackRepository = trackRepository;
            this.courseRepository = courseRepository;
        }


        public ActionResult Index()
        {
           var tracks = trackRepository.getTracks();
            var courses = courseRepository.GetCourses();
            
            ViewBag.AllTracks = new SelectList(tracks, "ID", "Name");
            ViewBag.AllCourses = new SelectList(courses, "ID", "Name");
            return View(intakeRepository.GetAllIntakes());
        }



        public ActionResult Details(int id)
        {
            return View();
        }


        /*------------------------------------------------------*/


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Intake newIntake)
        {
            if (ModelState.IsValid)
            {
                //var user = userManager.GetUserAsync(User).Result; 


                string adminID = userManager.GetUserId(User);  //get the currently logged-in AdminID

                //newIntake.CreationDate = DateTime.Now;
                //newIntake.AdminID = adminID;
                newIntake.AdminID = "admin1";

                intakeRepository.CreateIntake(newIntake);

                return RedirectToAction(nameof(Index));
            }

            return View();
        }


       /*------------------------------------------------------*/


        public ActionResult Edit(int id) //id = intake ID
        {            
            return View(intakeRepository.getIntakebyID(id));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Intake editedIntake)
        {
            if(ModelState.IsValid)
            {
                //Intake oldIntake = intakeRepository.getIntakebyID(id);
                //oldIntake.ExamName = editedIntake.ExamName; 
                //oldIntake.Duration = editedIntake.Duration;
                //oldIntake.StartDate = editedIntake.StartDate;
                //oldIntake.EndDate = editedIntake.EndDate;

                //intakeRepository.CreateIntake(oldIntake); 

                intakeRepository.UpdateIntake(id, editedIntake);
                
                return RedirectToAction(nameof(Index));   
            }

            return View();
        }


        /*------------------------------------------------------*/


        public ActionResult Delete(int id) //id = Intake ID
        {
            intakeRepository.DeleteIntake(id);
            return RedirectToAction(nameof(Index));
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
