using Admin_Panel_ITI.Data;
using Admin_Panel_ITI.Models;
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

        void IIntake_InstructorRepository.AddIntake_Instructor(Intake_Instructor itc)
        {
            Context.Intake_Instructors.Add(itc);
            Context.SaveChanges();
        }


        //---//
        public void AddIntake_Instructor(int IntakeID, string instructorID)
        {
            Intake_Instructor intake_Instructor = new Intake_Instructor()
            {
                IntakeID = IntakeID,
                InstructorID = instructorID
            };

            Context.Intake_Instructors.Add(intake_Instructor);
            Context.SaveChanges();
        }

    }
}
