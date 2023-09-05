using Admin_Panel_ITI.Models;
using Admin_Panel_ITI.Repos;
using Admin_Panel_ITI.Repos.Interfaces;
using Admin_Panel_ITI.ViewModels;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> userManager;

        public HomeController(ILogger<HomeController> logger, IIntakeRepository intakeRepository, ITrackRepository trackRepository, IStudentRepository studentRepository, IInstructorRepository instructorRepository, ICourseRepository courseRepository, UserManager<AppUser> userManager)
        {
            _logger = logger;
            this.intakeRepository = intakeRepository;
            this.trackRepository = trackRepository;
            this.studentRepository = studentRepository;
            this.instructorRepository = instructorRepository;
            this.courseRepository = courseRepository;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewData["Intakes"] = intakeRepository.GetAllIntakes().Select(intake => new Intake
                {
                    ID = intake.ID, // Replace with the actual property name for the ID
                    Name = intake.Name // Replace with the actual property name for the display name
                }).ToList(); 

                HomePageViewModel hmPageViewModel = new HomePageViewModel()
                {
                    IntakeNumber = intakeRepository.getIntakeNumber(),
                    TrackNumber = trackRepository.getTrackNumber(),
                    StudentNumber = studentRepository.getStudentNumber(),
                    InstructorNumber = instructorRepository.GetInstructorNumber(),
                    CourseNumber = courseRepository.GetCourseNumber(),

                };

                var user = userManager.GetUserAsync(User).Result; // Get the current user

                if (user != null)
                {
                    // You can pass user-related data to the view here
                    ViewData["FullName"] = user.FullName;
                }
                return View(hmPageViewModel);
            }
            else
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }

        public ActionResult IntakesData(int intakeID)
        {
            var intakeNumber = intakeRepository.getIntakeNumber();
            var studentNumber = intakeID != 0 ? studentRepository.getStudentNumberbyIntakeID(intakeID) : studentRepository.getStudentNumber();
            var trackNumber = intakeID != 0 ? trackRepository.getTrackNumberbyIntakeID(intakeID) : trackRepository.getTrackNumber();
            var instructorNumber = intakeID != 0 ? instructorRepository.GetInstructorNumberbyIntakeID(intakeID) : instructorRepository.GetInstructorNumber();
            var courseNumber = intakeID != 0 ? courseRepository.GetCourseNumberbyIntakeID(intakeID) : courseRepository.GetCourseNumber();

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