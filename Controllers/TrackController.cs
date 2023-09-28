using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using Microsoft.AspNetCore.Mvc.Rendering;
using Admin_Panel_ITI.Repos;
using Admin_Panel_ITI.Models;
using NuGet.DependencyResolver;
using Microsoft.AspNetCore.Identity;
using Admin_Panel_ITI.Repos.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.CodeAnalysis.Options;
using System.Diagnostics;

namespace Admin_Panel_ITI.Controllers
{
    public class TrackController : Controller
    {
        private readonly ITrackRepository trackRepositry;
        public IIntakeRepository intakeRepository { get; }
        private readonly UserManager<AppUser> _userManager;
        private readonly IInstructorRepository instructorRepository;
        private readonly IStudentRepository studentRepository;

        public TrackController(ITrackRepository trackRepositry, IIntakeRepository intakeRepository, UserManager<AppUser> userManager, IInstructorRepository instructorRepository, IStudentRepository studentRepository)
        {
            this.trackRepositry = trackRepositry;
            this.intakeRepository = intakeRepository;
            _userManager = userManager;
            this.instructorRepository = instructorRepository;
            this.studentRepository = studentRepository;
        }
        // GET: TrackController
        public ActionResult Index(int pageNumber)
        {
            var intakes = intakeRepository.GetAllIntakes();
            var Tracks = trackRepositry.getTracks(pageNumber, 10);
            List<int> studentNumsforTrack = new List<int>();
            foreach (var track in Tracks)
            {
                var studentNuminTrack = studentRepository.getStudentNumberbyTrackID(track.ID);
                studentNumsforTrack.Add(studentNuminTrack);
            }
            ViewData["NumOfStudsInEachTrack"] = studentNumsforTrack;
            ViewData["Intakes"] = new SelectList(intakes, "ID", "Name"); // Add this line
            ViewBag.PageNumber = pageNumber;
            ViewBag.IntakeID = 0;
            return View(Tracks);
        }

        public ActionResult TracksByInakeId(int id, int pageNumber)
        {
            var intakes = intakeRepository.GetAllIntakes();
            var tracks = trackRepositry.GetTracksByIntakeId(id, pageNumber, 10);
            List<int> studentNumsforTrack = new List<int>();
            foreach (var track in tracks)
            {
                var studentNuminTrack = studentRepository.getStudentNumberbyTrackID(track.ID);
                studentNumsforTrack.Add(studentNuminTrack);
            }
            ViewData["NumOfStudsInEachTrack"] = studentNumsforTrack;
            ViewData["Intakes"] = new SelectList(intakes, "ID", "Name");
            ViewBag.PageNumber = pageNumber;
            ViewBag.IntakeID = id;
            return View(tracks);
        }


        public ActionResult UpdateTableData(int intakeID, int pageNumber)
        {

            var intakes = intakeRepository.GetAllIntakes();
            List<Track> tracksByIntake;

            if (intakeID == 0)
            {
                // Get all tracks without filtering by intake ID
                tracksByIntake = trackRepositry.getTracks(pageNumber, 10);
                if(tracksByIntake.Count == 0 && pageNumber > 1)
                {
                    tracksByIntake = trackRepositry.getTracks(pageNumber - 1, 10);
                    pageNumber--;
                }
              
            }
            else
            {
                // Get tracks filtered by intake ID
                 tracksByIntake = trackRepositry.getTrackbyIntakeID(intakeID, pageNumber, 10).Select(t => t.Track).Distinct().ToList();
                if (tracksByIntake.Count == 0 && pageNumber > 1)
                {
                    tracksByIntake = trackRepositry.getTrackbyIntakeID(intakeID, pageNumber -1, 10).Select(t => t.Track).Distinct().ToList();
                    pageNumber--;
                }
            }

            List<int> studentNumsforTrack = new List<int>();
            foreach (var track in tracksByIntake)
            {
                var studentNuminTrack = studentRepository.getStudentNumberbyTrackID(track.ID);
                studentNumsforTrack.Add(studentNuminTrack);
            }

            ViewData["NumOfStudsInEachTrack"] = studentNumsforTrack;
            ViewData["Intakes"] = new SelectList(intakes, "ID", "Name");
            ViewBag.PageNumber = pageNumber;
            ViewBag.IntakeID = intakeID;
            return PartialView("_TableDataPartial", tracksByIntake);
        }


        // GET: TrackController/DetailsForManager/5
        public ActionResult Details(int id)
        {
            return View(trackRepositry.getTrackbyID(id));
        }


        // GET: TrackController/Create
        public ActionResult Create()
        {

            var instructors = instructorRepository.GetInstructors();
            ViewBag.AllInstructors = new SelectList(instructors, "AspNetUserID", "AspNetUser.FullName");
            return View();
        }


        

        //POST: TrackController/Create
        [HttpPost]
       [ValidateAntiForgeryToken]
       async public Task<ActionResult> Create(Track track)
        {
            var instructors = instructorRepository.GetInstructors();
            ViewBag.AllInstructors = new SelectList(instructors, "AspNetUserID", "AspNetUser.FullName");
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                string userId = user.Id;
                track.AdminID = "admin1";
            }
            if (ModelState.IsValid)
            {
                trackRepositry.CreateTrack(track);
                return RedirectToAction(nameof(Index));
            }
            return View(track);
        }

        // GET: TrackController/Edit/5
        public ActionResult Edit(int id)
        {
            var instructors = instructorRepository.GetInstructors();
            ViewBag.AllInstructors = new SelectList(instructors, "AspNetUserID", "AspNetUser.FullName");
            var track = trackRepositry.getTrackbyID(id);
            return View(track);
        }

        // POST: TrackController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Track track)
        {
            if (ModelState.IsValid)
            {

                var Intake = intakeRepository.GetIntakes();
                ViewBag.AllIntakes = new SelectList(Intake, "AspNetUserID", "AspNetUser.FullName");
                trackRepositry.UpdateTrack(id, track);
                return RedirectToAction(nameof(Index));
            }
            return View(track);
        }

        // GET: TrackController/Delete/5
        [HttpGet]
        public ActionResult Delete(List<int> selectedTrackIds, int intakeID, int pageNumber)
        {
            trackRepositry.DeleteTrack(selectedTrackIds);

            var intakes = intakeRepository.GetAllIntakes();
            List<Track> tracksByIntake;

            if (intakeID == 0)
            {
                // Get all tracks without filtering by intake ID
                tracksByIntake = trackRepositry.getTracks(pageNumber, 10);
                if (tracksByIntake.Count == 0 && pageNumber > 1)
                {
                    tracksByIntake = trackRepositry.getTracks(pageNumber - 1, 10);
                    pageNumber--;
                }

            }
            else
            {
                // Get tracks filtered by intake ID
                tracksByIntake = trackRepositry.getTrackbyIntakeID(intakeID, pageNumber, 10).Select(t => t.Track).Distinct().ToList();
                if (tracksByIntake.Count == 0 && pageNumber > 1)
                {
                    tracksByIntake = trackRepositry.getTrackbyIntakeID(intakeID, pageNumber - 1, 10).Select(t => t.Track).Distinct().ToList();
                    pageNumber--;
                }
            }

            List<int> studentNumsforTrack = new List<int>();
            foreach (var track in tracksByIntake)
            {
                var studentNuminTrack = studentRepository.getStudentNumberbyTrackID(track.ID);
                studentNumsforTrack.Add(studentNuminTrack);
            }

            ViewData["NumOfStudsInEachTrack"] = studentNumsforTrack;
            ViewData["Intakes"] = new SelectList(intakes, "ID", "Name");
            ViewBag.PageNumber = pageNumber;
            ViewBag.IntakeID = intakeID;
            return PartialView("_TableDataPartial", tracksByIntake);

        }


        public IActionResult GetTracksForIntake(int intakeID)
        {
            // Retrieve tracks for the selected intake based on intakeID
            // Generate HTML <option> elements for the select list

            // Example code (replace with your actual logic):
            //var tracks = trackRepositry.GetTracksByIntakeID(intakeID);

            // Generate HTML options with asp-for attribute
            //var options = tracks.Select(track =>
            //    $"<option asp-for='Input.TrackID' value='{track.ID}'>{track.Name}</option>");

            // Combine the options into a string
            //var selectListHtml = string.Join("", options);

            //var selectList = new SelectList(tracks, "ID", "Name");
            //ViewBag.tracks = selectList;
            //var finalHTML = "<select asp-for ='Input.TrackID' id='studentFields2' asp-items='@ViewBag.tracks' class='form-select'> < option > Select a Track</ option ></ select > ";
            //return Content(finalHTML);


            var tracks = trackRepositry.GetTracksByIntakeID(intakeID);
            var selectList = new SelectList(tracks, "ID", "Name");
            var trackList = selectList.Select(item => new SelectListItem
            {
                Value = item.Value,
                Text = item.Text
            });

            return Json(trackList);



        }
    }


    //// POST: TrackController/Delete/5
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult Delete(int id, IFormCollection collection)
    //{
    //    try
    //    {
    //        return RedirectToAction(nameof(Index));
    //    }
    //    catch
    //    {
    //        return View();
    //    }
    //}
}
