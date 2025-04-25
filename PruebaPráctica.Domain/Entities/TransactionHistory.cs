using PruebaPráctica.Domain.Enums;

namespace PruebaPráctica.Domain.Entities;

public sealed class TransactionHistory
{
    public int Id { get; init; }
    public int WalletId { get; init; }
    public decimal Amount { get; init; }
    public OperationType? Type { get; init; }
    public DateTime CreatedAt { get; init; }
    
    public Wallet? Wallet { get; init; }
}