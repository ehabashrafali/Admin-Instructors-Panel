using Admin_Panel_ITI.Models;
using Admin_Panel_ITI.Repos;
using Admin_Panel_ITI.Repos.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Admin_Panel_ITI.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IInstructorRepository instructorRepository;
        private readonly ICourseRepository courseRepository;
        private readonly IInstructor_CourseRepository instructor_CourseRepository;
        private readonly IIntakeRepository intakeRepository;
        private readonly IIntake_InstructorRepository intake_InstructorRepository;

        public InstructorController(IInstructorRepository instructorRepository, ICourseRepository courseRepository, IInstructor_CourseRepository instructor_CourseRepository, IIntakeRepository intakeRepository, IIntake_InstructorRepository intake_InstructorRepository) {
            this.instructorRepository = instructorRepository;
            this.courseRepository = courseRepository;
            this.instructor_CourseRepository = instructor_CourseRepository;
            this.intakeRepository = intakeRepository;
            this.intake_InstructorRepository = intake_InstructorRepository;
        }
        // GET: InstructorController
        public ActionResult Index(int pageNumber)
        {
            var instructors = instructorRepository.GetInstructors(pageNumber, 10);
            return View(instructors);
        }

        // GET: InstructorController/Details/5
        public ActionResult Details(string id)
        {
            var instructor = instructorRepository.GetInstructorbyID(id);
            ViewData["tracks"] = instructor.Tracks.Select(t=>t.Name).Distinct().ToList();
            ViewData["intakes"] = instructor.IntakeInstructors
                    .Select(itc => itc.Intake.Name)
                    .Distinct()
                    .ToList();
            ViewData["courses"] = instructor.InstructorCourses
                    .Select(itc => itc.Course.Name)
                    .Distinct()
                    .ToList();

            return View(instructor);
        }

        
        // GET: InstructorController/Edit/5
        public ActionResult Edit(string id)
        {
            var instructor = instructorRepository.GetInstructorbyID(id);
            var courses = courseRepository.GetCourses();
            var intakes = intakeRepository.GetIntakes();
            ViewData["CourseList"] = new SelectList(courses, "ID", "Name");
            ViewData["IntakeList"] = new SelectList(intakes, "ID", "Name");
            return View(instructor);
        }

        // POST: InstructorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Instructor instructor)
        {
            var selectedCourses = Request.Form["SelectedCourses"];
            var selectedIntakes = Request.Form["selectedIntakes"];
            if (ModelState.IsValid)
            {
                instructorRepository.UpdateInstructor(id, instructor);
                foreach (var item in selectedCourses)
                {
                    Instructor_Course ic_record = new Instructor_Course() {
                        CourseID = int.Parse(item),
                        InstructorID = id
                  
                    };
                    instructor_CourseRepository.CreateInstructor_Course(ic_record);
                }
                foreach (var item in selectedIntakes)
                {
                    Intake_Instructor ic_record = new Intake_Instructor()
                    {
                        IntakeID = int.Parse(item),
                        InstructorID = id

                    };
                    intake_InstructorRepository.AddIntake_Instructor(ic_record);
                }

                return RedirectToAction("Index");
            }
            return View(instructor);
        }

        // GET: InstructorController/Delete/5
        public ActionResult Delete(string id)
        {
            var instructor = instructorRepository.GetInstructorbyID(id);
            return View(instructor);
        }

        // POST: InstructorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, Instructor instructor)
        {
            instructorRepository.DeleteInstructor(id);
            return RedirectToAction("index");
        }
    }
}
