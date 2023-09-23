using Admin_Panel_ITI.Models;
using System.ComponentModel.DataAnnotations;

namespace Admin_Panel_ITI.Areas.InstructorsArea.ViewModels
{
    public class Exam_QuestionsVM
    {
        [Required, MaxLength(100)]
        public string ExamName { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        public List<Question> Questions { get; set; }
    }
}
