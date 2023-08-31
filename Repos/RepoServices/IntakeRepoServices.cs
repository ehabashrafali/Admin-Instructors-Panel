using Admin_Panel_ITI.Data;
using Admin_Panel_ITI.Models;
using Admin_Panel_ITI.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Admin_Panel_ITI.Repos
{
    public class IntakeRepoServices : IIntakeRepository
    {
        private readonly IStudentRepository studentRepository;
        private readonly IIntake_TrackRepository intake_TrackRepository;
        public MainDBContext Context { get; set; }
        public IntakeRepoServices(
            MainDBContext context, 
            IStudentRepository studentRepository, 
            IIntake_TrackRepository intake_TrackRepository)
        {
            Context = context;
            this.studentRepository = studentRepository;
            this.intake_TrackRepository = intake_TrackRepository;
        }

        void IIntakeRepository.CreateIntake(Intake intake)
        {
            Context.Intakes.Add(intake);
            Context.SaveChanges();
        }

        //void IIntakeRepository.DeleteIntake(int intakeID)
        //{
        //    var intake_students = studentRepository.getStudentsbyIntakeID(intakeID);

        //    if(intake_students.Count() == 0)
        //    {
        //        intake_TrackRepository.DeleteIntake_Track(intakeID);
        //        var intake = Context.Intakes.FirstOrDefault(i => i.ID == intakeID);
        //        Context.Intakes.Remove(intake);
        //    }
        //}

        Intake IIntakeRepository.getIntakebyID(int intakeID)
        {
            var intake = Context.Intakes.FirstOrDefault(i => i.ID == intakeID);
            return intake;
        }

        int IIntakeRepository.getIntakeNumber()
        {
            return Context.Intakes.Count();
        }

        List<Intake> IIntakeRepository.GetIntakes()
        {
            return Context.Intakes.Include(c=>c.Admin)
                                 .Include(c=>c.IntakeTracks)
                                 .ToList();
        }



        void IIntakeRepository.UpdateIntake(int IntakeID, Intake intake)
        {
            var intakeUpdated = Context.Intakes.FirstOrDefault(i => i.ID == IntakeID);

            intakeUpdated.Name = intake.Name;
            intakeUpdated.StartDate = intake.StartDate;
            intakeUpdated.EndDate = intake.EndDate;
            intakeUpdated.Duration = intake.Duration;
            intakeUpdated.CreationDate = intake.CreationDate;
            intakeUpdated.AdminID = intake.AdminID;

            Context.SaveChanges();
        }



        //---------------------- asem_updates ---------------------------------------//

        public List<Intake> GetAllIntakes()
        {
            return Context.Intakes.Include(i => i.Admin).ToList();
        }


        void IIntakeRepository.DeleteIntake(int intakeID)
        {
            var intake_students = studentRepository.getStudentsbyIntakeID(intakeID);

            if (intake_students.Count() == 0)
            {
                intake_TrackRepository.DeleteIntake_Track(intakeID);


                var intake = Context.Intakes.FirstOrDefault(i => i.ID == intakeID);
                Context.Intakes.Remove(intake);
            }
        }
    }
}
