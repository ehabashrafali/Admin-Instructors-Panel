using Admin_Panel_ITI.Areas.InstructorsArea.ViewModels;
using Admin_Panel_ITI.Models;
using Admin_Panel_ITI.Repos;
using Admin_Panel_ITI.Repos.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Admin_Panel_ITI.Areas.InstructorsArea.Controllers
{
    [Area("InstructorsArea")] //have to be added(mandatory)
    public class CourseController : Controller
    {
        private readonly ICourseRepository icourseRepo;
        private readonly UserManager<AppUser> userManager;
        private readonly IIntake_Track_CourseRepository intakeTrackCourseRepo;
        private readonly IInstructor_CourseRepository instructorCourseRepo;
        public CourseController(
            ICourseRepository _icourseRepo,
            UserManager<AppUser> _userManager,
            IIntake_Track_CourseRepository _intaketrackCourseRepo,
            IInstructor_CourseRepository _instructorCourseRepo)
        {
            icourseRepo = _icourseRepo;
            userManager = _userManager;
            intakeTrackCourseRepo = _intaketrackCourseRepo;
            instructorCourseRepo = _instructorCourseRepo;
        }



        //get the courses in this track in this specific Intake. id(intakeID) , iid(TrackID) , name2(IntakeName)
        public ActionResult DetailsForManager(int id, int iid, string name, string name2)
        {
            ViewBag.TrackName = name; 
            ViewBag.IntakeName = name2; 
            ViewBag.IntakeID = id;
            ViewBag.TrackID = iid;

            List<InstructorCourseVM> instructorCourseVM = new(); //list of the new view model

            var AllcoursesInTrack = icourseRepo.GetCoursesByIntakeTrackID(id, iid); //all courses in a specific track and intake

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


        public ActionResult DetailsForTeacher(int id, int iid, string name, string name2)
        {
            ViewBag.TrackName = name;
            ViewBag.IntakeName = name2;
            ViewBag.IntakeID = id;
            ViewBag.TrackID = iid;

            string InstructorID = userManager.GetUserId(User);

            return View(icourseRepo.GetTeacherCourses(id, iid, InstructorID));
        }
    }
}
