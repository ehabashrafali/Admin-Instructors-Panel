using Admin_Panel_ITI.Models;
using Admin_Panel_ITI.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Admin_Panel_ITI.Controllers
{
    public class IntakeController : Controller
    {
        private readonly IIntakeRepository intakeRepository;
        private readonly UserManager<AppUser> userManager;
        public IntakeController(IIntakeRepository _intakeRepository, UserManager<AppUser> _userManager)
        {
            intakeRepository = _intakeRepository;
            userManager = _userManager;
        }


        public ActionResult Index()
        {
            return View(intakeRepository.GetAllIntakes());
        }


        /*------------------------------------------------------*/

        public ActionResult Details(int id)
        {
            return View();
        }


        /*------------------------------------------------------*/


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Intake newIntake)
        {
            if (ModelState.IsValid)
            {
                //var user = userManager.GetUserAsync(User).Result; 


                string adminID = userManager.GetUserId(User);  //get the currently logged-in AdminID

                newIntake.CreationDate = DateTime.Now;
                newIntake.AdminID = adminID;

                intakeRepository.CreateIntake(newIntake);

                return RedirectToAction(nameof(Index));
            }

            return View();
        }


       /*------------------------------------------------------*/


        public ActionResult Edit(int id) //id = intake ID
        {            
            return View(intakeRepository.getIntakebyID(id));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Intake editedIntake)
        {
            if(ModelState.IsValid)
            {
                //Intake oldIntake = intakeRepository.getIntakebyID(id);
                //oldIntake.Name = editedIntake.Name; 
                //oldIntake.Duration = editedIntake.Duration;
                //oldIntake.StartDate = editedIntake.StartDate;
                //oldIntake.EndDate = editedIntake.EndDate;

                //intakeRepository.CreateIntake(oldIntake); 

                intakeRepository.UpdateIntake(id, editedIntake);
                
                return RedirectToAction(nameof(Index));   
            }

            return View();
        }


        /*------------------------------------------------------*/


        public ActionResult Delete(int id) //id = Intake ID
        {
            intakeRepository.DeleteIntake(id);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
