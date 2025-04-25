namespace PruebaPráctica.Domain.Entities;

public sealed class Wallet
{
    public int Id { get; init; }
    public int DocumentId { get; set; }
    public string? Name { get; set; }
    public decimal Balance { get; set; }
    public DateOnly? CreatedAt { get; init; }
    public DateOnly? UpdatedAt { get; set; }

    public List<TransactionHistory> TransactionHistory { get; init; } = [];
}