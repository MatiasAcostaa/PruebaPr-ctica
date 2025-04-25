using PruebaPráctica.Domain.Entities;
using PruebaPráctica.Domain.Enums;

namespace PruebaPráctica.Application.Features.TransactionsHistory.GetTransactionsHistory;

public sealed record GetTransactionsHistoryDto (
    int Id,
    int WalletId,
    decimal Amount,
    OperationType? Type,
    DateTime? CreatedAt
)
{
    internal static GetTransactionsHistoryDto Map(TransactionHistory transactionHistory)
    {
        return new GetTransactionsHistoryDto(
            Id: transactionHistory.Id, 
            WalletId: transactionHistory.WalletId, 
            Amount: transactionHistory.Amount, 
            Type: transactionHistory.Type, 
            CreatedAt: transactionHistory.CreatedAt
        );
    }
}