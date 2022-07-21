namespace WebServer.MVC.Identity
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using WebServer.MVC.Identity.EntityFrameworkCore;
    using WebServer.MVC.Identity.EntityFrameworkCore.Models;

    public class UserManager
    {
        private IdentityDbContext dbContext;

        public UserManager()
        {
            this.dbContext = new IdentityDbContext();
        }
        public bool IsEmailValid(string email)
        {
            return !this.dbContext.Users.Any(x => x.Email == email);
        }

        public bool IsUsernameValid(string username)
        {
            return !this.dbContext.Users.Any(x => x.Username == username);
        }

        public string Login(string username, string password)
        {
            if (this.dbContext.Users.Any(x => x.Username == username &&
            x.Password == this.GetHash(password.TrimEnd('\0'))))
            {
                return this.GetUserId(username);
            }
            return null;
        }
        private string GetUserId(string username)
        {
            return this.dbContext.Users.FirstOrDefault(x => x.Username == username)?.Id;
        }

        public void Register(string username, string email, string password)
        {
            IdentityUser user = new IdentityUser
            {
                Id = Guid.NewGuid().ToString(),
                Username = username,
                Email = email,
                Password = GetHash(password),
                Role = IdentityRole.User,
            };
            this.dbContext.Users.Add(user);
            this.dbContext.SaveChanges();
        }
        private string GetHash(string plainText)
        {
            var bytes = Encoding.UTF8.GetBytes(plainText);
            var hash = SHA512.Create();
            var hashedInputBytes = hash.ComputeHash(bytes);

            var hashedInputStringBuilder = new StringBuilder(128);
            foreach (var b in hashedInputBytes)
                hashedInputStringBuilder.Append(b.ToString("X2"));
            return hashedInputStringBuilder.ToString();
        }
    }
}
