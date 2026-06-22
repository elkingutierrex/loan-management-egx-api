using LoanManagement.Application.DTOs;
using LoanManagement.Application.Interfaces;
using LoanManagement.Domain.Entities;
using LoanManagement.Domain.Interfaces;

namespace LoanManagement.Application.Services;

public class LoanService : ILoanService
{
    private readonly ILoanRepository _loanRepository;

    public LoanService(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }

    public async Task<IEnumerable<LoanResponseDto>> GetAllLoansAsync()
    {
        var loans = await _loanRepository.GetAllAsync();
        return loans.Select(MapToDto);
    }

    public async Task<IEnumerable<LoanResponseDto>> GetUserLoansAsync(string userId)
    {
        var loans = await _loanRepository.GetByUserIdAsync(userId);
        return loans.Select(MapToDto);
    }

    public async Task<LoanResponseDto> RequestLoanAsync(string userId, string userName, LoanRequestDto request)
    {
        var loan = new Loan
        {
            Id = Guid.NewGuid(),
            Amount = request.Amount,
            Term = request.Term,
            Status = LoanStatus.Pendiente,
            RequestDate = DateTime.UtcNow,
            UserId = userId,
            UserName = userName
        };

        await _loanRepository.AddAsync(loan);
        return MapToDto(loan);
    }

    public async Task ApproveLoanAsync(Guid loanId)
    {
        var loan = await _loanRepository.GetByIdAsync(loanId);
        if (loan == null || loan.Status != LoanStatus.Pendiente)
            throw new InvalidOperationException("Loan must be in Pendiente status to be approved.");

        loan.Status = LoanStatus.Aprobado;
        await _loanRepository.UpdateAsync(loan);
    }

    public async Task RejectLoanAsync(Guid loanId, string reason)
    {
        var loan = await _loanRepository.GetByIdAsync(loanId);
        if (loan == null || loan.Status != LoanStatus.Pendiente)
            throw new InvalidOperationException("Loan must be in Pendiente status to be rejected.");

        loan.Status = LoanStatus.Rechazado;
        loan.RejectionReason = reason;
        await _loanRepository.UpdateAsync(loan);
    }

    private static LoanResponseDto MapToDto(Loan loan) =>
        new LoanResponseDto(
            loan.Id,
            loan.Amount,
            loan.Term,
            loan.Status.ToString(),
            loan.RequestDate,
            loan.UserId,
            loan.UserName,
            loan.RejectionReason);
}
