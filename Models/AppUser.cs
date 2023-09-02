using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin_Panel_ITI.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }



        [InverseProperty("Admin")]
        public virtual IEnumerable<Instructor>? Instructors_Admin { get; set; }
        [InverseProperty("AspNetUser")]
        public virtual IEnumerable<Instructor>? Instructors_AspNetUser { get; set; }



        public virtual IEnumerable<Admin>? Admins { get; set; }




        [InverseProperty("AspNetUser")]
        public virtual IEnumerable<Student>? Students_AspNetUser { get; set; }
        [InverseProperty("Admin")]
        public virtual IEnumerable<Student>? Students_Admin { get; set; }

    }
}
