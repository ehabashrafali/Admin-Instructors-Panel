using Admin_Panel_ITI.Data;
using Admin_Panel_ITI.Models;
using Admin_Panel_ITI.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Admin_Panel_ITI.Repos
{
    public class TrackRepoServices : ITrackRepository
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ITrack_CourseRepository _track_courseRepository;
        private readonly IIntake_TrackRepository _intake_trackRepository;

        public MainDBContext Context { get; set; }
        public TrackRepoServices( MainDBContext context, IStudentRepository studentRepository, ITrack_CourseRepository track_courseRepository, IIntake_TrackRepository intake_trackRepository)
        {
            Context = context;
            _studentRepository = studentRepository;
            _track_courseRepository = track_courseRepository;
            _intake_trackRepository = intake_trackRepository;
        }
        Track ITrackRepository.getTrackbyID(int trackID)
        {
            var track =  Context.Tracks.Include(t=>t.Manager)
                                       .Include(t2 => t2.TrackCourses)
                                       .Include(t3=>t3.IntakeTracks)
                                       .FirstOrDefault(t=> t.ID == trackID);
            return track;
        }

        int ITrackRepository.getTrackNumber()
        {
            return Context.Tracks.Count();
        }       
        
        int ITrackRepository.getTrackNumberbyIntakeID(int intakeID)
        {
            return Context.Intake_Tracks.Where(it=>it.IntakeID == intakeID).Count();
        }

        List<Track> ITrackRepository.getTracks()
        {
            return Context.Tracks.Include(t => t.Manager)
                                .Include(t => t.TrackCourses)
                                .Include(t => t.IntakeTracks).ToList();
        }


        // Check should we update virtual navigation properties 
        void ITrackRepository.UpdateTrack(int trackID, Track track)
        {
            var trackUpdated  = Context.Tracks.FirstOrDefault(t=>t.ID == trackID);

            trackUpdated.Name = track.Name;
            trackUpdated.CreationDate = track.CreationDate;
            trackUpdated.ManagerID = track.ManagerID;
            trackUpdated.AdminID = track.AdminID;
            Context.SaveChanges();
        }

        void ITrackRepository.DeleteTrack(int trackID)
        {

            var students = _studentRepository.getStudentsbyTrackID(trackID);
            if (students.Count == 0)
            {

                _track_courseRepository.DeleteTrack_Course(trackID);
                _intake_trackRepository.DeleteIntake_Track(trackID);

                var track = Context.Tracks.FirstOrDefault(t => t.ID == trackID);
                Context.Tracks.Remove(track);
                Context.SaveChanges();


            }
      

        }

        void ITrackRepository.CreateTrack( Track track)
        {
            Context.Tracks.Add(track);
            Context.SaveChanges();
        }
        

        // check null or na

        void ITrackRepository.RemoveManager(int managerID)
        {
            var tracks = Context.Tracks.Where(t=>t.ManagerID == managerID.ToString()).ToList();
            foreach (var track in tracks)
            {
                track.ManagerID = null;
            }
            Context.SaveChanges();
        }
    }
}
