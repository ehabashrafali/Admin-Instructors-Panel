using Admin_Panel_ITI.Models;
using Admin_Panel_ITI.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Admin_Panel_ITI.Repos.Interfaces;
using System.Collections.Generic;

namespace Admin_Panel_ITI.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository courseRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITrackRepository trackRepository;
        private readonly IInstructorRepository instructorRepository;
        private readonly IInstructor_CourseRepository instructor_CourseRepository;
        private readonly IIntakeRepository intakeRepository;

        public CourseController(ICourseRepository courseRepository, UserManager<AppUser> userManager, ITrackRepository trackRepository, IInstructorRepository instructorRepository, IInstructor_CourseRepository instructor_CourseRepository, IIntakeRepository intakeRepository)
        {
            this.courseRepository = courseRepository;
            _userManager = userManager;
            this.trackRepository = trackRepository;
            this.instructorRepository = instructorRepository;
            this.instructor_CourseRepository = instructor_CourseRepository;
            this.intakeRepository = intakeRepository;
        }

        // GET: CourseController
        public ActionResult Index(int pageNumber)
        {
            var Tracks = trackRepository.getTracks(); // for filter by track
            ViewData["Tracks"] = new SelectList(Tracks, "ID", "Name"); // Add this line
            var Intakes = intakeRepository.GetIntakes(); // for filter by Intake
            ViewData["Intakes"] = new SelectList(Intakes, "ID", "Name"); 
            var Courses = courseRepository.GetCourses2(pageNumber,10);
            ViewBag.PageNumber = pageNumber;
            ViewBag.TrackID = 0;
            ViewBag.IntakeID = 0;
            ViewBag.intakes_involved = false;
            ViewBag.tracks_involved = false;
            return View(Courses);
        }

        public ActionResult CrsIndexByIntakeId(int Id, int pageNumber)
        {
            var Tracks = trackRepository.GetTracksByIntakeID(Id);
            ViewData["Tracks"] = new SelectList(Tracks, "ID", "Name");

            //var trackIdList = trackIds.Split(',')
            //             .Select(idStr => int.Parse(idStr))
            //             .ToList();

            //ViewBag.SelectedTrackId = trackIdList;

            var Courses = courseRepository.GetCoursesbyIntakeID(Id, pageNumber, 10);
            ViewBag.PageNumber = pageNumber;
            ViewBag.IntakeID = Id;
            ViewBag.TrackID = 0;
            ViewBag.intakes_involved = true;
            ViewBag.tracks_involved = true;

            return View(Courses);

        }


        //to update table for index action
        public ActionResult UpdateTableData2(int IntakeID, int trackID, int pageNumber)
        {

            var tracks = trackRepository.getTracks();
            List<Course> coursesbytrack = new List<Course>();

            if (IntakeID == 0)
            {
                if (trackID == 0)
                {
                    // Get all courses without filtering by track ID
                    coursesbytrack = courseRepository.GetCourses2(pageNumber, 10);
                    if (coursesbytrack.Count == 0 && pageNumber > 1)
                    {
                        coursesbytrack = courseRepository.GetCourses2(pageNumber - 1, 10);
                        pageNumber--;
                    }

                }
                else
                {
                    // Get tracks filtered by track ID
                    coursesbytrack = courseRepository.GetCoursesbyTrackID(trackID, pageNumber, 10);
                    if (coursesbytrack.Count == 0 && pageNumber > 1)
                    {
                        coursesbytrack = courseRepository.GetCoursesbyTrackID(trackID, pageNumber - 1, 10);
                        pageNumber--;
                    }
                }
            }

            if(trackID == 0)
            {
                ViewBag.tracks_involved = false;

            }
            else
            {
                ViewBag.tracks_involved = true;

            }

            if (IntakeID==0 )
            {
                ViewBag.intakes_involved = false;

            }
            else
            {
                ViewBag.intakes_involved = false;

            }

            ViewData["Tracks"] = new SelectList(tracks, "ID", "Name");
            ViewBag.PageNumber = pageNumber;
            ViewBag.TrackID = trackID;
            ViewBag.IntakeID = IntakeID;

            return PartialView("_TableDataPartial", coursesbytrack);
        }


        //to update table for CrsIndexByIntakeId action
        public ActionResult UpdateTableData(int IntakeID,int trackID, int pageNumber)
        {

            var tracks = trackRepository.getTracks();
            List<Intake_Track_Course> coursesbytrack;

            if(IntakeID == 0)
            {
                if (trackID == 0)
                {
                    // Get all tracks without filtering by intake ID
                    coursesbytrack = courseRepository.GetCourses(pageNumber, 10);
                    if (coursesbytrack.Count == 0 && pageNumber > 1)
                    {
                        coursesbytrack = courseRepository.GetCourses(pageNumber - 1, 10);
                        pageNumber--;
                    }

                }
                else
                {
                    // Get tracks filtered by intake ID
                    coursesbytrack = courseRepository.GetCoursesbyTrackIDitc(trackID, pageNumber, 10);
                    if (coursesbytrack.Count == 0 && pageNumber > 1)
                    {
                        coursesbytrack = courseRepository.GetCoursesbyTrackIDitc(trackID, pageNumber - 1, 10);
                        pageNumber--;
                    }
                }
            }
            else
            {
                if (trackID == 0)
                {

                    //  get course by intakeonly
                    


                    coursesbytrack = courseRepository.GetCoursesbyIntakeID(IntakeID,pageNumber, 10);
                    if (coursesbytrack.Count == 0 && pageNumber > 1)
                    {
                        coursesbytrack = courseRepository.GetCoursesbyIntakeID(IntakeID,pageNumber - 1, 10);
                        pageNumber--;
                    }

                }
                else
                {
                    // edit to get courses by both intake and track id

                    // Get tracks filtered by intake ID
                    coursesbytrack = courseRepository.GetCoursesByIntakeTrackID(IntakeID,trackID,pageNumber,10);
                    if (coursesbytrack.Count == 0 && pageNumber > 1)
                    {
                        coursesbytrack = courseRepository.GetCoursesByIntakeTrackID(IntakeID, trackID, pageNumber-1, 10);
                        pageNumber--;
                    }
                }
            }

            ViewData["Tracks"] = new SelectList(tracks, "ID", "Name");
            ViewBag.PageNumber = pageNumber;
            ViewBag.TrackID = trackID;
            ViewBag.IntakeID = IntakeID;

            if (trackID == 0)
            {
                ViewBag.tracks_involved = false;

            }
            else
            {
                ViewBag.tracks_involved = true;

            }

            if (IntakeID == 0)
            {
                ViewBag.intakes_involved = false;

            }
            else
            {
                ViewBag.intakes_involved = false;

            }

            return PartialView("_TableDataPartialFilteredbyIntakes", coursesbytrack);
        }


        // GET: CourseController/DetailsForManager/5
        public ActionResult Details(int id)
        {
            List<string> tracksNames = new List<string>();
            var course = courseRepository.GetCoursebyID(id);
            foreach (var item in course.IntakeTrackCourse)
            {
                tracksNames.Add(item.Track.Name);
            }
            ViewData["tracks"] = tracksNames;
            return View(course);
        }

        // GET: CourseController/Create
        public ActionResult Create()
        {
            var instructors = instructorRepository.GetInstructors();
            ViewBag.AllInstructors = new SelectList(instructors, "AspNetUserID", "AspNetUser.FullName");
            return View();
        }

        // POST: CourseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {

            var instructors = instructorRepository.GetInstructors();
            ViewBag.AllInstructors = new SelectList(instructors, "AspNetUserID", "AspNetUser.FullName");
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                string userId = user.Id;
                course.AdminID = userId;
            }
            if (ModelState.IsValid)
            {
                courseRepository.CreateCourse(course);

                string instructorIdsAsString = Request.Form["selectedInstructorIds"];
                List<string> instructorIds = instructorIdsAsString.Split(',').ToList();

                foreach (var ins_id in instructorIds)
                {
                    Instructor_Course ins = new Instructor_Course
                    {
                        InstructorID = ins_id.ToString(),
                        CourseID = course.ID
                    };
                    instructor_CourseRepository.CreateInstructor_Course(ins);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: CourseController/Edit/5
        public ActionResult Edit(int id)
        {

            var instructors = instructorRepository.GetInstructors();
            var course = courseRepository.GetCoursebyID(id);
            List<Instructor> instructorsSelected = new List<Instructor>();
            foreach (var item in course.InstructorCourses)
            {
                instructorsSelected.Add(item.Instructor);
            }
            ViewBag.SelectedInstructors = instructorsSelected;
            ViewBag.AllInstructors = new SelectList(instructors.Except(instructorsSelected), "AspNetUserID", "AspNetUser.FullName");


            return View(course);
        }

        // POST: CourseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Course course, List<string> SelectedInstructorIds)
        {
            if (ModelState.IsValid)
            {

                foreach (var item in SelectedInstructorIds)
                {
                    Instructor_Course ins = new Instructor_Course()
                    {
                        InstructorID = item,
                        CourseID = id
                    };
                    instructor_CourseRepository.CreateInstructor_Course(ins);

                }

                var Tracks = trackRepository.getTracks(); // for filter by track
                ViewData["Tracks"] = new SelectList(Tracks, "ID", "Name");
                courseRepository.UpdateCourse(id, course);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }
        
        [HttpPost]
        public ActionResult RemoveInstructor_Course(string insID,int crsID)
        {
            instructor_CourseRepository.DeleteInstructor_Course(crsID, insID);
            return RedirectToAction("Edit", new { id = crsID });
        }

        // GET: CourseController/Delete/5
        public ActionResult Delete(List<int> selectedCourseIds, int trackID,  int intakeID,int pageNumber)
        {
            courseRepository.DeleteCourse(selectedCourseIds);

            var tracks = trackRepository.getTracks();
            List<Course> coursesbytrack = new List<Course>();
            List<Intake_Track_Course> intakeTrackcourse = new List<Intake_Track_Course>();

            if (intakeID == 0)
            {
                if (trackID == 0)
                {
                    // Get all courses without filtering by track ID
                    coursesbytrack = courseRepository.GetCourses2(pageNumber, 10);
                    if (coursesbytrack.Count == 0 && pageNumber > 1)
                    {
                        coursesbytrack = courseRepository.GetCourses2(pageNumber - 1, 10);
                        pageNumber--;
                    }

                }
                else
                {
                    // Get tracks filtered by track ID
                    coursesbytrack = courseRepository.GetCoursesbyTrackID(trackID, pageNumber, 10);
                    if (coursesbytrack.Count == 0 && pageNumber > 1)
                    {
                        coursesbytrack = courseRepository.GetCoursesbyTrackID(trackID, pageNumber - 1, 10);
                        pageNumber--;
                    }
                }
            }
            else
            {
                if (trackID == 0)
                {
                    // Get all courses without filtering by track ID
                    intakeTrackcourse = courseRepository.GetCoursesbyIntakeID(intakeID,pageNumber, 10);
                    if (intakeTrackcourse.Count == 0 && pageNumber > 1)
                    {
                        intakeTrackcourse = courseRepository.GetCoursesbyIntakeID(intakeID,pageNumber - 1, 10);
                        pageNumber--;
                    }

                }
                else
                {
                    // Get tracks filtered by track ID
                    intakeTrackcourse = courseRepository.GetCoursesByIntakeTrackID(intakeID, trackID, pageNumber, 10);
                    if (coursesbytrack.Count == 0 && pageNumber > 1)
                    {
                        intakeTrackcourse = courseRepository.GetCoursesByIntakeTrackID(intakeID, trackID, pageNumber - 1, 10);
                        pageNumber--;
                    }
                }
            }

            ViewData["Tracks"] = new SelectList(tracks, "ID", "Name");
            ViewBag.PageNumber = pageNumber;
            ViewBag.TrackID = trackID;
            ViewBag.IntakeID = intakeID;

            if (trackID == 0)
            {
                ViewBag.tracks_involved = false;

            }
            else
            {
                ViewBag.tracks_involved = true;

            }

            if (intakeID == 0)
            {
                ViewBag.intakes_involved = false;

            }
            else
            {
                ViewBag.intakes_involved = true;

            }



           

            if(!ViewBag.intakes_involved)
            {
                return PartialView("_TableDataPartial", coursesbytrack);
            }

            return PartialView("_TableDataPartialFilteredbyIntakes", intakeTrackcourse);

        }


        // POST: CourseController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, Course course)
        //{
        //    courseRepository.DeleteCourse(id);
        //    return RedirectToAction("index");
        //}
    }
}
