using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Admin_Panel_ITI.Areas.InstructorsArea.ViewModels
{
    public class InstructorCourseVM
    {
        [DisplayName("Course Name")]
        public string CourseName { get; set; }



        [DisplayName("Instructor")]
        public string? InstructorName { get; set; }


        public int Duration { get; set; }


        [DisplayName("Admin")]
        public string AdminName { get; set; }
    }
}
