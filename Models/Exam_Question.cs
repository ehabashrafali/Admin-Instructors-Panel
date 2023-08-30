using System.ComponentModel.DataAnnotations.Schema;

namespace Admin_Panel_ITI.Models
{
    [Table("Exam_Question")]
    public class Exam_Question
    {
        [ForeignKey(nameof(Question))]
        public int QuestionID { get; set; }
        public virtual Question? Question { get; set; }


        [ForeignKey(nameof(Exam))]
        public int ExamID { get; set; }
        public virtual Exam? Exam { get; set; }

    }
}
