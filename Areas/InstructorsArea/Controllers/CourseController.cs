using Admin_Panel_ITI.Areas.InstructorsArea.ViewModels;
using Admin_Panel_ITI.Models;
using Admin_Panel_ITI.Repos;
using Admin_Panel_ITI.Repos.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Studebt_Panel_ITI.Areas.InstructorsArea.Controllers
{
    [Area("InstructorsArea")] //have to be added(mandatory)
    public class CourseController : Controller
    {
        private readonly ICourseRepository icourseRepo;
        private readonly UserManager<AppUser> userManager;
        private readonly IInstructor_CourseRepository instructorCourseRepo;
        private readonly IIntakeRepository intakeRepo;
        private readonly ITrackRepository trackRepo;
        public CourseController(
            ICourseRepository _icourseRepo,
            UserManager<AppUser> _userManager,
            IInstructor_CourseRepository _instructorCourseRepo, 
            IIntakeRepository _intakeRepo, 
            ITrackRepository _trackRepo)
        {
            icourseRepo = _icourseRepo;
            userManager = _userManager;
            instructorCourseRepo = _instructorCourseRepo;
            intakeRepo = _intakeRepo;
            trackRepo = _trackRepo;
        }



        //get the courses in this track in this specific Intake. id(intakeID) , iid(TrackID) , name2(IntakeName)
        [Route("DFM/{id?}/{iid?}")]
        public ActionResult DetailsForManager(int id, int iid)
        {
            ViewBag.TrackName = trackRepo.getTrackName(id); 
            ViewBag.IntakeName = intakeRepo.getIntakeName(iid);
            ViewBag.IntakeID = iid;
            ViewBag.TrackID = id;

            List<InstructorCourseVM> instructorCourseVM = new(); //list of the new view model

            var AllcoursesInTrack = icourseRepo.GetCoursesByIntakeTrackID(iid, id); //all courses in a specific track and intake

            foreach (var course in AllcoursesInTrack)
            {
                string? instructorName = instructorCourseRepo.GetInstructorTeachCourse(course.ID)?.Instructor?.AspNetUser?.FullName ?? "NA"; //get the instructor that teach a specific course

                instructorCourseVM.Add(new InstructorCourseVM
                {
                    CourseName = course.Name,
                    Duration = course.Duration,
                    InstructorName = instructorName,
                    AdminName = course.Admin.AspNetUser.FullName
                });
            }


            return View(instructorCourseVM);


            #region additional info
            //int intakeId = (int)HttpContext.Session.GetInt32("IntakeID"); //get the intakeID in this session: GetInt32 returns nullable integer(int?) so convert it to (int) only.
            #endregion
        }


    
        [Route("DFT/{trackId?}/{intakeId?}")]
        public ActionResult DetailsForTeacher(int trackId, int intakeId)
        {
            ViewBag.TrackName = trackRepo.getTrackName(trackId);
            ViewBag.IntakeName = intakeRepo.getIntakeName(intakeId);
            ViewBag.IntakeID = intakeId;
            ViewBag.TrackID = trackId;

            string InstructorID = userManager.GetUserId(User);

            return View(icourseRepo.GetTeacherCourses(intakeId, trackId , InstructorID));
        }
    }
}
