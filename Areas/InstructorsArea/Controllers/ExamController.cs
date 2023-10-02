using Admin_Panel_ITI.Models;
using Admin_Panel_ITI.Repos.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace Admin_Panel_ITI.Areas.InstructorsArea.Controllers
{
    [Area("InstructorsArea")]
    public class ExamController : Controller
    {
        private readonly IExamRepository examRepo;
        private readonly IQuestionRepository questionRepo;
        private readonly IExam_QuestionRepository examQuestionRepo;
        private readonly UserManager<AppUser> userManager;
        public ExamController(IExamRepository _examRepo, 
            IQuestionRepository _questionRepo,
            IExam_QuestionRepository _examQuestionRepo,
            UserManager<AppUser> _userManager)
        {
            examRepo = _examRepo;
            questionRepo = _questionRepo;
            examQuestionRepo = _examQuestionRepo;
            userManager = _userManager;
        }



        // GET: ExamController
        public ActionResult Index(int pageNumber)
        {
            var exams = examRepo.GetExams(pageNumber, 10);

            return View(exams);
        }

        // GET: ExamController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        [HttpGet]
        public ActionResult Create(int id)
        {
            ViewBag.Courseid = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Createe(IFormCollection collection)
        {
            string InstructorId = userManager.GetUserId(User);
            string ExamName = collection["ExamName"];
            int Duration = int.Parse( collection["Duration"]);
            int CourseId = int.Parse(collection["CourseId"]);
            string Json_Questions = collection["Questions"];

            Exam newExam = new Exam()
            {
                Name = ExamName,
                Duration = Duration,
                CourseID = CourseId,
                InstructorID = InstructorId,
                CreationDate = DateTime.Now.Date,
            };

            examRepo.CreateExam(newExam);


            Question[] questionsArray = JsonConvert.DeserializeObject<Question[]>(Json_Questions);

            int[] questionsIDs = await questionRepo.CreateQuestion(questionsArray);


            List<Exam_Question> exam_Questions = new List<Exam_Question>();
            foreach(int Q in questionsIDs)
            {
                Exam_Question question = new Exam_Question()
                {
                    ExamID = newExam.ID,
                    QuestionID = Q,
                };

                exam_Questions.Add(question);
            }

            examQuestionRepo.CreateExam_Question(exam_Questions);

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
            examRepo.DeleteExam(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: ExamController/Delete/5
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
