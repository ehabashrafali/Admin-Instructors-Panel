using Admin_Panel_ITI.Controllers;
using Admin_Panel_ITI.Models;
using Admin_Panel_ITI.Repos;
using Admin_Panel_ITI.Repos.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Admin_Panel_ITI.Areas.InstructorsArea.Controllers
{
    [Area("InstructorsArea")] //have to be added(mandatory)
    public class SubmissionsController : Controller
    {
        private readonly IStudent_SubmissionRepository student_SubmissionRepo;
        private readonly IIntakeRepository intakeRepo;
        private readonly ITrackRepository trackRepo;
        private readonly ICourseDayRepository courseDayRepo;
        private readonly ICourseRepository courseRepo;

        public SubmissionsController(IStudent_SubmissionRepository student_SubmissionRepo,
            IIntakeRepository _intakeRepo, 
            ITrackRepository _trackRepo, 
           ICourseDayRepository _courseDayRepo,
           ICourseRepository _courseRepo)
        {
            this.student_SubmissionRepo = student_SubmissionRepo;
            intakeRepo = _intakeRepo;
            trackRepo = _trackRepo;
            courseDayRepo = _courseDayRepo;
            courseRepo = _courseRepo;
        }


        [Route("SI/{id?}/{intakeID?}/{trackID?}/{CourseDayId?}")]
        public IActionResult Index(int id, int intakeID, int trackID, int CourseDayId)
        {

            var all_submissions = student_SubmissionRepo.GetAll_SubmissionsByCrsDay(CourseDayId);

            ViewBag.Id = id;
            ViewBag.IntakeID = intakeID;
            ViewBag.TrackID = trackID;
            ViewBag.CourseDayId = CourseDayId;
            ViewBag.Name = courseRepo.GetCourseName(id);
            ViewBag.IntakeName = intakeRepo.getIntakeName(intakeID);
            ViewBag.TrackName = trackRepo.getTrackName(trackID);
            ViewBag.CourseDayNum = courseDayRepo.GetCourseDaybyID(CourseDayId).DayNumber;

            return View(all_submissions);

        }


        public void UpdateGrade(string studentId, int grade, int courseDayId)
        {
            student_SubmissionRepo.UpdateGrade(studentId,courseDayId,grade);

        }

    }
}
