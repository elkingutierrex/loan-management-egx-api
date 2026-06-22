using LoanManagement.Domain.Entities;

namespace LoanManagement.Domain.Interfaces;

public interface ILoanRepository
{
    Task<IEnumerable<Loan>> GetAllAsync();
    Task<IEnumerable<Loan>> GetByUserIdAsync(string userId);
    Task<Loan?> GetByIdAsync(Guid id);
    Task AddAsync(Loan loan);
    Task UpdateAsync(Loan loan);
}
