using Admin_Panel_ITI.Models;

namespace Admin_Panel_ITI.Repos.Interfaces
{
    public interface IExam_QuestionRepository
    {
        public int GetQuestionNumbersInExam(int examiD);
        public List<Exam_Question> GetExambyqid(int qid);
        public void DeleteExam_Question(int ExamId, int questionID);
        public void DeleteExam_Question(int questionID);
        public void CreateExam_Question(Exam_Question exam_question);
        public void CreateExam_Question(List<Exam_Question> exam_question);
        public void CreateExam_QuestionForQuestions(List<int> QIds, int examid);
        public List<Exam_Question> GetExamsRecordsbyExamID(int exID);
        public void CreateExam_QuestionForExams(List<int> examIDs, int qid);
        public List<int> DeleteExamQuestions(int ExamId);
    }
}
