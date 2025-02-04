using Microsoft.EntityFrameworkCore;
using MusicCrudAsync.DataAccess.Entity;

namespace MusicCrudAsync.DataAccess
{
    public class MainContext : DbContext
    {
        public DbSet<Music> Music { get; set; }

        // Parametrli konstruktor
        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=WIN-BNO54FDBS2G\\SQLEXPRESS;Database=MusicCRUD;User Id=sa;Password=1;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
