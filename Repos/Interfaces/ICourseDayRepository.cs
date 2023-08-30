using Admin_Panel_ITI.Models;

namespace Admin_Panel_ITI.Repos.Interfaces
{
    public interface ICourseDayRepository
    {


        public int GetCourseDaysNumber();



        public CourseDay GetCourseDaybyID(int courseDayID);



        public List<CourseDay> GetCourseDays();


        public void UpdateCourseDay(int courseDayID, CourseDay courseDay);


        public void DeleteCourseDay(int courseDayID);
        public void CreateCourseDay(CourseDay courseday);

    }
}
