using Microsoft.EntityFrameworkCore;

namespace SignalR.CovidAPI.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Covid> Covids { get; set; }
        public DbSet<CovidPivotTableDTO> CovidPivotTableDTOs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CovidPivotTableDTO>().HasNoKey();
        }
    }
}
