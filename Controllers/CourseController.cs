using Admin_Panel_ITI.Models;
using Admin_Panel_ITI.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace Admin_Panel_ITI.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository courseRepository;
        private readonly UserManager<AppUser> _userManager;

        public CourseController(ICourseRepository courseRepository, UserManager<AppUser> userManager)
        {
            this.courseRepository = courseRepository;
            _userManager = userManager;
        }
        // GET: CourseController
        public ActionResult Index(int pageNumber)
        {
            var courses = courseRepository.GetCourses(pageNumber, 10);
            return View(courses);
        }


        // GET: CourseController/Details/5
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
            return View();
        }

        // POST: CourseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {

            // Requires Editing
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                string userId = user.Id;
                course.AdminID = "admin1";
            }
            courseRepository.CreateCourse(course);
            return RedirectToAction("index");
        }

        // GET: CourseController/Edit/5
        public ActionResult Edit(int id)
        {

            var course = courseRepository.GetCoursebyID(id);
            ViewBag.AdminID = course.AdminID;
            return View(course);
        }

        // POST: CourseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Course course)
        {
            if (ModelState.IsValid)
            {
                courseRepository.UpdateCourse(id, course);
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: CourseController/Delete/5
        public ActionResult Delete(int id)
        {
            var course = courseRepository.GetCoursebyID(id);
            return View(course);
        }

        // POST: CourseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Course course)
        {
            courseRepository.DeleteCourse(id);
            return RedirectToAction("index");
        }
    }
}
