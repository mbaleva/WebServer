namespace WebServer.MVC.Identity.EntityFrameworkCore
{
    using WebServer.MVC.Identity.EntityFrameworkCore.Models;
    using Microsoft.EntityFrameworkCore;
    public class IdentityDbContext : DbContext
    {
        public DbSet<IdentityUser> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GlobalConstants.ConnectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
