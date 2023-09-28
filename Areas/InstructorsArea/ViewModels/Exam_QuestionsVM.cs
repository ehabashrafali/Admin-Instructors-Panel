using Admin_Panel_ITI.Models;
using System.ComponentModel.DataAnnotations;

namespace Admin_Panel_ITI.Areas.InstructorsArea.ViewModels
{
    public class Exam_QuestionsVM
    {
        [MaxLength(100)]
        [Required(ErrorMessage = "Please enter the Exam Name.")]
        public string ExamName { get; set; }

        [Required(ErrorMessage = "Please enter the Exam Duration.")]
        public int Duration { get; set; }

        [Required]
        public List<Question> Questions { get; set; }
    }
}
