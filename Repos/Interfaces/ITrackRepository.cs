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


        public void CreateTrack(Track track);



        public void RemoveManager(string managerID);
    }
}
