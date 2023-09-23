using System.ComponentModel.DataAnnotations;

namespace Admin_Panel_ITI.Areas.InstructorsArea.ViewModels
{
    public class CourseDay_Material_TaskVM
    {
        [Required]
        public int DayNumber { get; set; }

        [Required]
        public List<IFormFile> Materials { get; set; }

        [Required]
        public  IFormFile Task { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime DeadLine { get; set; }
    }
}
