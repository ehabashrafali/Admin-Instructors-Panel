using Admin_Panel_ITI.Models;

namespace Admin_Panel_ITI.Repos.Interfaces
{
    public interface IIntake_InstructorRepository
    {

        public void deleteIntake_InstructorbyInstructorID(string instructorID);
        public void AddIntake_Instructor(Intake_Instructor itc);
    }
}
