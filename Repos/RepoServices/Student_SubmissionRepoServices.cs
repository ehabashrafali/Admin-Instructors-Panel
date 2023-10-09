using Admin_Panel_ITI.Data;
using Admin_Panel_ITI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Admin_Panel_ITI.Repos
{
    public class Student_SubmissionRepoServices : IStudent_SubmissionRepository
    {

        private MainDBContext Context { get; set; }

        public Student_SubmissionRepoServices(MainDBContext context)
        {
            Context = context;
        }

        int IStudent_SubmissionRepository.GetStudent_SubmissionNumber()
        {
            return Context.Student_Submissions.Count();
        }        

        int IStudent_SubmissionRepository.GetStudent_SubmissionNumber(int stdid)
        {
            return Context.Student_Submissions.Where(ss=>ss.StudentID==stdid.ToString()).Count();
        }

        Student_Submission IStudent_SubmissionRepository.GetStudent_Submission(int studentID, int courseDayID)
        {
            var student_submission = Context.Student_Submissions.SingleOrDefault(ss => ss.StudentID == studentID.ToString() && ss.CourseDayID == courseDayID);
            return student_submission;
        }

        List<Student_Submission> IStudent_SubmissionRepository.GetStudent_Submissions()
        {
            return Context.Student_Submissions.ToList();
        }

        List<Student_Submission> IStudent_SubmissionRepository.GetStudent_SubmissionsByStdIDCrsDayID(int studentID, int courseDayID)
        {
            var student_submission = Context.Student_Submissions.Where(ss => ss.StudentID == studentID.ToString() && ss.CourseDayID == courseDayID).ToList();
            return student_submission;
        }


        List<Student_Submission> IStudent_SubmissionRepository.GetAll_SubmissionsByCrsDay(int courseDayID)
        {
            var all_submissions = Context.Student_Submissions
                                         .Where(ss=> ss.CourseDayID == courseDayID)
                                         .Include(ss=> ss.Student)
                                         .ThenInclude(s=> s.AspNetUser)
                                         .ToList();
            return all_submissions;
        }

        List<Student_Submission> IStudent_SubmissionRepository.GetStudent_SubmissionsByStdIDCrsDayID(int studentID)
        {
            var student_submission = Context.Student_Submissions.Where(ss => ss.StudentID == studentID.ToString() ).ToList();
            return student_submission;
        }

        void IStudent_SubmissionRepository.UpdateStudent_Submission(int studentID, int courseDayID, Student_Submission Student_Submission)
        {
            var student_submission = Context.Student_Submissions.SingleOrDefault(ss => ss.StudentID == studentID.ToString() && ss.CourseDayID == courseDayID);
            student_submission.SubmissionPath = Student_Submission.SubmissionPath;
            student_submission.SubmissionGrade = Student_Submission.SubmissionGrade;
            student_submission.StudentID = Student_Submission.StudentID;
            //student_submission.SubmessionID = Student_Submission.SubmessionID;
            student_submission.CourseDayID = Student_Submission.CourseDayID;
            Context.SaveChanges();
        }

        void IStudent_SubmissionRepository.DeleteStudent_Submission(string studentID, int courseDayID)
        {
            var student_submission = Context.Student_Submissions.SingleOrDefault(ss => ss.StudentID == studentID.ToString() && ss.CourseDayID == courseDayID );
            Context.Student_Submissions.Remove(student_submission);
            Context.SaveChanges();

        }

        void IStudent_SubmissionRepository.DeleteStudent_Submission(string studentID)
        {
            var student_submission = Context.Student_Submissions.Where(ss => ss.StudentID == studentID.ToString() );
            foreach (var item in student_submission)
            {
                Context.Student_Submissions.Remove(item);
            }
            Context.SaveChanges();

        }

        void IStudent_SubmissionRepository.CreateStudent_Submission(Student_Submission student_Submission)
        {

            Context.Student_Submissions.Add(student_Submission);
            Context.SaveChanges();
        }

        void IStudent_SubmissionRepository.CreateStudent_Submission(int stdID, int crsDayID, string submissionPath)
        {
       



            Student_Submission ssbm = new Student_Submission()
            {
                SubmissionPath = submissionPath,
                SubmissionGrade = -1,
                CourseDayID = crsDayID,
                StudentID = stdID.ToString()
            };
            Context.Student_Submissions.Add(ssbm);
            Context.SaveChanges();
        }

        void IStudent_SubmissionRepository.DeleteStudent_SubmissionbyCourseDayID(int courseDayID)
        {
            var submissions = Context.Student_Submissions.Where(ss => ss.CourseDayID == courseDayID).ToList();
            foreach (var submission in submissions)
            {
                Context.Student_Submissions.Remove(submission);
            }
            Context.SaveChanges();
        }

        void IStudent_SubmissionRepository.UpdateGrade(string studentId, int courseDayId, int grade)
        {
            var sub_grade = Context.Student_Submissions.SingleOrDefault(ss => ss.StudentID == studentId && ss.CourseDayID == courseDayId);
            sub_grade.SubmissionGrade = grade;
            Context.SaveChanges();
        }
    }
}
