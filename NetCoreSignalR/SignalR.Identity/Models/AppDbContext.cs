namespace SignalR.Identity.Models;

public class AppDbContext(DbContextOptions<AppDbContext> options) 
    : IdentityDbContext<IdentityUser, IdentityRole, string>(options)
{
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=SignalRIdentityDb;User=sa;Password=Password123;TrustServerCertificate=true");

        return new AppDbContext(optionsBuilder.Options);
    }
}