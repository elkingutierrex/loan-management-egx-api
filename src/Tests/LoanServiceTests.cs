using LoanManagement.Application.DTOs;
using LoanManagement.Application.Services;
using LoanManagement.Domain.Entities;
using LoanManagement.Domain.Interfaces;
using Moq;
using Xunit;

namespace LoanManagement.Tests;

public class LoanServiceTests
{
    private readonly Mock<ILoanRepository> _loanRepositoryMock;
    private readonly LoanService _loanService;

    public LoanServiceTests()
    {
        _loanRepositoryMock = new Mock<ILoanRepository>();
        _loanService = new LoanService(_loanRepositoryMock.Object);
    }

    [Fact]
    public async Task RequestLoanAsync_ShouldCreateLoan_WhenValidRequest()
    {
        // Arrange
        var userId = "user123";
        var userName = "Test User";
        var request = new LoanRequestDto(5000, 12);
        
        _loanRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Loan>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _loanService.RequestLoanAsync(userId, userName, request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(5000, result.Amount);
        Assert.Equal(12, result.Term);
        Assert.Equal("Pendiente", result.Status);
        _loanRepositoryMock.Verify(r => r.AddAsync(It.Is<Loan>(l => 
            l.Amount == 5000 && l.UserId == userId)), Times.Once);
    }

    [Fact]
    public async Task ApproveLoanAsync_ShouldUpdateStatus_WhenLoanIsPending()
    {
        // Arrange
        var loanId = Guid.NewGuid();
        var loan = new Loan { Id = loanId, Status = LoanStatus.Pendiente };
        
        _loanRepositoryMock.Setup(r => r.GetByIdAsync(loanId))
            .ReturnsAsync(loan);

        // Act
        await _loanService.ApproveLoanAsync(loanId);

        // Assert
        Assert.Equal(LoanStatus.Aprobado, loan.Status);
        _loanRepositoryMock.Verify(r => r.UpdateAsync(loan), Times.Once);
    }

    [Fact]
    public async Task ApproveLoanAsync_ShouldThrowException_WhenLoanIsNotPending()
    {
        // Arrange
        var loanId = Guid.NewGuid();
        var loan = new Loan { Id = loanId, Status = LoanStatus.Aprobado };
        
        _loanRepositoryMock.Setup(r => r.GetByIdAsync(loanId))
            .ReturnsAsync(loan);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => 
            _loanService.ApproveLoanAsync(loanId));
    }

    [Fact]
    public async Task RejectLoanAsync_ShouldUpdateStatusAndReason_WhenLoanIsPending()
    {
        // Arrange
        var loanId = Guid.NewGuid();
        var loan = new Loan { Id = loanId, Status = LoanStatus.Pendiente };
        var reason = "Mala calificación";
        
        _loanRepositoryMock.Setup(r => r.GetByIdAsync(loanId))
            .ReturnsAsync(loan);

        // Act
        await _loanService.RejectLoanAsync(loanId, reason);

        // Assert
        Assert.Equal(LoanStatus.Rechazado, loan.Status);
        Assert.Equal(reason, loan.RejectionReason);
        _loanRepositoryMock.Verify(r => r.UpdateAsync(loan), Times.Once);
    }
}
