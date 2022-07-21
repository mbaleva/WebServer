namespace PCShop.Data
{
    using Microsoft.EntityFrameworkCore;
    using PCShop.Data.Models;
    using WebServer.MVC.Identity.EntityFrameworkCore;
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<CPU> Processors { get; set; }
        public DbSet<GPU> GraphicalProcessor { get; set; }
        public DbSet<HardDrive> HardDrives { get; set; }
        public DbSet<RAM> Memory { get; set; }
        public DbSet<Laptop> Laptops { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
