using Admin_Panel_ITI.Models;
using Admin_Panel_ITI.Repos;
using Admin_Panel_ITI.Repos.Interfaces;
using Admin_Panel_ITI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Admin_Panel_ITI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IIntakeRepository intakeRepository;
        private readonly ITrackRepository trackRepository;
        private readonly IStudentRepository studentRepository;
        private readonly IInstructorRepository instructorRepository;
        private readonly ICourseRepository courseRepository;

        public HomeController(ILogger<HomeController> logger, IIntakeRepository intakeRepository, ITrackRepository trackRepository, IStudentRepository studentRepository, IInstructorRepository instructorRepository, ICourseRepository courseRepository)
        {
            _logger = logger;
            this.intakeRepository = intakeRepository;
            this.trackRepository = trackRepository;
            this.studentRepository = studentRepository;
            this.instructorRepository = instructorRepository;
            this.courseRepository = courseRepository;
        }

        public IActionResult Index()
        {

            HomePageViewModel hmPageViewModel = new HomePageViewModel()
            {
                IntakeNumber = intakeRepository.getIntakeNumber(),
                TrackNumber = trackRepository.getTrackNumber(),
                StudentNumber = studentRepository.getStudentNumber(),
                InstructorNumber = instructorRepository.GetInstructorNumber(),
                CourseNumber = courseRepository.GetCourseNumber(),

            };
            return View(hmPageViewModel);
        }

        public ActionResult IntakesData(int intakeID)
        {
            // Fetch updated data from your data source
            var intakeNumber = intakeRepository.getIntakeNumber();
            var studentNumber = studentRepository.getStudentNumberbyIntakeID(intakeID);
            var trackNumber = trackRepository.getTrackNumberbyIntakeID(intakeID);
            var instructorNumber = instructorRepository.GetInstructorNumberbyIntakeID(intakeID);
            var courseNumber = courseRepository.GetCourseNumberbyIntakeID(intakeID);
            // Return a partial view with the updated data

            var viewModel = new HomePageViewModel
            {
                IntakeNumber = intakeNumber,
                StudentNumber = studentNumber,
                TrackNumber = trackNumber,
                InstructorNumber = instructorNumber,
                CourseNumber = courseNumber
            };

            return PartialView("_NumbersPartial", viewModel);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}