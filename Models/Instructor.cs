using Admin_Panel_ITI.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin_Panel_ITI.Models
{
    [Table("Instructor")]
    public class Instructor : AppUser
    {

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required, Column(TypeName = "date")]
        public DateTime CreationDate { get; set; }







        [ForeignKey(nameof(Admin))]
        public string AdminID { get; set; }
        public virtual Admin? Admin { get; set; }


        public virtual IEnumerable<Track>? Tracks { get; set; }
        public virtual IEnumerable<Exam>? Exams { get; set; }


        public virtual IEnumerable<Instructor_Course>? InstructorCourses { get; set; }
        public virtual IEnumerable<Intake_Instructor>? IntakeInstructors { get; set; }
        public virtual IEnumerable<Material>? Materials { get; set; }



    }
}
