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

        public InstructorController(IInstructorRepository instructorRepository, ICourseRepository courseRepository, IInstructor_CourseRepository instructor_CourseRepository) {
            this.instructorRepository = instructorRepository;
            this.courseRepository = courseRepository;
            this.instructor_CourseRepository = instructor_CourseRepository;
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
            ViewData["CourseList"] = new SelectList(courses, "ID", "Name");
            return View(instructor);
        }

        // POST: InstructorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Instructor instructor)
        {
            var selectedCourses = Request.Form["SelectedCourses"];
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
