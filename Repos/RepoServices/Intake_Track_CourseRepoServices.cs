using Admin_Panel_ITI.Data;
using Admin_Panel_ITI.Repos.Interfaces;

namespace Admin_Panel_ITI.Repos.RepoServices
{
    public class Intake_Track_CourseRepoServices : IIntake_Track_CourseRepository
    {
        public MainDBContext Context { get; set; }

        public Intake_Track_CourseRepoServices(MainDBContext context)
        {
            Context = context;
        }
        void IIntake_Track_CourseRepository.DeleteIntake_Track_CoursebyCourseID(int courseID)
        {
            var records = Context.Intake_Track_Courses.Where(itc => itc.CourseID == courseID).ToList();
            foreach (var record in records)
            {
                Context.Intake_Track_Courses.Remove(record);
            }
            Context.SaveChanges();
        }

        void IIntake_Track_CourseRepository.DeleteIntake_Track_CoursebyTrackID(int trackID)
        {
            var records = Context.Intake_Track_Courses.Where(itc => itc.TrackID == trackID).ToList();
            Context.Intake_Track_Courses.RemoveRange(records);
        }


        void IIntake_Track_CourseRepository.DeleteIntake_Track_CoursebyIntakeID(int intakeID)
        {
            var records = Context.Intake_Track_Courses.Where(itc => itc.IntakeID == intakeID).ToList();
            Context.Intake_Track_Courses.RemoveRange(records);

        }
    }
}
