namespace Admin_Panel_ITI.ViewModels
{
    public class HomePageViewModel
    {
        public int IntakeNumber { get; set; }
        public int TrackNumber { get; set; }
        public int StudentNumber { get; set; }
        public int InstructorNumber { get; set; }
        public int CourseNumber { get; set; }
        public int IntakeId { get; internal set; }
        public List<int> TrackIds { get; set; }
    }

}
