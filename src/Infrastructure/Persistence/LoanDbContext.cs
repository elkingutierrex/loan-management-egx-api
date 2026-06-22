using Microsoft.EntityFrameworkCore;
using LoanManagement.Domain.Entities;

namespace LoanManagement.Infrastructure.Persistence;

public class LoanDbContext : DbContext
{
    public LoanDbContext(DbContextOptions<LoanDbContext> options) : base(options) { }

    public DbSet<Loan> Loans { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Email).IsUnique();
        });

        // Seed Users
        modelBuilder.Entity<User>().HasData(
            new User { Id = "admin-id", Email = "admin@test.com", Password = "123", Name = "Admin System", Role = "ADMIN" },
            new User { Id = "user-id", Email = "usuario@test.com", Password = "123", Name = "Test User", Role = "USER" },
            new User { Id = "admin-old", Email = "admin@bank.com", Password = "admin123", Name = "Admin Legacy", Role = "ADMIN" },
            new User { Id = "user-old", Email = "user@bank.com", Password = "user123", Name = "User Legacy", Role = "USER" }
        );
        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Amount).HasPrecision(18, 2);
            entity.Property(e => e.UserId).IsRequired();
            entity.Property(e => e.UserName).IsRequired();
            entity.Property(e => e.Status).HasConversion<string>();
        });
    }
}
