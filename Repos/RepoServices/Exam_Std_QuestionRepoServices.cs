using Admin_Panel_ITI.Areas.InstructorsArea.ViewModels;
using Admin_Panel_ITI.Data;
using Admin_Panel_ITI.Models;
using Admin_Panel_ITI.Repos.Interfaces;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Admin_Panel_ITI.Repos.RepoServices
{
    public class Exam_Std_QuestionRepoServices : IExam_Std_QuestionRepository
    {
        public MainDBContext Context { get; set; }

        public Exam_Std_QuestionRepoServices(MainDBContext context)
        {
            Context = context;
        }

        void IExam_Std_QuestionRepository.CreateExam_Std_Question(Exam_Std_Question esq)
        {
            Context.Exam_Std_Questions.Add(esq);
            Context.SaveChanges();
        }

        void IExam_Std_QuestionRepository.DeleteExam_Std_Question(int examID, string studentID, int questionID)
        {
            var esq = Context.Exam_Std_Questions.SingleOrDefault(esq => esq.ExamID == examID && esq.StudentID == studentID.ToString() && esq.QuestionID == questionID);
            Context.Exam_Std_Questions.Remove(esq);
            Context.SaveChanges();
        }

        void IExam_Std_QuestionRepository.DeleteExam_Std_Question(int examID)
        {
            var esq = Context.Exam_Std_Questions.Where(sq => sq.ExamID == examID);
            Context.Exam_Std_Questions.RemoveRange(esq);
            Context.SaveChanges();
        }

        void IExam_Std_QuestionRepository.DeleteExam_Std_Question(string studentID)
        {
            var esq = Context.Exam_Std_Questions.Where(esq=> esq.StudentID == studentID.ToString() );
            foreach (var item in esq)
            {
                Context.Exam_Std_Questions.Remove(item);
            }
            Context.SaveChanges();
        }

        List<Exam_Std_Question> IExam_Std_QuestionRepository.GetExambyStdIDExmID(int stdID, int exmID)
        {
            return Context.Exam_Std_Questions.Where(es => es.StudentID == stdID.ToString() && es.ExamID == exmID).Include(es=>es.Question).ToList();

        }

        List<Exam_Std_Question> IExam_Std_QuestionRepository.GetExamsbyqid(int qid)
        {
            return Context.Exam_Std_Questions.Where(es => es.QuestionID == qid).ToList();

        }

        Exam_Std_Question IExam_Std_QuestionRepository.GetExam_Std_QuestionbyID(int examID, int studentID, int questionID)
        {
            var esq = Context.Exam_Std_Questions.SingleOrDefault(esq => esq.ExamID == examID && esq.StudentID == studentID.ToString() && esq.QuestionID == questionID);
            return esq;

        }

        void IExam_Std_QuestionRepository.UpdateExam_Std_Question(int examID, int studentID, int questionID, Exam_Std_Question main_esq)
        {
            var esq = Context.Exam_Std_Questions.SingleOrDefault(esq => esq.ExamID == examID && esq.StudentID == studentID.ToString() && esq.QuestionID == questionID);
            esq.StudentAnswer = main_esq.StudentAnswer;

            esq.StudentGrade = main_esq.StudentGrade;

            esq.StudentID = main_esq.StudentID;
            esq.ExamID = main_esq.ExamID;
            esq.QuestionID = main_esq.QuestionID;

            Context.SaveChanges();
        }

        public List<ExamSubmitionsVM> GetExam(int examID)
        {

            var result = Context.Exam_Std_Questions
                .Where(esq => esq.ExamID == examID)
                .GroupBy(esq => new { esq.StudentID, esq.Student.AspNetUser.FullName})
                .Select(obj => new
                {
                    StudentFullName = obj.Key.FullName,
                    TotalGrade = obj.Sum(esq => esq.StudentGrade)
                })
                .ToList();


            List<ExamSubmitionsVM> ESList = new();

            foreach (var item in result)
            {
                ExamSubmitionsVM esM = new ExamSubmitionsVM()
                {
                    StdFullName = item.StudentFullName,
                    TotalGrade = item.TotalGrade
                };

                ESList.Add(esM);
            }

            return ESList;
        }
    }
}
