using Microsoft.EntityFrameworkCore;
using LoanManagement.Domain.Entities;
using LoanManagement.Domain.Interfaces;
using LoanManagement.Infrastructure.Persistence;

namespace LoanManagement.Infrastructure.Repositories;

public class LoanRepository : ILoanRepository
{
    private readonly LoanDbContext _context;

    public LoanRepository(LoanDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Loan>> GetAllAsync()
    {
        return await _context.Loans.OrderByDescending(l => l.RequestDate).ToListAsync();
    }

    public async Task<IEnumerable<Loan>> GetByUserIdAsync(string userId)
    {
        return await _context.Loans
            .Where(l => l.UserId == userId)
            .OrderByDescending(l => l.RequestDate)
            .ToListAsync();
    }

    public async Task<Loan?> GetByIdAsync(Guid id)
    {
        return await _context.Loans.FindAsync(id);
    }

    public async Task AddAsync(Loan loan)
    {
        await _context.Loans.AddAsync(loan);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Loan loan)
    {
        _context.Loans.Update(loan);
        await _context.SaveChangesAsync();
    }
}
