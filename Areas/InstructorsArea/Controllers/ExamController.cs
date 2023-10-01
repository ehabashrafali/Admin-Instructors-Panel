using Admin_Panel_ITI.Repos.Interfaces;
using Admin_Panel_ITI.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using Admin_Panel_ITI.Areas.InstructorsArea.ViewModels;
using Admin_Panel_ITI.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;

namespace Admin_Panel_ITI.Areas.InstructorsArea.Controllers
{
    [Area("InstructorsArea")] //have to be added(mandatory)

    public class ExamController : Controller
    {
        private readonly IExamRepository examRepository;
        private readonly ITrackRepository trackRepository;
        private readonly IIntakeRepository intakeRepository;
        private readonly IExam_QuestionRepository exam_QuestionRepository;
        private readonly IExam_Std_QuestionRepository exam_Std_QuestionRepository;
        private readonly IInstructorRepository instructorRepository;
        private readonly ICourseRepository courseRepositoy;
        private readonly IQuestionRepository questionRepository;
        private readonly UserManager<AppUser> _userManager;
        public ExamController(IExamRepository examRepository, ITrackRepository trackRepository,
            IIntakeRepository intakeRepository, IExam_QuestionRepository exam_QuestionRepository,
          IExam_Std_QuestionRepository exam_Std_QuestionRepository,
          IInstructorRepository instructorRepository, UserManager<AppUser> userManager,
          ICourseRepository courseRepositoy, IQuestionRepository questionRepository)
        {
            this.examRepository = examRepository;
            this.trackRepository = trackRepository;
            this.intakeRepository = intakeRepository;
            this.exam_QuestionRepository = exam_QuestionRepository;
            this.exam_Std_QuestionRepository = exam_Std_QuestionRepository;
            this.instructorRepository = instructorRepository;
            this.courseRepositoy = courseRepositoy;
            this.questionRepository = questionRepository;
            _userManager = userManager;

        }

        // GET: ExamController
        public ActionResult Index(int pageNumber)
        {
            var exams = examRepository.GetExams(pageNumber, 10);

            return View(exams);
        }

        // GET: ExamController/Details/5
        public ActionResult Details(int id)
        {
            var exam = examRepository.GetExambyID(id);
            return View(exam);
        }

        public ActionResult DetailsByCourseID(int Id)
        {
            return View(examRepository.GetExamsbycourseID(Id));
        }

        public ActionResult Create(int Id)
        {
            ViewBag.CourseId = Id;
            return View();
        }

        public async Task<ActionResult> AddExam(string Name, string Duration, string Questions, int CourseId)
        {

            var user = await _userManager.GetUserAsync(User);
            string instructorId = user.Id;

            // Deserialize the Questions parameter back to a list of questions
            List<Question> questions = JsonConvert.DeserializeObject<List<Question>>(Questions);

            var IDs = questionRepository.CreateQuestion(questions);

            Exam ex = new Exam()
            {
                Name = Name,
                Duration = int.Parse(Duration),
                CreationDate = DateTime.Now,
                CourseID = CourseId,
                InstructorID = instructorId
            };

            var exId = examRepository.CreateExam(ex);


            foreach (var qId in IDs)
            {
                var ex_q = new Exam_Question()
                {
                    ExamID = exId,
                    QuestionID = qId
                };
                exam_QuestionRepository.CreateExam_Question(ex_q);
            }


            return RedirectToAction(nameof(Index));
        }

        // GET: ExamController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ExamController/Edit/5
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

        // GET: ExamController/Delete/5
        public ActionResult Delete(int id)
        {
            examRepository.DeleteExam(id);
            return RedirectToAction(nameof(Index));
        }

    }
}