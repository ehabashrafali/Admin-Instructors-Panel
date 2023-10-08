using Admin_Panel_ITI.Controllers;
using Admin_Panel_ITI.Models;
using Admin_Panel_ITI.Repos;
using Microsoft.AspNetCore.Mvc;

namespace Admin_Panel_ITI.Areas.InstructorsArea.Controllers
{
    [Area("InstructorsArea")] //have to be added(mandatory)

    public class SubmissionsController : Controller
    {
        private readonly IStudent_SubmissionRepository student_SubmissionRepo;

        public SubmissionsController(IStudent_SubmissionRepository student_SubmissionRepo)
        {
            this.student_SubmissionRepo = student_SubmissionRepo;
        }

        public IActionResult Index(int id, string name, int intakeID, int trackID, string intakeName, string trackName, int CourseDayNum,int crsDay)
        {

            var all_submissions = student_SubmissionRepo.GetAll_SubmissionsByCrsDay(crsDay);

            ViewBag.Id = id;
            ViewBag.Name = name;

            ViewBag.IntakeName = intakeName;
            ViewBag.TrackName = trackName;
            ViewBag.IntakeID = intakeID;
            ViewBag.TrackID = trackID;
            ViewBag.CourseDayId = crsDay;
            ViewBag.CourseDayNum = CourseDayNum;

            return View(all_submissions);

        }


        public void UpdateGrade(string studentId, int grade, int courseDayId)
        {
            student_SubmissionRepo.UpdateGrade(studentId,courseDayId,grade);

        }

    }
}
