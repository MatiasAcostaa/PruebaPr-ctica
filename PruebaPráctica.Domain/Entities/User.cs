namespace PruebaPráctica.Domain.Entities;

public sealed class User
{
    public int Id { get; init; }
    public string? Username { get; init; } = string.Empty;
    public string? PasswordHash { get; init; } = string.Empty;
}