namespace LoanManagement.Application.DTOs;

public record LoanRequestDto(decimal Amount, int Term);

public record LoanResponseDto(
    Guid Id,
    decimal Amount,
    int Term,
    string Status,
    DateTime RequestDate,
    string UserId,
    string UserName,
    string? RejectionReason);

public record UpdateLoanStatusDto(string Status, string? RejectionReason);
