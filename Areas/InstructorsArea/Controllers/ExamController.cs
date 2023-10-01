using Admin_Panel_ITI.Repos.Interfaces;
using Admin_Panel_ITI.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using Admin_Panel_ITI.Areas.InstructorsArea.ViewModels;
using Admin_Panel_ITI.Models;

namespace Admin_Panel_ITI.Areas.InstructorsArea.Controllers
{
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
        public ExamController(IExamRepository examRepository, ITrackRepository trackRepository, 
            IIntakeRepository intakeRepository, IExam_QuestionRepository exam_QuestionRepository,
          IExam_Std_QuestionRepository exam_Std_QuestionRepository,
          IInstructorRepository instructorRepository,
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
        }

        // GET: ExamController
        public ActionResult Index()
        {
            return View();
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

        public ActionResult Create()
        {
            Console.WriteLine("ehabashrafaliahmed");
            var EQ = new Exam_QuestionsVM();
            return View(EQ);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Exam_QuestionsVM model)
        {
            if (ModelState.IsValid)
            {
                // Create a new Exam object and populate it with data from the model.
                var newExam = new Exam
                {
                    Name = model.ExamName,
                    Duration = model.Duration,
                };

                var Question = new Question
                {
                    Body = model.Body,
                    Answer = model.Answer,
                    Mark = (double)model.Mark,
                    
                };
                  
                questionRepository.CreateQuestion(Question);


                // Call the repository method to create the exam.
                examRepository.CreateExam(newExam);
                //return RedirectToAction("Details", new { id = newExam.ID });
            }
            return View(model);

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
        }
    }

