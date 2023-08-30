using Admin_Panel_ITI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin_Panel_ITI.Models
{
    [Table("Material")]
    public class Material
    {
        public int ID { get; set; }
        public string Path { get; set; }



        [ForeignKey(nameof(Instructor))]
        public string InstructorID { get; set; }
        public virtual Instructor? Instructor { get; set; }


        public virtual IEnumerable<Course_Day_Material>? CourseDayMaterials { set; get; }
    }
}
