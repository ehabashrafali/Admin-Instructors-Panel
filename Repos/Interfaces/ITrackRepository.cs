using Admin_Panel_ITI.Models;

namespace Admin_Panel_ITI.Repos
{
    public interface ITrackRepository
    {
        public int getTrackNumber();
        public int getTrackNumberbyIntakeID(int intakeID);



        public Track getTrackbyID(int trackID);



        public List<Track> getTracks();


        public void UpdateTrack(int trackID, Track track);


        public void DeleteTrack(int trackID);


        public void CreateTrack(Track track);



        public void RemoveManager(int managerID);
    }
}
