using Admin_Panel_ITI.Data;
using Admin_Panel_ITI.Models;
using Admin_Panel_ITI.Repos.Interfaces;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing.Printing;
using System.Web.Mvc;

namespace Admin_Panel_ITI.Repos
{
    public class CourseRepoServices : ICourseRepository
    {
        private readonly IIntake_Track_CourseRepository intake_track_CourseRepository;
        private readonly IInstructor_CourseRepository instructor_CourseRepository;
        private readonly IStudent_CourseRepository student_CourseRepository;
        private readonly ICourse_Day_MaterialRepository course_Day_MaterialRepository;
        //private readonly ICourseDayRepository courseDayRepository;
        //private readonly IMaterialRepository materialRepository;
        private MainDBContext Context { get; set; }

        public CourseRepoServices(MainDBContext context,
            IIntake_Track_CourseRepository Intake_track_CourseRepository, 
            IInstructor_CourseRepository instructor_CourseRepository, 
            IStudent_CourseRepository student_CourseRepository,
            ICourse_Day_MaterialRepository course_Day_MaterialRepository, 
            ICourseDayRepository courseDayRepository, IMaterialRepository materialRepository)
        {
            Context = context;
            this.intake_track_CourseRepository = Intake_track_CourseRepository;
            this.instructor_CourseRepository = instructor_CourseRepository;
            this.student_CourseRepository = student_CourseRepository;
            this.course_Day_MaterialRepository = course_Day_MaterialRepository;
            //this.courseDayRepository = courseDayRepository;
            //this.materialRepository = materialRepository;
        }

        void ICourseRepository.CreateCourse(Course course)
        {
            Context.Courses.Add(course);
            Context.SaveChanges();
        }

        void ICourseRepository.DeleteCourse(int courseID)
        {

            // Delete track course records
            intake_track_CourseRepository.DeleteIntake_Track_CoursebyCourseID(courseID);

            // Delete Instructor Course records
            instructor_CourseRepository.DeleteInstructor_CourseByCourseID(courseID);

            // Delete Student Course records
            student_CourseRepository.DeleteStudent_CourseByCourseID(courseID);


            // Delete Course_Day_Materials, all CourseDays, all Materials
            course_Day_MaterialRepository.DeleteAllRelatedCourseDays_Materials(courseID);



            var course = Context.Courses.FirstOrDefault(c => c.ID == courseID);
            Context.Courses.Remove(course);
            Context.SaveChanges();
        }

        void ICourseRepository.DeleteCourse(List<int> courseIDs)
        {
            foreach (var id in courseIDs)
            {
                // Delete track course records
                intake_track_CourseRepository.DeleteIntake_Track_CoursebyCourseID(id);

                // Delete Instructor Course records
                instructor_CourseRepository.DeleteInstructor_CourseByCourseID(id);

                // Delete Student Course records
                student_CourseRepository.DeleteStudent_CourseByCourseID(id);


                // Delete Course_Day_Materials, all CourseDays, all Materials
                course_Day_MaterialRepository.DeleteAllRelatedCourseDays_Materials(id);



                var course = Context.Courses.FirstOrDefault(c => c.ID == id);
                Context.Courses.Remove(course);
            }

            
            Context.SaveChanges();
        }

        Course ICourseRepository.GetCoursebyID(int courseID)
        {
            var course = Context.Courses
                .Include(c => c.Admin)
                .Include(c => c.InstructorCourses)
                .ThenInclude(cc=>cc.Instructor)
                .Include(c => c.IntakeTrackCourse)
                .ThenInclude(itc => itc.Track)
                .FirstOrDefault(c => c.ID == courseID);

            if (course != null)
            {
                var uniqueTracks = course.IntakeTrackCourse
                    .Select(itc => itc.Track)
                    .Distinct()
                    .ToList();

                course.IntakeTrackCourse = uniqueTracks
                    .Select(track => new Intake_Track_Course
                    {
                        Track = track,
                        Course = course
                    })
                    .ToList();
            }

            return course;
        }
        

        // get all courses even assigned to track&intake or not
        int ICourseRepository.GetCourseNumber()
        {

            int count =
              (from course in Context.Courses
               join intakeTrackCourse in Context.Intake_Track_Courses
               on course.ID equals intakeTrackCourse.CourseID into joined
               from rightJoin in joined.DefaultIfEmpty()
               select course)
              .Distinct()
              .Count();

            return count;
        }
        List<Course> ICourseRepository.GetCourses2(int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            //var query2 = (from course in Context.Courses
            //              join intakeTrackCourse in Context.Intake_Track_Courses
            //              on course.ID equals intakeTrackCourse.CourseID into joined
            //              from rightJoin in joined.DefaultIfEmpty()
            //              select course)
            //             .Include(c => c.IntakeTrackCourse)
            //             .Skip((pageNumber - 1) * pageSize)
            //             .Take(pageSize)
            //             .ToList();

            var query2 = Context.Courses
                                 .Skip((pageNumber - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToList();



            return query2;
        }
        int ICourseRepository.GetCourseNumberbyIntakeID(int intakeID)
        {
            return Context.Intake_Track_Courses.Where(itc=>itc.IntakeID == intakeID).Count();
        }

        List<Intake_Track_Course> ICourseRepository.GetCourses(int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            var query2 = Context.Intake_Track_Courses
                 .Include(t => t.Course)
                 .Include(tc => tc.Track)
                 .Skip((pageNumber - 1) * pageSize)
                 .Take(pageSize)
                 .ToList();

            return query2;
        }

       
        List<Course> ICourseRepository.GetCourses()
        {
            
            var courses = Context.Courses
                .Include(c => c.Admin)
                .Include(c => c.InstructorCourses)
                .Include(c => c.IntakeTrackCourse)
                .ToList();

            return courses;
        }

        void ICourseRepository.UpdateCourse(int CourseID, Course course)
        {
            var courseUpdated = Context.Courses.FirstOrDefault(c => c.ID == CourseID);
            courseUpdated.Name = course.Name;
            courseUpdated.Duration = course.Duration;
            courseUpdated.CreationDate = course.CreationDate;
            Context.SaveChanges();
        }

        public List<Intake_Track_Course> GetCoursesByIntakeTrackID(int intakeID, int trackID, int pageNumber, int pageSize)
        {

            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            var query =
                (from tc in Context.Intake_Track_Courses
                 join c in Context.Courses on tc.CourseID equals c.ID
                 where tc.TrackID == trackID && tc.IntakeID == intakeID
                 select tc)
                 .Include(tt => tt.Intake)
                 .Include(tt=>tt.Track)
                 .Include(tt2=>tt2.Course)
                 .Skip((pageNumber - 1) * pageSize)
                 .Take(pageSize)
                 .ToList();

            return query;
        }

        public List<Course> GetCoursesByIntakeTrackID(int intakeID, int trackID)
        {


            var query =
                (from c in Context.Courses
                 join tc in Context.Intake_Track_Courses on c.ID equals tc.CourseID
                 where tc.TrackID == trackID && tc.IntakeID == intakeID
                 select c)
                 .Include(c => c.Admin)
                 .ThenInclude(a => a.AspNetUser)

                 .ToList();

            return query;
        }

        List<Course> ICourseRepository.GetCoursesbyTrackID(int trackID, int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            var query2 = (from c in Context.Courses
                          join tc in Context.Intake_Track_Courses on c.ID equals tc.CourseID
                          where tc.TrackID == trackID
                          select c)
                         .Include(c => c.IntakeTrackCourse).Distinct()
                 .Skip((pageNumber - 1) * pageSize)
                 .Take(pageSize)
                 .ToList();

            return query2;

        }

        List<Intake_Track_Course> ICourseRepository.GetCoursesbyTrackIDitc(int trackID, int pageNumber, int pageSize)
        {
            var query2 = Context.Intake_Track_Courses
                 .Where(itc => itc.TrackID == trackID)
                 .Include(t => t.Course)
                 .Include(i => i.Intake)
                 .Include(tc => tc.Track)
                 .Skip((pageNumber - 1) * pageSize)
                 .Take(pageSize)
                 .ToList();

            return query2;

        }

        List<Intake_Track_Course> ICourseRepository.GetCoursesbyIntakeID(int intakeid, int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }



            var query2 = Context.Intake_Track_Courses
                .Where(itc => itc.IntakeID == intakeid)
                .Include(t=> t.Course)
                .Include (tc=> tc.Track)
                .Include(i => i.Intake)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return query2;
        }

        public List<Course> GetTeacherCourses(int intakeID, int trackID, string instructorID)
        {
            var query =
                (from ic in Context.Instructor_Courses
                 where ic.InstructorID == instructorID
                 join itc in Context.Intake_Track_Courses on ic.CourseID equals itc.CourseID
                 where itc.IntakeID == intakeID && itc.TrackID == trackID
                 select ic.Course).ToList();

            return query;
        }

        public string GetCourseName(int courseID)
        {
            return Context.Courses.FirstOrDefault(c => c.ID == courseID).Name;
        }
    }
}
