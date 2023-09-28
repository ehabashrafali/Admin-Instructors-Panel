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
        private readonly IIntake_Track_CourseRepository intake_Track_CourseRepository;

        public HomeController(ILogger<HomeController> logger, IIntakeRepository intakeRepository, ITrackRepository trackRepository, IStudentRepository studentRepository, IInstructorRepository instructorRepository, ICourseRepository courseRepository, UserManager<AppUser> userManager, IIntake_Track_CourseRepository intake_Track_CourseRepository)
        {
            _logger = logger;
            this.intakeRepository = intakeRepository;
            this.trackRepository = trackRepository;
            this.studentRepository = studentRepository;
            this.instructorRepository = instructorRepository;
            this.courseRepository = courseRepository;
            this.userManager = userManager;
            this.intake_Track_CourseRepository = intake_Track_CourseRepository;
        }

        public IActionResult Index()
        {

            if (User.Identity.IsAuthenticated)
            {
                
                if (User.IsInRole("Instructor"))
                {
                    return LocalRedirect( Url.Action("Index", "Intake", new { area = "InstructorsArea" }));

                }
                else
                {
                    ViewData["Intakes"] = intakeRepository.GetAllIntakes()
                     .Select(intake => new Intake
                     {
                         ID = intake.ID,
                         Name = intake.NameAndDuration,
                         Tracks = intake.IntakeTrackCourse?.Select(tc => tc.Track).ToList() ?? new List<Track>()
                     })
                     .ToList();

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
            int  IntakeId = intakeID ;

            var selectedtracks = intake_Track_CourseRepository.GetTracksByIntakeID(intakeID);

            // to get the fucken tracks ids
            var trackIds = selectedtracks.Select(tc => tc.TrackID).ToList();

            var viewModel = new HomePageViewModel
            {
                IntakeNumber = intakeNumber,
                StudentNumber = studentNumber,
                TrackNumber = trackNumber,
                InstructorNumber = instructorNumber,
                CourseNumber = courseNumber,
                IntakeId = intakeID,
                TrackIds = trackIds //pass tracksids to VM
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