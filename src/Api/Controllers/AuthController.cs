using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LoanManagement.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LoanManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly LoanDbContext _context;
    private readonly IConfiguration _config;

    public AuthController(LoanDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email && u.Password == request.Password);

        if (user == null) return Unauthorized(new { message = "Credenciales inválidas" });

        var token = GenerateJwtToken(user);
        
        return Ok(new { 
            token, 
            user = new { user.Id, user.Email, user.Name, user.Role } 
        });
    }

    private string GenerateJwtToken(Domain.Entities.User user)
    {
        var jwtSettings = _config.GetSection("Jwt");
        var key = Encoding.ASCII.GetBytes(jwtSettings["Key"] ?? "SUPER_SECRET_KEY_FOR_LOAN_MANAGEMENT_API_2026");
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

public record LoginRequest(string Email, string Password);
