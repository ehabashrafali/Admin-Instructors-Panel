using Admin_Panel_ITI.Models;
using Admin_Panel_ITI.Repos;
using Admin_Panel_ITI.Repos.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.DependencyResolver;


namespace Admin_Panel_ITI.Areas.InstructorsArea.Controllers
{
    [Area("InstructorsArea")]
    public class ExamController : Controller
    {
        private readonly IExamRepository examRepo;
        private readonly IQuestionRepository questionRepo;
        private readonly IExam_QuestionRepository examQuestionRepo;
        private readonly UserManager<AppUser> userManager;
        private readonly ICourseRepository courseRepository;
        private readonly IExam_Std_QuestionRepository exam_Std_QuestionRepository;
        private readonly IIntakeRepository intakeRepo;
        private readonly ITrackRepository trackRepo;

        public ExamController(IExamRepository _examRepo,
            IQuestionRepository _questionRepo,
            IExam_QuestionRepository _examQuestionRepo,
            UserManager<AppUser> _userManager,
            ICourseRepository _courseRepository, 
            IExam_Std_QuestionRepository _exam_Std_QuestionRepository,
            IIntakeRepository _intakeRepo,
            ITrackRepository _trackRepo)
        {
            examRepo = _examRepo;
            questionRepo = _questionRepo;
            examQuestionRepo = _examQuestionRepo;
            userManager = _userManager;
            courseRepository = _courseRepository;
            exam_Std_QuestionRepository = _exam_Std_QuestionRepository;
            intakeRepo = _intakeRepo;
            trackRepo = _trackRepo;
        }


        [Route("Index/{courseId?}/{intakeID?}/{trackID?}")]
        public ActionResult Index(int courseId, int intakeID, int trackID)
        {
            ViewBag.Id = courseId;
            ViewBag.IntakeID = intakeID;
            ViewBag.TrackID = trackID;
            ViewBag.Name = courseRepository.GetCourseName(courseId);
            ViewBag.IntakeName = intakeRepo.getIntakeName(intakeID);
            ViewBag.TrackName = trackRepo.getTrackName(trackID);

            string UserID = userManager.GetUserId(User);

            var exams = examRepo.GetExamsByInstructorIDAndCourseID(UserID, courseId);

            var course = courseRepository.GetCoursebyID(courseId);

            ViewBag.Course = course;

            return View(exams);
        }


        [Route("ED/{ExamId?}/{id?}/{trackID?}/{intakeID?}")]
        public ActionResult Details(int ExamId , int id, int trackID, int intakeID)
        {
            var Submetions = exam_Std_QuestionRepository.GetExam(ExamId);

            ViewBag.Id = id;
            ViewBag.intakeID = intakeID;
            ViewBag.TrackID = trackID;
            ViewBag.ExamName = examRepo.GetExambyID(ExamId).Name;

            ViewBag.crsName = courseRepository.GetCourseName(id);
            ViewBag.trackName = trackRepo.getTrackName(trackID);
            ViewBag.intakeName = intakeRepo.getIntakeName(intakeID);

            return View(Submetions);
        }



        [Route("CE/{Id?}")]
        public ActionResult Create(int Id, int intakeID, int trackID)
        {
            ViewBag.Courseid = Id;
            ViewBag.IntakeID = intakeID;
            ViewBag.TrackID = trackID;

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Createe(IFormCollection collection)
        {
            string InstructorId = userManager.GetUserId(User);
            string ExamName = collection["ExamName"];
            int Duration = int.Parse(collection["Duration"]);
            int courseId = int.Parse(collection["CourseId"]);
            int intakeID = int.Parse(collection["intakeID"]);
            int trackID = int.Parse(collection["trackID"]);
            string Json_Questions = collection["Questions"];

            Exam newExam = new Exam()
            {
                Name = ExamName,
                Duration = Duration,
                CourseID = courseId,
                InstructorID = InstructorId,
                CreationDate = DateTime.Now.Date,
            };

            examRepo.CreateExam(newExam);


            Question[] questionsArray = JsonConvert.DeserializeObject<Question[]>(Json_Questions);

            int[] questionsIDs = await questionRepo.CreateQuestion(questionsArray);

            List<Exam_Question> exam_Questions = new List<Exam_Question>();
            foreach (int Q in questionsIDs)
            {
                Exam_Question question = new Exam_Question()
                {
                    ExamID = newExam.ID,
                    QuestionID = Q,
                };

                exam_Questions.Add(question);
            }

            examQuestionRepo.CreateExam_Question(exam_Questions);

            return RedirectToAction(nameof(Index), new {courseId, intakeID , trackID });
        }


        [Route("D/{ExamID?}/{CourseId?}/{intakeID?}/{trackID?}")]
        public ActionResult Delete(int ExamID, int CourseId , int intakeID, int trackID)
        {
            exam_Std_QuestionRepository.DeleteExam_Std_Question(ExamID);

            List<int> QuestionIDs = examQuestionRepo.DeleteExamQuestions(ExamID);

            questionRepo.DeleteQuestions(QuestionIDs);

            examRepo.DeleteExam(ExamID);

            return RedirectToAction(nameof(Index), new { CourseId, intakeID, trackID });
        }
    }
}