namespace Admin_Panel_ITI.Repos.Interfaces
{
    public interface IIntake_Track_CourseRepository
    {

        public void DeleteIntake_Track_CoursebyCourseID(int courseID);
        public void DeleteIntake_Track_CoursebyTrackID(int trackID);
    }
}
