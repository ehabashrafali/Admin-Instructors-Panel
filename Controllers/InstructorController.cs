using Admin_Panel_ITI.Models;
using Admin_Panel_ITI.Repos.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Admin_Panel_ITI.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IInstructorRepository instructorRepository;

        public InstructorController(IInstructorRepository instructorRepository) {
            this.instructorRepository = instructorRepository;
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
            return View(instructor);
        }

        // POST: InstructorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                instructorRepository.UpdateInstructor(id, instructor);
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
