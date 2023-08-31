using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Admin_Panel_ITI.Models
{
    [Table("Admin")]
    public class Admin : AppUser
    {


        public virtual IEnumerable<Intake>? Intakes { get; set; }
        public virtual IEnumerable<Track>? Tracks { get; set; }
        public virtual IEnumerable<Student>? Students { get; set; }
        public virtual IEnumerable<Instructor>? Instructors { get; set; }
        public virtual IEnumerable<Exam>? Exams { get; set; }


    }
}
