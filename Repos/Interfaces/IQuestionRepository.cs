using Admin_Panel_ITI.Models;

namespace Admin_Panel_ITI.Repos.Interfaces
{
    public interface IQuestionRepository
    {
        public int getQuestionsNumber();
        public Question getQuestionbyID(int questionbyID);
        public List<Question> getQuestions();
        public void UpdateQuestion(int questionID, Question question);
        public void DeleteQuestion(int questionID);
        public void CreateQuestion(Question question);
        public Task<int[]> CreateQuestion(Question[] qs );
        public void DeleteQuestions(List<int> QuestionsIDs);
    }
}