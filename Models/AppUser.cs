using Microsoft.AspNetCore.Identity;

namespace Admin_Panel_ITI.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        
        public string Email { get; set; }

        public string Phone { get; set; }



    }
}
