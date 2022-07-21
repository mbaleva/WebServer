namespace Template.Data
{
    using WebServer.MVC.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using WebServer.MVC;

    public class ApplicationDbContext : IdentityDbContext
    {
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
