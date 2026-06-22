namespace LoanManagement.Domain.Entities;

public class User
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty; // Simplificado para la prueba
    public string Role { get; set; } = string.Empty; // ADMIN o USER
}
