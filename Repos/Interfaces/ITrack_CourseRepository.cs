using Admin_Panel_ITI.Models;

namespace Admin_Panel_ITI.Repos
{
    public interface ITrack_CourseRepository
    {


        public int GetTrack_CourseNumber(int courseID);

        public List<Track_Course> getTrack_CoursebyTrackID(int trackID);

        public List<Track_Course> getTrack_CoursebyCourseID(int courseID);



        public List<Track_Course> GetTrack_Courses();


        //public void UpdateTrack_Course(int trackID, int courseID, Track_Course track_course);


        public void DeleteTrack_Course(int trackID, int courseID);
        public void DeleteTrack_Course(int trackID);
        public void DeleteTrack_CourseByCourseID(int courseID);


        public void CreateTrack_Course(Track_Course track_course);
        public void CreateTrack_Course(List<Track_Course> track_courses);
        public void CreateTrack_Course(List<int> trackid, int courseid);


    }
}
