using Admin_Panel_ITI.Repos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using Microsoft.AspNetCore.Mvc.Rendering;
using Admin_Panel_ITI.Repos;
using Admin_Panel_ITI.Models;

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
        public ActionResult Index(int pageNumber)
        {
        
            var students = studentRepository.getStudents(pageNumber,10);
            return View(students);
        }

        public ActionResult StdIndexByTrackId(int Trackid, int pageNumber)
        {
            
            var students = studentRepository.getStudentsbyTrackID(Trackid, pageNumber, 10);
            return View(students);
        }

        public ActionResult StdIndexByIntakeId(int IntakeId, int pageNumber)
        {

            var students = studentRepository.getStudentsbyIntakeID(IntakeId, pageNumber,10);
            return View(students);
        }

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

        public IActionResult Delete(string id)
        {
            studentRepository.DeleteStudent(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
