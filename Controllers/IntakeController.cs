using Admin_Panel_ITI.Models;
using Admin_Panel_ITI.Repos;
using Admin_Panel_ITI.Repos.Interfaces;
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
        private readonly UserManager<AppUser> _userManager;
        private readonly IStudentRepository studentRepository;
        private readonly IIntake_Track_CourseRepository intake_Track_CourseRepository;

        public IntakeController(IIntakeRepository _intakeRepository, UserManager<AppUser> _userManager, ITrackRepository trackRepository, ICourseRepository courseRepository, IIntake_Track_CourseRepository intake_Track_CourseRepository, UserManager<AppUser> userManager, IStudentRepository studentRepository)
        {
            intakeRepository = _intakeRepository;
            userManager = _userManager;
            this.trackRepository = trackRepository;
            this.courseRepository = courseRepository;
            this.intake_Track_CourseRepository = intake_Track_CourseRepository;
            this._userManager = userManager;
            this.studentRepository = studentRepository;
        }


        public ActionResult Index(int pageNumber)
        {
            var intakes = intakeRepository.GetAllIntakes(pageNumber,10);

            List<int> studentNumsforIntake = studentRepository.getStudentNumberforIntakes(intakes);

            ViewData["NumOfStudsInEachIntake"] = studentNumsforIntake;
            ViewData["Intakes"] = new SelectList(intakes, "ID", "Name"); // Add this line
            ViewBag.PageNumber = 1;
            ViewBag.duration = 0;

            return View(intakes);
        }

        public ActionResult UpdateTableData(int duration, int pageNumber)
        {

            var intakes = intakeRepository.GetAllIntakes();
            List<Intake> intakesbyduration;

            if (duration == 0)
            {
                // Get all tracks without filtering by intake ID
                intakesbyduration = intakeRepository.GetAllIntakes(pageNumber, 10);
                if (intakesbyduration.Count == 0 && pageNumber > 1)
                {
                    intakesbyduration = intakeRepository.GetAllIntakes(pageNumber - 1, 10);
                    pageNumber--;
                }

            }
            else
            {
                // Get tracks filtered by intake ID
                intakesbyduration = intakeRepository.GetIntakesbyDuration(duration, pageNumber, 10);
                if (intakesbyduration.Count == 0 && pageNumber > 1)
                {
                    intakesbyduration = intakeRepository.GetIntakesbyDuration(duration, pageNumber - 1, 10);
                    pageNumber--;
                }
            }

            List<int> studentNumsforIntake = studentRepository.getStudentNumberforIntakes(intakesbyduration);


            ViewData["NumOfStudsInEachIntake"] = studentNumsforIntake;
            ViewBag.PageNumber = pageNumber;
            ViewBag.duration = duration;

            return PartialView("_TableDataPartial", intakesbyduration);
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




        //POST: TrackController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        async public Task<ActionResult> Create(Intake intake)
        {
           
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                string userId = user.Id;
                intake.AdminID = userId;
            }
            if (ModelState.IsValid)
            {
                intakeRepository.CreateIntake(intake);
                return RedirectToAction(nameof(Index));
            }
            return View(intake);
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
                //oldIntake.Name = editedIntake.Name; 
                //oldIntake.Duration = editedIntake.Duration;
                //oldIntake.StartDate = editedIntake.StartDate;
                //oldIntake.EndDate = editedIntake.EndDate;

                //intakeRepository.CreateIntake(oldIntake); 

                intakeRepository.UpdateIntake(id, editedIntake);
                
                return RedirectToAction(nameof(Index));   
            }

            return View();
        }

        [HttpPost]
        public ActionResult Add_Track_Courses(string intakeIDs)
        {
            List<int> selectedIntakeIds = intakeIDs.Split(',').Select(int.Parse).ToList();
            List<String> intakes = new List<String>();
            List<Track> tracks = new List<Track>();
            List<Course> courses = new List<Course>();
            tracks = trackRepository.getTracks();
            courses = courseRepository.GetCourses();

            foreach (var intake in selectedIntakeIds)
            {
                var mainIntake = intakeRepository.getIntakebyID(intake);
                intakes.Add(mainIntake.Name);
            }

            ViewBag.intakeNames = intakes;
            ViewBag.intakeIDs = selectedIntakeIds;
            ViewBag.tracks = new SelectList(tracks, "ID", "Name");
            ViewBag.courses = new SelectList(courses,"ID", "Name");
            return View();
        }

        [HttpPost]
        public void SaveSelectedTrackAndCourses(int selectedTrackId, List<int> selectedCourseIds, List<string> selectedIntakeIds)
        {
            foreach (var intake in selectedIntakeIds)
            {
                foreach (var course in selectedCourseIds)
                {

                    intake_Track_CourseRepository.CreateIntake_Track_Course(int.Parse(intake), selectedTrackId, course);
                }

            }

        }



        
        public ActionResult Delete(List<int> selectedIntakeIds, int duration, int pageNumber)
        {

            intakeRepository.DeleteIntake(selectedIntakeIds);


            var intakes = intakeRepository.GetAllIntakes();
            List<Intake> intakesbyduration;

            if (duration == 0)
            {
                // Get all tracks without filtering by intake ID
                intakesbyduration = intakeRepository.GetAllIntakes(pageNumber, 10);
                if (intakesbyduration.Count == 0 && pageNumber > 1)
                {
                    intakesbyduration = intakeRepository.GetAllIntakes(pageNumber - 1, 10);
                    pageNumber--;
                }

            }
            else
            {
                // Get tracks filtered by intake ID
                intakesbyduration = intakeRepository.GetIntakesbyDuration(duration, pageNumber, 10);
                if (intakesbyduration.Count == 0 && pageNumber > 1)
                {
                    intakesbyduration = intakeRepository.GetIntakesbyDuration(duration, pageNumber - 1, 10);
                    pageNumber--;
                }
            }

            List<int> studentNumsforIntake = studentRepository.getStudentNumberforIntakes(intakesbyduration);


            ViewData["NumOfStudsInEachIntake"] = studentNumsforIntake;
            ViewBag.PageNumber = pageNumber;
            ViewBag.duration = duration;

            return PartialView("_TableDataPartial", intakesbyduration);

        }
    }
}
