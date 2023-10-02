using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin_Panel_ITI.Models
{

    [Table("Question")]
    public class Question
    {
        [Key, JsonIgnore]
        public int ID { get; set; }

        [MaxLength(10)]
        public string Type { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public string Answer { get; set; }

        [Required]
        public double Mark { get; set; }



        public virtual IEnumerable<Exam_Std_Question>? Student_Quest_Exam { get; set; }

        public virtual IEnumerable<Exam_Question>? Exam_Question { get; set; }

    }
}
