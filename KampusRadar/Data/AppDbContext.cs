using KampusRadar.Models;
using Microsoft.EntityFrameworkCore;

namespace KampusRadar.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // Svaki DbSet = jedna tablica u bazi
    public DbSet<Dogadaj> Dogadaji { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ZAMKA: SQLite nema pravi decimal tip. EF Core ga sprema kao TEXT pa ne može
        // uspoređivati (<, >) ni sortirati po cijeni u bazi. Rješenje: spremi kao REAL.
        modelBuilder.Entity<Dogadaj>()
            .Property(d => d.Cijena)
            .HasConversion<double>();
    }
}
