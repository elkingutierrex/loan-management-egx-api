using Microsoft.EntityFrameworkCore;
using LoanManagement.Domain.Entities;

namespace LoanManagement.Infrastructure.Persistence;

public class LoanDbContext : DbContext
{
    public LoanDbContext(DbContextOptions<LoanDbContext> options) : base(options) { }

    public DbSet<Loan> Loans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
