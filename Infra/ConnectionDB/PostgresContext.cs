
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infra.ConnectionDB;

public partial class PostgresContext : DbContext
{
    public PostgresContext(DbContextOptions<PostgresContext> options) : base(options) { }

    public virtual DbSet<User> user { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .ToTable("Users", "public")
            .HasKey(e => e.Id);

        base.OnModelCreating(modelBuilder);
    }
}
