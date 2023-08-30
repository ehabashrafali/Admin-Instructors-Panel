using Admin_Panel_ITI.Data;
using Admin_Panel_ITI.Models;
using Admin_Panel_ITI.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Admin_Panel_ITI.Repos
{
    public class CourseRepoServices : ICourseRepository
    {
        private readonly ITrack_CourseRepository track_CourseRepository;
        private readonly IInstructor_CourseRepository instructor_CourseRepository;
        private readonly IStudent_CourseRepository student_CourseRepository;
        private readonly ICourse_Day_MaterialRepository course_Day_MaterialRepository;
        private readonly ICourseDayRepository courseDayRepository;
        private readonly IMaterialRepository materialRepository;

        public MainDBContext Context { get; set; }

        public CourseRepoServices(MainDBContext context, ITrack_CourseRepository track_CourseRepository, IInstructor_CourseRepository instructor_CourseRepository, IStudent_CourseRepository student_CourseRepository, ICourse_Day_MaterialRepository course_Day_MaterialRepository, ICourseDayRepository courseDayRepository, IMaterialRepository materialRepository)
        {
            Context = context;
            this.track_CourseRepository = track_CourseRepository;
            this.instructor_CourseRepository = instructor_CourseRepository;
            this.student_CourseRepository = student_CourseRepository;
            this.course_Day_MaterialRepository = course_Day_MaterialRepository;
            this.courseDayRepository = courseDayRepository;
            this.materialRepository = materialRepository;
        }

        void ICourseRepository.CreateCourse(Course course)
        {
            Context.Courses.Add(course);
            Context.SaveChanges();
        }

        void ICourseRepository.DeleteCourse(int courseID)
        {

            // Delete track course records
            track_CourseRepository.DeleteTrack_CourseByCourseID(courseID);

            // Delete Instructor Course records
            instructor_CourseRepository.DeleteInstructor_CourseByCourseID(courseID);

            // Delete Student Course records
            student_CourseRepository.DeleteStudent_CourseByCourseID(courseID);


            // Delete Course_Day_Materials, all CourseDays, all Materials
            course_Day_MaterialRepository.DeleteAllRelatedCourseDays_Materials(courseID);



            var course = Context.Courses.FirstOrDefault(c => c.ID == courseID);
            Context.Courses.Remove(course);
        }

        Course ICourseRepository.GetCoursebyID(int courseID)
        {
            var course = Context.Courses.Include(c=>c.Admin).FirstOrDefault(c => c.ID == courseID);
            return course;
        }

        int ICourseRepository.GetCourseNumber()
        {
            return Context.Courses.Count();
        }    
        
        int ICourseRepository.GetCourseNumberbyIntakeID(int intakeID)
        {
            return Context.Intake_Track_Courses.Where(itc=>itc.IntakeID == intakeID).Count();
        }

        List<Course> ICourseRepository.GetCourses()
        {
            return Context.Courses.Include(c=>c.Admin)
                                  .Include(c=>c.InstructorCourses)
                                  .Include(c=>c.TrackCourses)
                                  .ToList();
        }

        void ICourseRepository.UpdateCourse(int CourseID, Course course)
        {
            var courseUpdated = Context.Courses.FirstOrDefault(c => c.ID == CourseID);
            courseUpdated.Name = course.Name;
            courseUpdated.Duration = course.Duration;
            courseUpdated.AdminID = course.AdminID;
            courseUpdated.CreationDate = course.CreationDate;
            Context.SaveChanges();
        }
    }
}
