using Admin_Panel_ITI.Models;
using Admin_Panel_ITI.Repos;
using Admin_Panel_ITI.Repos.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Admin_Panel_ITI.Areas.InstructorsArea.Controllers
{
    [Area("InstructorsArea")] //have to be added(mandatory)
    public class IntakeController : Controller
    {
        private readonly IIntakeRepository intakeRepo;
        private readonly UserManager<AppUser> userManager;
        private readonly IIntake_InstructorRepository intakeInstructorRepo;
        public IntakeController(IIntakeRepository _intakeRepo, 
            UserManager<AppUser> _userManager, 
            IIntake_InstructorRepository _intakeInstructorRepo)
        {
            intakeRepo = _intakeRepo;
            userManager = _userManager;
            intakeInstructorRepo = _intakeInstructorRepo;
        }


        //---// //List of all intakes that instructor works in only
        [Route("Index")]
        public ActionResult Index()
        {
            ViewBag.InstructorName = userManager.GetUserAsync(User).Result.FullName;

            var x = intakeInstructorRepo.GetIntakesByInstructorID(userManager.GetUserId(User));

            return View(x);
        }
    }
}
