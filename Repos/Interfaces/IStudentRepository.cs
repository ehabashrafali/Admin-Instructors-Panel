using Admin_Panel_ITI.Models;

namespace Admin_Panel_ITI.Repos
{
    public interface IStudentRepository
    {
        public int getStudentNumber();
        public int getStudentNumberbyIntakeID(int intakeID);



        public Student getStudentbyID(int studentbyID);



        public List<Student> getStudents();


        public void UpdateStudent(int studentID, Student student);


        public void DeleteStudent(int studentID);
        public List<Student> getStudentsbyTrackID(int trackid);


        public void CreateStudent(Student student);
        public List<Student> getStudentsbyIntakeID(int intakeID);
    }
}
