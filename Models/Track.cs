using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Admin_Panel_ITI.Models
{
    [Table("Track")]
    public class Track
    {
        [Key]
        public int ID { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required, Column(TypeName = "date")]
        public DateTime CreationDate { get; set; }


        [ForeignKey(nameof(Manager))]
        public string ManagerID { get; set; }  //InstructorID, string because it will be GUID (hashed password)
        public virtual Instructor? Manager { get; set; }



        [ForeignKey(nameof(Admin))]
        public string? AdminID { get; set; }
        public virtual Admin? Admin { get; set; }


        public virtual IEnumerable<Intake_Track>? IntakeTracks { get; set; }
        public virtual IEnumerable<Intake_Track_Course>? IntakeTrackCourse { get; set; }


    }
}
