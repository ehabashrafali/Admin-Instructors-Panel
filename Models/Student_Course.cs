using Admin_Panel_ITI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin_Panel_ITI.Models
{
    [Table("Student_Course")]
    public class Student_Course
    {

        [ForeignKey(nameof(Student))]
        public string StudentID { get; set; }
        public virtual Student? Student { get; set; }



        [ForeignKey(nameof(Course))]
        public int CourseID { get; set; }
        public virtual Course? Course { get; set; }
    }
}
