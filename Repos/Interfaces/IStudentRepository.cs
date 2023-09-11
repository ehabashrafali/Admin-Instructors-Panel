using Admin_Panel_ITI.Models;

namespace Admin_Panel_ITI.Repos
{
    public interface IStudentRepository
    {
        public int getStudentNumber();
        public int getStudentNumberbyIntakeID(int intakeID);
        public int getStudentNumberbyTrackID(int trackID);
        public Student getStudentbyID(string studentbyID);
        public List<Student> getStudents(int pageNumber, int pageSize);
        public void UpdateStudent(string studentID, Student student);
        public void DeleteStudent(string studentID);
        public List<Student> getStudentsbyTrackID(int trackid, int pageNumber, int pageSize);
        public List<Student> getStudentsbyTrackID(int trackID);
        public void CreateStudent(Student student);
        public List<Student> getStudentsbyIntakeID(int intakeID, int pageNumber, int pageSize);
        public List<Student> getStudentsbyIntakeID(int intakeID);



        /*---------------------------------------------- Instructor Repos -----------------------------------------------*/
        public int GetStudentsNumberbyIntakeTrackID(int intakeID, int trackID);
        public List<Student> GetStudentsbyIntakeTrackID(int intakeID, int trackID);

    }
}
