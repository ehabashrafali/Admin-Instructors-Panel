using Admin_Panel_ITI.Models;

namespace Admin_Panel_ITI.Repos.Interfaces
{
    public interface IExamRepository
    {
        public int GetExamNumbers();
        public Exam GetExambyID(int examID);
        public List<Exam> GetExamsByInstructorIDAndCourseID(string instructorID, int crsid );
        public List<Exam> GetExamsbycourseID(int courseID);
        public List<Exam> GetExams(int pageNumber, int pageSize);
        public void UpdateExam(int examID, Exam exam);
        public void DeleteExam(int examID);
        public void RemoveInstructor(string instructorID);
        public int CreateExam(Exam exam);
        public int GetExamNumbersForCourse(int courseID);
    }
}