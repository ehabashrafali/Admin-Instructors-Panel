using Admin_Panel_ITI.Models;
using Admin_Panel_ITI.Repos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Admin_Panel_ITI.Areas.InstructorsArea.Controllers
{
    [Area("InstructorsArea")] //have to be added(mandatory)
    public class TrackController : Controller
    {
        private readonly ITrackRepository itrackRepo;
        private readonly UserManager<AppUser> userManager;
        private readonly IIntakeRepository intakeRepo;
        public TrackController(
            ITrackRepository _itrackRepo,
            UserManager<AppUser> _userManager, 
            IIntakeRepository _intakeRepo)
        {
            itrackRepo = _itrackRepo;
            userManager = _userManager;
            intakeRepo = _intakeRepo;
        }


        //get the tracks in a specific intake, and only the tracks this instructor manage | id = intake id
        [Route("DFM/{id?}")]
        public ActionResult DetailsForManager(int id)
        {
            string? UserID = userManager.GetUserId(User);

            ViewBag.IntakeID = id;
            ViewBag.intakeName = intakeRepo.getIntakeName(id);

            return View(itrackRepo.getTracks(UserID, id));


            #region additional info
            //HttpContext.Session.SetInt32("IntakeID", id); //store the intakeID in session state to use it in the whole app during session lifetime. 
            #endregion
        }




        //get the tracks in a specific intake, and only the tracks this instructor Teach in | id = intake id
        [Route("/DFT/{id?}")]
        public ActionResult DetailsForTeacher(int id)
        {
            ViewBag.IntakeName = intakeRepo.getIntakeName(id);
            ViewBag.IntakeID = id;

            return View(itrackRepo.GetTracksByTeacher(id, userManager.GetUserId(User)));


            #region additional info
            //HttpContext.Session.SetInt32("IntakeID", id); //store the intakeID in session state to use it in the whole app during session lifetime. 
            #endregion
        }


    }
}
