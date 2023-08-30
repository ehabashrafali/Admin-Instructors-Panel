using Admin_Panel_ITI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin_Panel_ITI.Models
{
    [Table("Instructor_Course")]
    public class Instructor_Course
    {
        [ForeignKey(nameof(Course))]
        public int CourseID { get; set; }
        public virtual Course? Course { get; set; }



        [ForeignKey(nameof(Instructor))]
        public string InstructorID { get; set; }
        public virtual Instructor? Instructor { get; set; }
    }
}
