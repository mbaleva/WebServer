namespace PCShop.Common
{
    using PCShop.Data;
    using System.Linq;
    using WebServer.MVC.Identity.EntityFrameworkCore.Models;

    public static class AuthHelper
    {
        public static bool IsUserAdministrator(string userId, ApplicationDbContext db)
        {
            var user = db.Users.Where(x => x.Id == userId).FirstOrDefault();

            if (user != null)
            {
                return user.Role == IdentityRole.Administrator;
            }
            return false;
        }
    }
}
