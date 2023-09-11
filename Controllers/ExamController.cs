using Admin_Panel_ITI.Models;
using Admin_Panel_ITI.Repos;
using Admin_Panel_ITI.Repos.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Admin_Panel_ITI.Controllers
{
    public class ExamController : Controller
    {
        private readonly IExamRepository examRepository;
        private readonly ITrackRepository trackRepository;
        private readonly IIntakeRepository intakeRepository;
        private readonly IExam_QuestionRepository exam_QuestionRepository;
        private readonly IExam_Std_QuestionRepository exam_Std_QuestionRepository;
        private readonly IInstructorRepository instructorRepository;
        private readonly IQuestionRepository questionRepository;

        public ICourseRepository CourseRepository { get; }

        public ExamController(IExamRepository examRepository, ITrackRepository trackRepository, IIntakeRepository intakeRepository, IExam_QuestionRepository exam_QuestionRepository,
            IExam_Std_QuestionRepository exam_Std_QuestionRepository,
            IInstructorRepository instructorRepository,
            ICourseRepository courseRepository, IQuestionRepository questionRepository)
        {
            this.examRepository = examRepository;
            this.trackRepository = trackRepository;
            this.intakeRepository = intakeRepository;
            this.exam_QuestionRepository = exam_QuestionRepository;
            this.exam_Std_QuestionRepository = exam_Std_QuestionRepository;
            this.instructorRepository = instructorRepository;
            CourseRepository = courseRepository;
            this.questionRepository = questionRepository;
        }

        // GET: ExamController
        public ActionResult Index(int pageNumber)
        {
            var exams = examRepository.GetExams(pageNumber, 10);

            return View(exams);
        }

        // GET: ExamController/DetailsForManager/5
        public ActionResult Details(int id)
        {
            var exam = examRepository.GetExambyID(id);
            return View(exam);
        }

        public ActionResult DetailsByCourseID(int Id)
        {
            return View(examRepository.GetExamsbycourseID(Id));
        }

        // GET: ExamController/Delete/5
        public ActionResult Delete(int id)
        {
            examRepository.DeleteExam(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: ExamController/Create
        public ActionResult Create()
        {

            var crs = CourseRepository.GetCourses();
            var Instructor = instructorRepository.GetInstructors();
            ViewBag.AllCourses = new SelectList(crs, "ID", "Name");
            ViewBag.AllInstructors = new SelectList(Instructor, "AspNetUserID", "AspNetUser.FullName");
            return View();
        }

        // POST: ExamController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Exam exam)
        {
            if (ModelState.IsValid)
            {
                examRepository.CreateExam(exam);
                return RedirectToAction("Index");
            }

            return View(exam);
        }

        // GET: ExamController/Edit/5
        public ActionResult Edit(int id)
        {
            var exam = examRepository.GetExambyID(id);

            var crs = CourseRepository.GetCourses();
            var Instructor = instructorRepository.GetInstructors();
            ViewBag.AllCourses = new SelectList(crs, "ID", "Name");
            ViewBag.AllInstructors = new SelectList(Instructor, "AspNetUserID", "AspNetUser.FullName");
            return View(exam);
        }
        // POST: ExamController/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(int id, Exam exam)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    examRepository.UpdateExam(id, exam);
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    ModelState.AddModelError(string.Empty, "An error occurred while updating the exam.");
                }
            }

            // If ModelState is not valid or an error occurred, re-populate the ViewBag and return to the Edit view.

            return View(exam);

        }



        //------------------------------------------------------------------------------------//

        public ActionResult CreateQuestionForExam(int Id)
        {
            var exam = examRepository.GetExambyID(Id);
            var question = new Question();

            ViewBag.Exam = exam;
            return View(question);
        }

        [HttpPost]
        public IActionResult CreateQuestionForExam(int Id, Question question)
        {
            if (ModelState.IsValid)
            {
                var exam = examRepository.GetExambyID(Id);

                var newQuestion = new Question
                {
                    Type = question.Type,
                    Body = question.Body,
                    Answer = question.Answer,
                    Mark = question.Mark,
                
                };

                questionRepository.CreateQuestion(newQuestion);

                var examQuestion = new Exam_Question
                {
                    QuestionID = newQuestion.ID,
                    ExamID = exam.ID
                };

                exam_QuestionRepository.CreateExam_Question(examQuestion);

                return RedirectToAction("Details", new { id = Id });
            }

            ViewBag.Exam = examRepository.GetExambyID(Id);
            return View(question);
        }

    }
}