using Admin_Panel_ITI.Data;
using Admin_Panel_ITI.Models;
using Admin_Panel_ITI.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Admin_Panel_ITI.Repos
{
    public class StudentRepoServices : IStudentRepository
    {
        private readonly IStudent_CourseRepository student_CourseRepository;
        private readonly IStudent_SubmissionRepository student_SubmissionRepository;
        private readonly IExam_Std_QuestionRepository exam_Std_QuestionRepository;

        public MainDBContext Context { get; set; }

        public StudentRepoServices(MainDBContext context, 
            IStudent_CourseRepository student_CourseRepository, 
            IStudent_SubmissionRepository student_SubmissionRepository, 
            IExam_Std_QuestionRepository exam_Std_QuestionRepository)
        {
            Context = context;
            this.student_CourseRepository = student_CourseRepository;
            this.student_SubmissionRepository = student_SubmissionRepository;
            this.exam_Std_QuestionRepository = exam_Std_QuestionRepository;
        }

        void IStudentRepository.CreateStudent(Student student)
        {
            Context.Students.Add(student);
        }

        void IStudentRepository.DeleteStudent(int studentID)
        {
            student_CourseRepository.DeleteStudent_Course(studentID);
            student_SubmissionRepository.DeleteStudent_Submission(studentID);
            exam_Std_QuestionRepository.DeleteExam_Std_Question(studentID);

            var student = Context.Students.FirstOrDefault(s => s.Id == studentID.ToString());
            Context.Students.Remove(student);
            Context.SaveChanges();
        }

        Student IStudentRepository.getStudentbyID(int studentID)
        {
            var student = Context.Students.FirstOrDefault(s => s.Id == studentID.ToString());
            return student;
        }

        int IStudentRepository.getStudentNumber()
        {
            return Context.Students.Count();
        }   
        
        int IStudentRepository.getStudentNumberbyIntakeID(int intakeID)
        {
            return Context.Students.Where(s=>s.IntakeID== intakeID).Count();
        }

        List<Student> IStudentRepository.getStudents()
        {
            return Context.Students.Include(s=>s.Admin)
                                   .Include(s=>s.Intake)
                                   .Include(s=>s.Track)
                                   .Include(s=>s.StudentCourses)
                                   .ToList();
        }


        List<Student> IStudentRepository.getStudentsbyTrackID(int trackid)
        {
            var students = Context.Students.Where(s => s.TrackID == trackid).ToList();
            return students;
        }        
        
         List<Student> IStudentRepository.getStudentsbyIntakeID(int intakeID)
         {
            var students = Context.Students.Where(s => s.IntakeID == intakeID).ToList();
            return students;
         }

        void IStudentRepository.UpdateStudent(int studentID, Student student)
        {
            var studentUpdated = Context.Students.FirstOrDefault(s=> s.Id == studentID.ToString());

            studentUpdated.IsGraduated = student.IsGraduated;
            studentUpdated.FullName = student.FullName;
            studentUpdated.UserName = student.UserName;
            studentUpdated.AdminID = student.AdminID;
            studentUpdated.IntakeID = student.IntakeID;
            studentUpdated.TrackID = student.TrackID;

            Context.SaveChanges();

        }
    }
}
