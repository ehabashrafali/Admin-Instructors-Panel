using Admin_Panel_ITI.Data;
using Admin_Panel_ITI.Repos.Interfaces;

namespace Admin_Panel_ITI.Repos.RepoServices
{
    public class Intake_InstructorRepoServices : IIntake_InstructorRepository
    {
        public MainDBContext Context { get; set; }


        public Intake_InstructorRepoServices(MainDBContext context)
        {
            Context = context;
        }
        void IIntake_InstructorRepository.deleteIntake_InstructorbyInstructorID(string instructorID)
        {
            var records = Context.Intake_Instructors.Where(ii => ii.InstructorID == instructorID);
            foreach (var item in records)
            {
                Context.Intake_Instructors.Remove(item);
            }
            Context.SaveChanges();
        }
    }
}
