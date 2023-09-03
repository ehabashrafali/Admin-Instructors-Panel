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

        public ICourseRepository CourseRepository { get; }

        public ExamController(IExamRepository examRepository , ITrackRepository trackRepository , IIntakeRepository intakeRepository, IExam_QuestionRepository exam_QuestionRepository, IExam_Std_QuestionRepository exam_Std_QuestionRepository, IInstructorRepository  instructorRepository , ICourseRepository courseRepository)
        {
            this.examRepository = examRepository;
            this.trackRepository = trackRepository;
            this.intakeRepository = intakeRepository;
            this.exam_QuestionRepository = exam_QuestionRepository;
            this.exam_Std_QuestionRepository = exam_Std_QuestionRepository;
            this.instructorRepository = instructorRepository;
            CourseRepository = courseRepository;
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

        public ActionResult DetailsByCourseID(int crsid)
        {
            return View(examRepository.GetExamsbycourseID(crsid));
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


   

        //// POST: ExamController/Delete/5
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
