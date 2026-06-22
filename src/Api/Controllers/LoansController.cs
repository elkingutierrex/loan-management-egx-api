using System.Security.Claims;
using LoanManagement.Application.DTOs;
using LoanManagement.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoanManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LoansController : ControllerBase
{
    private readonly ILoanService _loanService;

    public LoansController(ILoanService loanService)
    {
        _loanService = loanService;
    }

    [HttpGet]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<IEnumerable<LoanResponseDto>>> GetAll()
    {
        var loans = await _loanService.GetAllLoansAsync();
        return Ok(loans);
    }

    [HttpGet("my-loans")]
    public async Task<ActionResult<IEnumerable<LoanResponseDto>>> GetMyLoans()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var loans = await _loanService.GetUserLoansAsync(userId);
        return Ok(loans);
    }

    [HttpPost]
    public async Task<ActionResult<LoanResponseDto>> RequestLoan([FromBody] LoanRequestDto request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userName = User.FindFirst(ClaimTypes.Name)?.Value ?? "User";
        
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var response = await _loanService.RequestLoanAsync(userId, userName, request);
        return CreatedAtAction(nameof(GetMyLoans), response);
    }

    [HttpPatch("{id}/status")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateLoanStatusDto updateDto)
    {
        try
        {
            if (updateDto.Status.Equals("Aprobado", StringComparison.OrdinalIgnoreCase))
            {
                await _loanService.ApproveLoanAsync(id);
            }
            else if (updateDto.Status.Equals("Rechazado", StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrEmpty(updateDto.RejectionReason))
                    return BadRequest("Rejection reason is required.");

                await _loanService.RejectLoanAsync(id, updateDto.RejectionReason);
            }
            else
            {
                return BadRequest("Invalid status.");
            }

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
