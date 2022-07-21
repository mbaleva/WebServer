namespace WebServer.MVC.Attributes.Security
{
    using System;
    using WebServer.MVC.Identity.EntityFrameworkCore.Models;

    public class AuthorizeAttribute : Attribute
    {
        private bool isAdminNeeed;
        public AuthorizeAttribute(bool IsAdminNeeded = false)
        {
            this.isAdminNeeed = IsAdminNeeded;
        }
        public AuthorizeAttribute()
        {

        }
        public AuthorizeAttribute(string userId)
        {
            this.IsLoggedIn(userId);
        }
        public bool IsLoggedIn(string userId)
        {
            return userId != null;
        }
        public bool HasPermissionsToAccess(IdentityUser user) 
        {
            if (!this.isAdminNeeed)
            {
                return true;
            }
            return user.Role == IdentityRole.Administrator;
        }
    }
}
