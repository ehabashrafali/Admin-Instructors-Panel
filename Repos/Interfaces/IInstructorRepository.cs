using Admin_Panel_ITI.Models;

namespace Admin_Panel_ITI.Repos.Interfaces
{
    public interface IInstructorRepository
    {


        public int GetInstructorNumber();
        
        
        public int GetInstructorNumberbyIntakeID(int intakeID);



        public Instructor GetInstructorbyID(int instructorID);



        public List<Instructor> GetInstructors();


        public void UpdateInstructor(int instructorID, Instructor instructor);


        public void DeleteInstructor(int instructorID);


        public void CreateInstructor(Instructor instructor);
    }
}
