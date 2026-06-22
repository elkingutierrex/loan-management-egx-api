namespace LoanManagement.Domain.Entities;

public enum LoanStatus
{
    Pendiente,
    Aprobado,
    Rechazado
}

public class Loan
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public int Term { get; set; }
    public LoanStatus Status { get; set; } = LoanStatus.Pendiente;
    public DateTime RequestDate { get; set; } = DateTime.UtcNow;
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string? RejectionReason { get; set; }
}
