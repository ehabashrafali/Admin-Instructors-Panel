using Admin_Panel_ITI.Repos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using Microsoft.AspNetCore.Mvc.Rendering;
using Admin_Panel_ITI.Repos;
using Admin_Panel_ITI.Models;
using System.Drawing.Printing;

namespace Admin_Panel_ITI.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly ICourseRepository courseRepository;
        private readonly IStudent_SubmissionRepository student_SubmissionRepository;

        public ITrackRepository trackRepository { get; }
        public IIntakeRepository IntakeRepository { get; }


        // GET: StudentController

        public StudentController(IStudentRepository studentRepository, ICourseRepository courseRepository, ITrackRepository trackRepository, IStudent_SubmissionRepository student_SubmissionRepository, IIntakeRepository intakeRepository)
        {
            this.studentRepository = studentRepository;
            this.courseRepository = courseRepository;
            this.trackRepository = trackRepository;
            this.student_SubmissionRepository = student_SubmissionRepository;
            IntakeRepository = intakeRepository;
        }
        public ActionResult Index(int intakeID = 0, int pageNumber = 1)
        {
            var intakes = IntakeRepository.GetAllIntakes();
            ViewData["Intakes"] = new SelectList(intakes, "ID", "Name");

            if (intakeID != 0)
            {
                // Filter students by intake
                var students = studentRepository.getStudentsbyIntakeID(intakeID, pageNumber, 10);
                ViewBag.IntakeID = intakeID;
                ViewBag.PageNumber = pageNumber;
                return View(students);
            }
            else
            {
                // Show all students if no intake filter is applied
                ViewBag.IntakeID = 0;
                var students = studentRepository.getStudents(pageNumber, 10);
                ViewBag.PageNumber = pageNumber;
                return View(students);
            }
        }


        public ActionResult StdIndexByTrackId(int Trackid, int pageNumber)
        {
            
            var students = studentRepository.getStudentsbyTrackID(Trackid, pageNumber, 10);
            return View(students);
        }

        public ActionResult StdIndexByIntakeId(int Id, int pageNumber)
        {
            var students = studentRepository.getStudentsbyIntakeID(Id, pageNumber, 10);
            return PartialView("_TableDataPartial", students);
        }


        //// GET: StudentController/Create
        //public ActionResult Create()
        //{
        //    var Tracks = trackRepository.getTracks();
        //    var Intakes = IntakeRepository.GetAllIntakes();
        //    ViewBag.AllTracks = new SelectList(Tracks, "ID", "Name");
        //    ViewBag.AllIntakes = new SelectList(Intakes, "ID", "Name");
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreateStudent(Student student)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        studentRepository.CreateStudent(student);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(student);
        //}

        public ActionResult Details(string id)
        {
            return View(studentRepository.getStudentbyID(id));
        }


        // GET: StudentController/Edit/5
        public ActionResult Edit(string id)
        {

            var Intakes = IntakeRepository.GetIntakes();
            var trackes = trackRepository.getTracks();
            ViewBag.AllIntakes = new SelectList(Intakes, "ID", "Name");
            ViewBag.AllTracks = new SelectList(trackes, "ID", "Name");
            var student = studentRepository.getStudentbyID(id);
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Student student)
        {
            if (ModelState.IsValid)
            {
                studentRepository.UpdateStudent(id, student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        //public IActionResult TracksWithStudentNumber(int intakeId, int? page)
        //{

        //    int pageSize = 30;
        //    int pageNumber = page ?? 1;
        //    List<Intake_Track> intakeTracks = intake_TrackRepository.GetIntake_TrackbyintakeID(intakeId);
        //    var pagedList = intakeTracks.ToPagedList(pageNumber, pageSize);
        //        return View(pagedList);
        //}

        // GET: StudentController/Delete/5




        public ActionResult UpdateTableData(int intakeID, int pageNumber)
        {
            List<Student> students;

            if (intakeID != 0)
            {
                students = studentRepository.getStudentsbyIntakeID(intakeID, pageNumber, 10);
            }
            else
            {
                students = studentRepository.getStudents(pageNumber, 10);
            }

            if (students.Count == 0 && pageNumber > 1)
            {
                if (intakeID != 0)
                {
                    students = studentRepository.getStudentsbyIntakeID(intakeID, pageNumber - 1, 10);
                }
                else
                {
                    students = studentRepository.getStudents(pageNumber - 1, 10);
                }
                pageNumber--;
            }

            ViewBag.PageNumber = pageNumber;
            return PartialView("_TableDataPartial", students);
        }



        [HttpPost]
        public IActionResult Delete(List<string> Stdids)
        {
            studentRepository.DeleteStudent(Stdids);

            return RedirectToAction(nameof(Index));
        }

    }
}
