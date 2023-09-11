using Admin_Panel_ITI.Models;

namespace Admin_Panel_ITI.Repos
{
    public interface IIntakeRepository
    {
        public int getIntakeNumber();
        public Intake getIntakebyID(int intakeID);
        public List<Intake> GetIntakes();
        public void UpdateIntake(int IntakeID, Intake intake);
        public void DeleteIntake(int intakeID);





        //---------------------- asem_updates ---------------------------------------//

        public List<Intake> GetAllIntakes();

    }
}
