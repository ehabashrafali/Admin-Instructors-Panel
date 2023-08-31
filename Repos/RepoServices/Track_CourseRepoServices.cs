using Admin_Panel_ITI.Data;
using Admin_Panel_ITI.Models;

namespace Admin_Panel_ITI.Repos
{
    public class Track_CourseRepoServices : ITrack_CourseRepository
    {
        public MainDBContext Context { get; set; }

        public Track_CourseRepoServices(MainDBContext context)
        {
            Context = context;
        }
        void ITrack_CourseRepository.CreateTrack_Course(Track_Course track_course)
        {
            Context.Track_Courses.Add(track_course);
            Context.SaveChanges();
        }

        void ITrack_CourseRepository.CreateTrack_Course(List<Track_Course> track_courses)
        {
            foreach (var item in track_courses)
            {
                Context.Track_Courses.Add(item);
            }
            Context.SaveChanges() ;

        }

        void ITrack_CourseRepository.CreateTrack_Course(List<int> trackids, int courseid)
        {

            foreach (var trackid in trackids)
            {
                Track_Course tc = new Track_Course()
                {
                    TrackID = trackid,
                    CourseID = courseid
                };

                Context.Track_Courses.Add(tc);
            }
            Context.SaveChanges();
        }

        void ITrack_CourseRepository.DeleteTrack_Course(int trackID, int courseID)
        {
            var track_course = Context.Track_Courses.FirstOrDefault(tc=>tc.TrackID==trackID && tc.CourseID==courseID);

            Context.Track_Courses.Remove(track_course);
            Context.SaveChanges();
        }


        void ITrack_CourseRepository.DeleteTrack_Course(int trackID)
        {
            var track_course = Context.Track_Courses.Where(tc => tc.TrackID == trackID).ToList();

            foreach(var item in track_course)
            {
                Context.Track_Courses.Remove(item);
            }
            Context.SaveChanges();
        }
        void ITrack_CourseRepository.DeleteTrack_CourseByCourseID(int courseID)
        {
            var track_course = Context.Track_Courses.Where(tc => tc.CourseID == courseID).ToList();

            foreach(var item in track_course)
            {
                Context.Track_Courses.Remove(item);
            }
            Context.SaveChanges();
        }


        List<Track_Course> ITrack_CourseRepository.getTrack_CoursebyTrackID(int trackID)
        {
            var track_course = Context.Track_Courses.Where(tc => tc.TrackID == trackID).ToList();
            return track_course;

        }
        List<Track_Course> ITrack_CourseRepository.getTrack_CoursebyCourseID(int courseID)
        {
            var track_course = Context.Track_Courses.Where(tc => tc.CourseID == courseID).ToList();
            return track_course;
        }

        List<Track_Course> ITrack_CourseRepository.GetTrack_Courses()
        {
            return Context.Track_Courses.ToList();
        }


        // Maybe deleted
        //void ITrack_CourseRepository.UpdateTrack_Course(int trackID, int courseID, Track_Course track_course)
        //{
        //    var track_course_updated = Context.Track_Courses.FirstOrDefault(tc => tc.TrackID == trackID && tc.CourseID == courseID);
        //    track_course_updated.TrackID = track_course.TrackID;
        //    track_course_updated.CourseID = track_course.CourseID;
        //    Context.SaveChanges();
        //}

        int ITrack_CourseRepository.GetTrack_CourseNumber(int trackID)
        {
            return Context.Track_Courses.Select(tc => tc.TrackID == trackID).Count();
        }
    }
}
