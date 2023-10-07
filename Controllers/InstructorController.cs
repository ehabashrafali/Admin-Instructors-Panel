using Admin_Panel_ITI.Models;
using Admin_Panel_ITI.Repos;
using Admin_Panel_ITI.Repos.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> _userManager;


        public InstructorController(IInstructorRepository instructorRepository, UserManager<AppUser> userManager, ICourseRepository courseRepository, IInstructor_CourseRepository instructor_CourseRepository, IIntakeRepository intakeRepository, IIntake_InstructorRepository intake_InstructorRepository) {
            this.instructorRepository = instructorRepository;
            this.courseRepository = courseRepository;
            this.instructor_CourseRepository = instructor_CourseRepository;
            this.intakeRepository = intakeRepository;
            this.intake_InstructorRepository = intake_InstructorRepository;
            _userManager = userManager;
        }
        // GET: InstructorController
        public ActionResult Index(int pageNumber)
        {
            var instructors = instructorRepository.GetInstructors(pageNumber, 10);
            ViewBag.PageNumber = pageNumber;
            ViewBag.IntakeID = 0;
            return View(instructors);
        }
        public ActionResult InsIndexByIntakeId(int Id, int pageNumber)
        {
            var intakes = intakeRepository.GetAllIntakes();
            ViewData["Intakes"] = new SelectList(intakes, "ID", "Name");
            //var Instructors = intake_InstructorRepository.getInstructorbyIntakeID(Id, pageNumber, 10);
            var Instructors = instructorRepository.getInstructorbyIntakeID(Id, pageNumber, 10);
            ViewBag.PageNumber = pageNumber;
            ViewBag.IntakeID = Id;
            return View(Instructors);

        }

        public ActionResult UpdateTableData(int pageNumber, int intakeId)
        {
            if (intakeId == 0)
            {
                var instructors = instructorRepository.GetInstructors(pageNumber, 10);
                if (instructors.Count == 0 && pageNumber > 1)
                {
                    instructors = instructorRepository.GetInstructors(pageNumber - 1, 10);
                    pageNumber--;
                }
                ViewBag.PageNumber = pageNumber;
                return PartialView("_TableDataPartial", instructors);
            }
            else
            {
                var instructors = instructorRepository.getInstructorbyIntakeID(intakeId, pageNumber, 10);

                if (instructors.Count == 0 && pageNumber > 1)
                {
                    instructors = instructorRepository.getInstructorbyIntakeID(intakeId, pageNumber - 1, 10);
                    pageNumber--;
                }
                var intakes = intakeRepository.GetAllIntakes();
                ViewData["Intakes"] = new SelectList(intakes, "ID", "Name");
                ViewBag.PageNumber = pageNumber;
                ViewBag.IntakeID = intakeId;
                return PartialView("_TableDataPartial", instructors);
            }
        }


        // GET: InstructorController/Edit/5
        public ActionResult Edit(string id)
        {
            var instructor = instructorRepository.GetInstructorbyID(id);
            var intakes = intakeRepository.GetAllIntakes();
            List<Intake> intakessSelected = new List<Intake>();
            foreach (var item in instructor.IntakeInstructors)
            {
                intakessSelected.Add(item.Intake);
            }
            ViewBag.SelectedIntakes = intakessSelected;
            ViewBag.AllIntakes = new SelectList(intakes.Except(intakessSelected), "ID", "Name");
            return View(instructor);
        }

        // POST: InstructorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Instructor instructor, List<string> SelectedIntakeIds)
        {
            if (ModelState.IsValid)
            {

                foreach (var item in SelectedIntakeIds)
                {
                    Intake_Instructor ins = new Intake_Instructor()
                    {
                        IntakeID = int.Parse(item),
                        InstructorID = id
                    };
                    intake_InstructorRepository.AddIntake_Instructor(ins);

                }

                instructorRepository.UpdateInstructor(id, instructor);

                ViewBag.PageNumber = 0;

                return RedirectToAction(nameof(Index));
            }
            return View(instructor);
        }


        [HttpPost]
        public ActionResult RemoveIntake_Instructor(string intakeID, string insID)
        {
            intake_InstructorRepository.deleteIntake_Instructor(intakeID, insID);
            return RedirectToAction("Edit", new { id = insID });
        }

        public ActionResult Delete(List<string> selectedInstructorIds, int pageNumber, int intakeId)
        {
            instructorRepository.DeleteInstructor(selectedInstructorIds);

            if (intakeId == 0)
            {
                var instructors = instructorRepository.GetInstructors(pageNumber, 10);
                if (instructors.Count == 0 && pageNumber > 1)
                {
                    instructors = instructorRepository.GetInstructors(pageNumber - 1, 10);
                    pageNumber--;
                }
                ViewBag.PageNumber = pageNumber;
                return PartialView("_TableDataPartial", instructors);
            }
            else
            {
                var instructors = instructorRepository.getInstructorbyIntakeID(intakeId, pageNumber - 1, 10);

                if (instructors.Count == 0 && pageNumber > 1)
                {
                    instructors = instructorRepository.getInstructorbyIntakeID(intakeId, pageNumber - 1, 10);
                    pageNumber--;
                }
                var intakes = intakeRepository.GetAllIntakes();
                ViewData["Intakes"] = new SelectList(intakes, "ID", "Name");
                ViewBag.PageNumber = pageNumber;
                ViewBag.IntakeID = intakeId;
                return PartialView("_TableDataPartial", instructors);
            }
        }
    }
}



