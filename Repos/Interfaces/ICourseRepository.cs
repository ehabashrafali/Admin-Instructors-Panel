using Admin_Panel_ITI.Models;

namespace Admin_Panel_ITI.Repos
{
    public interface ICourseRepository
    {
        public int GetCourseNumber();
        public int GetCourseNumberbyIntakeID(int intakeID);
        public Course GetCoursebyID(int courseID);
        public List<Intake_Track_Course> GetCourses(int pageNumber, int pageSize);
        public List<Intake_Track_Course> GetCoursesbyTrackIDitc(int trackID, int pageNumber, int pageSize);
        public List<Course> GetCoursesbyTrackID(int trackID,int pageNumber, int pageSize);
        public List<Course> GetCourses();
        public List<Course> GetCourses2(int pageNumber, int pageSize);
        public void UpdateCourse(int CourseID, Course course);
        public void DeleteCourse(int courseID);
        public void CreateCourse(Course course);
        public void DeleteCourse(List<int> courseids);


        /*---------------------------------------------- Instructor Repos -----------------------------------------------*/
        public List<Intake_Track_Course> GetCoursesByIntakeTrackID(int intakeID, int trackID, int pageNumber, int pageSize);
        public List<Course> GetCoursesByIntakeTrackID(int intakeID, int trackID);
        public List<Course> GetTeacherCourses(int intakeID, int trackID, string instructorID);
        public List<Intake_Track_Course> GetCoursesbyIntakeID(int intakeid, int pageNumber, int pageSize);
        public string GetCourseName(int courseID); 
    }
}
