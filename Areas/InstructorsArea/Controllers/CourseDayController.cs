using Admin_Panel_ITI.Areas.InstructorsArea.ViewModels;
using Admin_Panel_ITI.Models;
using Admin_Panel_ITI.Repos;
using Admin_Panel_ITI.Repos.Interfaces;
using Admin_Panel_ITI.Repos.RepoServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Admin_Panel_ITI.Areas.InstructorsArea.Controllers
{
    [Area("InstructorsArea")] //have to be added(mandatory)
    public class CourseDayController : Controller
    {
        private readonly ICourseDayRepository courseDayRepo;
        private readonly IWebHostEnvironment webHostingEnvironment;
        private readonly UserManager<AppUser> userManager;
        private readonly IMaterialRepository materialRepo;
        private readonly ICourse_Day_MaterialRepository courseDayMaterialRepo;
        private readonly IIntakeRepository intakeRepo;
        private readonly ITrackRepository trackRepo;
        private readonly ICourseRepository courseRepo;

        public CourseDayController(ICourseDayRepository _courseDayRepo,
            IWebHostEnvironment _hostingEnvironment,
            UserManager<AppUser> _userManager,
            IMaterialRepository _materialRepo,
            ICourse_Day_MaterialRepository _courseDayMaterialRepo, 
            IIntakeRepository _intakeRepo,
            ITrackRepository _trackRepo, 
            ICourseRepository _courseRepo)
        {
            courseDayRepo = _courseDayRepo;
            webHostingEnvironment = _hostingEnvironment;
            userManager = _userManager;
            materialRepo = _materialRepo;
            courseDayMaterialRepo = _courseDayMaterialRepo;
            intakeRepo = _intakeRepo;
            trackRepo = _trackRepo;
            courseRepo = _courseRepo;
        }




        // id = course id
        [Route("CDI/{id?}/{intakeID?}/{trackID?}")]
        public ActionResult Index(int id, int intakeID, int trackID)
        {
            ViewBag.Id = id;
            ViewBag.Name = courseRepo.GetCourseName(id);
            ViewBag.IntakeName = intakeRepo.getIntakeName(intakeID);
            ViewBag.TrackName = trackRepo.getTrackName(trackID);
            ViewBag.IntakeID = intakeID;
            ViewBag.TrackID = trackID;
            ViewBag.CourseDaysCount = courseDayRepo.GetCourseDaysCount(id);


            return View(courseDayRepo.GetCourseDaysByCourseID(id));
        }






        //id(course id)
        [HttpGet]
        [Route("DDDG/{id?}/{intakeID?}/{trackID?}/{coursedayID?}")]
        public ActionResult Details(int id, int intakeID, int trackID, int coursedayID)
        {
            ViewBag.Id = id;
            ViewBag.IntakeID = intakeID;
            ViewBag.TrackID = trackID;

            ViewBag.CourseDayId = coursedayID;
            ViewBag.CourseDayNum = courseDayRepo.GetCourseDaybyID(coursedayID).DayNumber;

            ViewBag.Name = courseRepo.GetCourseName(id);
            ViewBag.IntakeName = intakeRepo.getIntakeName(intakeID);
            ViewBag.TrackName = trackRepo.getTrackName(trackID);

            ViewBag.Materials = new List<IFormFile>();
            ViewBag.Tasks = new List<IFormFile>();

            var courseDay = courseDayRepo.GetCourseDaybyID(coursedayID);
            var taskPath = courseDay.TaskPath;

            // Use Path.GetFileName to get the file name with extension
            string[] fileNameWithExtension = (Path.GetFileName(taskPath)).Split('.');

            ViewBag.taskName = fileNameWithExtension[0];
            ViewBag.taskExtension = fileNameWithExtension[1];

            return View(courseDayMaterialRepo.GetCourseDaysbyCourseDayID(coursedayID));
        }


        //[HttpPost]
        //public ActionResult Details(List<IFormFile> Materials, IFormFile Task, int id, string name, int intakeID, int trackID, string intakeName, string trackName, int coursedayID, int coursedayNum)
        //{
        //    if (true)
        //    {
        //        string? instructorID = userManager.GetUserId(User);
        //        string? uniqueFileName = null; //varaible to store the materials/Task name after make it uniqe using GUID

        //        if (Materials?.Count != 0)
        //        {
        //            List<Material> materials = new();

        //            string MaterialsFilePath = Path.Combine(webHostingEnvironment.WebRootPath, "Materials"); //where the materials gonna be store(~/wwwroot/Materials/)

        //            foreach (var material in Materials)
        //            {
        //                uniqueFileName = Guid.NewGuid().ToString() + "_" + material.FileName; //give each material a uniqu name to prevent override Files
        //                string MaterialPath = Path.Combine(MaterialsFilePath, uniqueFileName); //store the selected materials in "Materials" file(make string path: ~/wwwroot/Materials/filename.txt)

        //                material.CopyTo(new FileStream(MaterialPath, FileMode.Create));


        //                materials.Add(new Material() { Name = material.FileName.Split('.')[0], Path = MaterialPath, Type = Path.GetExtension(material.FileName), InstructorID = instructorID });
        //            }

        //            materialRepo.CreateMaterials(materials);


        //            List<Course_Day_Material> cdms = new();

        //            foreach (var material in materials)
        //            {
        //                cdms.Add(new Course_Day_Material() { CourseID = id, CourseDayID = coursedayID, MaterialID = material.ID });
        //            }

        //            courseDayMaterialRepo.CreateCourseDayMaterial(cdms);

        //            return RedirectToAction(nameof(Details), new { id, name, intakeID, trackID, intakeName, trackName, coursedayID, coursedayNum });
        //        }

        //        if(Task != null)
        //        {
        //            string TaskFilePath = Path.Combine(webHostingEnvironment.WebRootPath, "Tasks"); //where the Tasks gonna be store(~/wwwroot/Tasks/)

        //            //uniqueFileName = Guid.NewGuid().ToString() + "||" + Task.FileName;
        //            string TaskPath = Path.Combine(TaskFilePath, Task.FileName);

        //            Task.CopyTo(new FileStream(TaskPath, FileMode.Create));

        //            CourseDay oldCourseDay =  courseDayRepo.GetCourseDaybyID(coursedayID);
        //            if(oldCourseDay != null)
        //            {
        //                CourseDay NewCourseDay = new CourseDay()
        //                {
        //                    DayNumber = oldCourseDay.DayNumber,
        //                    Date = oldCourseDay.Date,
        //                    DeadLine = oldCourseDay.DeadLine,
        //                    TaskPath = TaskPath  //update the task path
        //                };

        //                //FileInfo fileInfo = new FileInfo(oldCourseDay.TaskPath);
        //                //if (fileInfo.Exists)
        //                //{
        //                //    fileInfo.Replace(TaskPath, oldCourseDay.TaskPath);
        //                //}

        //                courseDayRepo.UpdateCourseDay(coursedayID, NewCourseDay);

        //                return RedirectToAction(nameof(Details), new { id, name, intakeID, trackID, coursedayID});
        //            }
        //        }
        //    }

        //    return View(new {id,  intakeID, trackID,  coursedayID});
        //}



        [HttpPost]
        [Route("DDDP/{id?}/{intakeID?}/{trackID?}/{coursedayID?}")]
        public ActionResult Detailss(List<IFormFile> Materials, IFormFile Task, int id, int intakeID, int trackID, int coursedayID)
        {

            string? instructorID = userManager.GetUserId(User);
            string? uniqueFileName = null; //varaible to store the materials/Task name after make it uniqe using GUID

            if (Materials?.Count != 0)
            {
                List<Material> materials = new();

                string MaterialsFilePath = Path.Combine(webHostingEnvironment.WebRootPath, "Materials"); //where the materials gonna be store(~/wwwroot/Materials/)

                foreach (var material in Materials)
                {
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + material.FileName; //give each material a uniqu name to prevent override Files
                    string MaterialPath = Path.Combine(MaterialsFilePath, uniqueFileName); //store the selected materials in "Materials" file(make string path: ~/wwwroot/Materials/filename.txt)

                    material.CopyTo(new FileStream(MaterialPath, FileMode.Create));


                    materials.Add(new Material() { Name = material.FileName.Split('.')[0], Path = MaterialPath, Type = Path.GetExtension(material.FileName), InstructorID = instructorID });
                }

                materialRepo.CreateMaterials(materials);


                List<Course_Day_Material> cdms = new();

                foreach (var material in materials)
                {
                    cdms.Add(new Course_Day_Material() { CourseID = id, CourseDayID = coursedayID, MaterialID = material.ID });
                }

                courseDayMaterialRepo.CreateCourseDayMaterial(cdms);

                return RedirectToAction(nameof(Details), new { id, intakeID, trackID, coursedayID });
            }

            if (Task != null)
            {
                string TaskFilePath = Path.Combine(webHostingEnvironment.WebRootPath, "Tasks"); //where the Tasks gonna be store(~/wwwroot/Tasks/)

                //uniqueFileName = Guid.NewGuid().ToString() + "||" + Task.FileName;
                string TaskPath = Path.Combine(TaskFilePath, Task.FileName);

                Task.CopyTo(new FileStream(TaskPath, FileMode.Create));

                CourseDay oldCourseDay = courseDayRepo.GetCourseDaybyID(coursedayID);
                if (oldCourseDay != null)
                {
                    CourseDay NewCourseDay = new CourseDay()
                    {
                        DayNumber = oldCourseDay.DayNumber,
                        Date = oldCourseDay.Date,
                        DeadLine = oldCourseDay.DeadLine,
                        TaskPath = TaskPath  //update the task path
                    };

                    //FileInfo fileInfo = new FileInfo(oldCourseDay.TaskPath);
                    //if (fileInfo.Exists)
                    //{
                    //    fileInfo.Replace(TaskPath, oldCourseDay.TaskPath);
                    //}

                    courseDayRepo.UpdateCourseDay(coursedayID, NewCourseDay);

                    return RedirectToAction(nameof(Details), new { id, intakeID, trackID, coursedayID});
                }
            }
           
            return View(new { id, intakeID, trackID, coursedayID });
        }



        [Route("CreateG/{id?}/{intakeID?}/{trackID?}/{CourseDaysCount?}")]
        public ActionResult Create(int id, int intakeID, int trackID, int CourseDaysCount)
        {
            ViewBag.Id = id;
            ViewBag.IntakeID = intakeID;
            ViewBag.TrackID = trackID;
            ViewBag.CourseDaysCount = CourseDaysCount;

            ViewBag.Name = courseRepo.GetCourseName(id);
            ViewBag.IntakeName = intakeRepo.getIntakeName(intakeID);
            ViewBag.TrackName = trackRepo.getTrackName(trackID);

            return View();
        }

        [HttpPost]
        //[Route("CreateP/{model?}/{id?}/{intakeID?}/{trackID?}")]
        public ActionResult Createe(CourseDay_Material_TaskVM model, int id, int intakeID, int trackID)
        {
            if (ModelState.IsValid)
            {
                string? uniqueFileName = null; //varaible to store the materials name after make it uniqe using GUID.
                string? instructorID = userManager.GetUserId(User);

                List<Material> materials = new();

                if (model.Materials != null)
                {
                    string MaterialsFilePath = Path.Combine(webHostingEnvironment.WebRootPath, "Materials"); //where the materials gonna be store(~/wwwroot/Materials/)

                    foreach (var material in model.Materials)
                    {
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + material.FileName; //give each material a uniqu name to prevent override
                        string MaterialPath = Path.Combine(MaterialsFilePath, uniqueFileName); //store the selected materials in "Materials" file(make string path: ~/wwwroot/Materials/filename.txt)

                        material.CopyTo(new FileStream(MaterialPath, FileMode.Create));


                        materials.Add(new Material() { Name = material.FileName.Split('.')[0], Path = MaterialPath, Type = Path.GetExtension(material.FileName), InstructorID = instructorID });
                    }
                }

                materialRepo.CreateMaterials(materials);


                if (model.Task != null)
                {
                    string TaskFilePath = Path.Combine(webHostingEnvironment.WebRootPath, "Tasks");
                    //uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Task.FileName;
                    string TaskPath = Path.Combine(TaskFilePath, model.Task.FileName);

                    model.Task.CopyTo(new FileStream(TaskPath, FileMode.Create));

                    CourseDay newCourseDay = new CourseDay()
                    {
                        Date = DateTime.Now,
                        DayNumber = model.DayNumber,
                        TaskPath = TaskPath,
                        DeadLine = model.DeadLine
                    };

                    courseDayRepo.CreateCourseDay(newCourseDay);


                    List<Course_Day_Material> cdms = new();
                    foreach (var material in materials)
                    {
                        cdms.Add(new Course_Day_Material() { CourseID = id, CourseDayID = newCourseDay.ID, MaterialID = material.ID });
                    }

                    courseDayMaterialRepo.CreateCourseDayMaterial(cdms);
                }

                return RedirectToAction("Index", new { id, intakeID, trackID});
            }

            int CourseDaysCount = model.DayNumber;
            return RedirectToAction(nameof(Create), new { id, intakeID, trackID, CourseDaysCount }); 
        }





        public ActionResult Edit(int id)
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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




        
        //[Route("GetD/{id?}")]
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deletee(int MaterialID, int id, int intakeID, int trackID, int coursedayID)
        {
            if (ModelState.IsValid)
            {
                courseDayMaterialRepo.DeleteCourseDayMaterial(MaterialID);
                materialRepo.DeleteMaterial(MaterialID);

                return RedirectToAction(nameof(Details), new { id, intakeID, trackID, coursedayID });
            }

            return View();
        }
    }
}