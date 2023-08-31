using Admin_Panel_ITI.Data;
using Admin_Panel_ITI.Models;

namespace Admin_Panel_ITI.Repos.RepoServices
{
    public class AdminRepoServices : IAdminRepository
    {
        public MainDBContext Context { get; set; }
        public AdminRepoServices(MainDBContext context)
        {
            Context = context;
        }


        void IAdminRepository.CreateAdmin(Admin admin)
        {
            Context.Admins.Add(admin);
            Context.SaveChanges();
        }

        //void IAdminRepository.CreateAdmin(string adminID)
        //{
        //    var admin = Context.Admins.FirstOrDefault(a => a.Id == adminID);
        //    Console.WriteLine(admin);
        //    //Context.Admins.Add(admin);
        //    //Context.SaveChanges();
        //}

        void IAdminRepository.DeleteAdmin(int adminID)
        {
            var admin = Context.Admins.FirstOrDefault(ad=>ad.Id == adminID.ToString());
            Context.Admins.Remove(admin);
            Context.SaveChanges();

        }

        Admin IAdminRepository.GetAdminbyID(int adminID)
        {
            var admin = Context.Admins.FirstOrDefault(ad => ad.Id == adminID.ToString());
            return admin;
        }

        int IAdminRepository.GetAdminNumber()
        {
            return Context.Admins.Count();
        }

        List<Admin> IAdminRepository.GetAdmins()
        {
            return Context.Admins.ToList();
        }

        void IAdminRepository.UpdateAdmin(int adminID, Admin admin)
        {
            var admin_Updated = Context.Admins.FirstOrDefault(ad => ad.Id == adminID.ToString());
            admin_Updated.FullName = admin.FullName;
            admin_Updated.UserName = admin.UserName;
            admin_Updated.PasswordHash = admin.PasswordHash;
            admin_Updated.PhoneNumber = admin.PhoneNumber;
            Context.SaveChanges();
        }
    }
}
