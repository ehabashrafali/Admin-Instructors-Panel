using Admin_Panel_ITI.Models;
using Admin_Panel_ITI.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Admin_Panel_ITI.Areas.InstructorsArea.Controllers
{
    [Area("InstructorsArea")] //have to be added(mandatory)
    public class StudentController : Controller
    {
        private readonly IStudentRepository studentRepo;
        private readonly IIntakeRepository intakeRepo;
        private readonly ITrackRepository trackRepo;
        public StudentController(IStudentRepository _studentRepo,
            IIntakeRepository _intakeRepo,
            ITrackRepository _trackRepo)
        {
            studentRepo = _studentRepo;
            intakeRepo = _intakeRepo;
            trackRepo = _trackRepo;
        }


        [Route("SD/{id?}/{iid?}")]
        public ActionResult Details(int id , int iid) //id(TrackID) , iid(IntakeID)
        {
            ViewBag.TrackName = trackRepo.getTrackName(id);
            ViewBag.IntakeName = intakeRepo.getIntakeName(iid);
            ViewBag.TrackID = id;
            ViewBag.IntakeID = iid;

        
            return View(studentRepo.GetStudentsbyIntakeTrackID(iid, id));
        }
    }
}
