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
        public IIntakeRepository intakeRepository { get; }


        // GET: StudentController

        public StudentController(IStudentRepository studentRepository, ICourseRepository courseRepository, ITrackRepository trackRepository, IStudent_SubmissionRepository student_SubmissionRepository, IIntakeRepository intakeRepository)
        {
            this.studentRepository = studentRepository;
            this.courseRepository = courseRepository;
            this.trackRepository = trackRepository;
            this.student_SubmissionRepository = student_SubmissionRepository;
            this.intakeRepository = intakeRepository;
        }
        public ActionResult Index(int pageNumber)
        {
            var intakes = intakeRepository.GetAllIntakes();
            ViewData["Intakes"] = new SelectList(intakes, "ID", "Name");

            var students = studentRepository.getStudents(pageNumber, 10);
            ViewBag.PageNumber = pageNumber;
            ViewBag.IntakeID = 0;

            return View(students);
        }


        public ActionResult UpdateTableData(int intakeID, int pageNumber)
        {
            var intakes = intakeRepository.GetAllIntakes();

            List<Student> studentsbyintake;
            if (intakeID == 0)
            {
                // Get all tracks without filtering by intake ID
                studentsbyintake = studentRepository.getStudents(pageNumber, 10);
                if(studentsbyintake.Count == 0 && pageNumber > 1)
                {
                    studentsbyintake = studentRepository.getStudents(pageNumber - 1, 10);
                    pageNumber--;
                }
              
            }
            else
            {
                // Get tracks filtered by intake ID
                studentsbyintake = studentRepository.getStudentsbyIntakeID(intakeID, pageNumber, 10);
                if (studentsbyintake.Count == 0 && pageNumber > 1)
                {
                    studentsbyintake = studentRepository.getStudentsbyIntakeID(intakeID, pageNumber, 10);
                    pageNumber--;
                }
            }

           
            ViewData["Intakes"] = new SelectList(intakes, "ID", "Name");
            ViewBag.PageNumber = pageNumber;
            ViewBag.IntakeID = intakeID;
            return PartialView("_TableDataPartial", studentsbyintake);
        }


        //public ActionResult StdIndexByTrackId(int Trackid, int pageNumber)
        //{
            
        //    var students = studentRepository.getStudentsbyTrackID(Trackid, pageNumber, 10);
        //    return View(students);
        //}

        //public ActionResult StdIndexByIntakeId(int Id, int pageNumber)
        //{
        //    var students = studentRepository.getStudentsbyIntakeID(Id, pageNumber, 10);
        //    return PartialView("_TableDataPartial", students);
        //}


        //// GET: StudentController/Create
        //public ActionResult Create()
        //{
        //    var Tracks = trackRepository.getTracks();
        //    var IntakesIDs = IntakeRepository.GetAllIntakes();
        //    ViewBag.AllTracks = new SelectList(Tracks, "ID", "ExamName");
        //    ViewBag.AllIntakes = new SelectList(IntakesIDs, "ID", "ExamName");
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

            var Intakes = intakeRepository.GetIntakes();
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




       


        [HttpPost]
        public IActionResult Delete(List<string> Stdids, int intakeID, int pageNumber)
        {
            studentRepository.DeleteStudent(Stdids);

            var intakes = intakeRepository.GetAllIntakes();

            List<Student> studentsbyintake;
            if (intakeID == 0)
            {
                // Get all tracks without filtering by intake ID
                studentsbyintake = studentRepository.getStudents(pageNumber, 10);
                if (studentsbyintake.Count == 0 && pageNumber > 1)
                {
                    studentsbyintake = studentRepository.getStudents(pageNumber - 1, 10);
                    pageNumber--;
                }

            }
            else
            {
                // Get tracks filtered by intake ID
                studentsbyintake = studentRepository.getStudentsbyIntakeID(intakeID, pageNumber, 10);
                if (studentsbyintake.Count == 0 && pageNumber > 1)
                {
                    studentsbyintake = studentRepository.getStudentsbyIntakeID(intakeID, pageNumber, 10);
                    pageNumber--;
                }
            }


            ViewData["Intakes"] = new SelectList(intakes, "ID", "Name");
            ViewBag.PageNumber = pageNumber;
            ViewBag.IntakeID = intakeID;
            return PartialView("_TableDataPartial", studentsbyintake);
        }

    }
}
