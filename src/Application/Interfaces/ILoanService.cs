using LoanManagement.Application.DTOs;

namespace LoanManagement.Application.Interfaces;

public interface ILoanService
{
    Task<IEnumerable<LoanResponseDto>> GetAllLoansAsync();
    Task<IEnumerable<LoanResponseDto>> GetUserLoansAsync(string userId);
    Task<LoanResponseDto> RequestLoanAsync(string userId, string userName, LoanRequestDto request);
    Task ApproveLoanAsync(Guid loanId);
    Task RejectLoanAsync(Guid loanId, string reason);
}
