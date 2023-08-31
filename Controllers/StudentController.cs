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
        private readonly IIntake_TrackRepository intake_TrackRepository;

        public ITrackRepository trackRepository { get; }
        public IIntakeRepository IntakeRepository { get; }


        // GET: StudentController

        public StudentController(IStudentRepository studentRepository, ICourseRepository courseRepository, ITrackRepository trackRepository, IStudent_SubmissionRepository student_SubmissionRepository, IIntake_TrackRepository intake_TrackRepository, IIntakeRepository intakeRepository)
        {
            this.studentRepository = studentRepository;
            this.courseRepository = courseRepository;
            this.trackRepository = trackRepository;
            this.student_SubmissionRepository = student_SubmissionRepository;
            this.intake_TrackRepository = intake_TrackRepository;
            IntakeRepository = intakeRepository;
        }
        public ActionResult StdsIndex(int? page)
        {
            int pageSize = 30;
            int pageNumber = page ?? 1;
            var students = studentRepository.getStudents().ToPagedList(pageNumber, pageSize);
            return View(students);
        }

        public ActionResult StdIndexByTrackId(int Trackid, int? page)
        {
            int pageSize = 30;
            int pageNumber = page ?? 1;
            var students = studentRepository.getStudentsbyTrackID(Trackid).ToPagedList(pageNumber, pageSize);
            return View(students);
        }

        public ActionResult StdIndexByIntakeId(int IntakeId, int? page)
        {
            int pageSize = 30;
            int pageNumber = page ?? 1;
            var students = studentRepository.getStudentsbyIntakeID(IntakeId).ToPagedList(pageNumber, pageSize);
            return View(students);
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            var Tracks = trackRepository.getTracks();
            ViewBag.AllTracks = new SelectList(Tracks, "ID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                studentRepository.CreateStudent(student);
                return RedirectToAction(nameof(StdsIndex));
            }
            return View(student);
        }

        public ActionResult StudentDetails(int id)
        {
            return View(studentRepository.getStudentbyID(id));
        }


        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {

            var Intake = IntakeRepository.GetIntakes();
            ViewBag.AllIntakes = new SelectList(Intake, "ID", "Name");
            var student = studentRepository.getStudentbyID(id);
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Student student)
        {
            if (ModelState.IsValid)
            {
                studentRepository.UpdateStudent(id, student);
                return RedirectToAction(nameof(StdsIndex));
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

        public IActionResult Delete(int id)
        {
            studentRepository.DeleteStudent(id);
            return RedirectToAction(nameof(StdsIndex));
        }


    }
}
