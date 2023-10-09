using Admin_Panel_ITI.Models;

namespace Admin_Panel_ITI.Repos
{
    public interface ITrackRepository
    {
        public int getTrackNumber();
        public int getTrackNumberbyIntakeID(int intakeID);
        public Track getTrackbyID(int trackID);
        public List<Track> getTracks();
        public List<Track> getTracks(int pageNumber, int pageSize);
        List<Intake_Track_Course> getTrackbyIntakeID(int intakeID, int pageNumber, int pageSize);
        public void UpdateTrack(int trackID, Track track);
        public void DeleteTrack(int trackID);
        public void DeleteTrack(List<int> trackIDs);
        public string getTrackName(int trackID);
        public void CreateTrack(Track track);
        public void RemoveManager(string managerID);
        public List<Track> GetTracksByIntakeID(int intakeid);
        public List<Track> GetTracksByIntakeId(int intakeid, int pageNumber, int pageSize);


        /*---------------------------------------------- Instructor Repos -----------------------------------------------*/


        //get the tracks based on specific intake, and specific Manager.
        public List<Track> getTracks(string ManagerID, int intakeID);

        //get the tracks based on specific intake, and specific Instructor.
        public List<Track> GetTracksByTeacher(int intakeID, string InstructorID);
    }
}
