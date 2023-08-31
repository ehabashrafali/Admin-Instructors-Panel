using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Admin_Panel_ITI.Models
{
    [Table("Track_Course")]
    public class Track_Course
    {
        [ForeignKey(nameof(Track))]
        public int TrackID { get; set; }
        public virtual Track? Track { get; set; }


        [ForeignKey(nameof(Course))]
        public int CourseID { get; set; }
        public virtual Course? Course { get; set; }

    }
}
