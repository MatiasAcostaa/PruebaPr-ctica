using MediatR;
using Microsoft.EntityFrameworkCore;
using PruebaPráctica.Application.Common.Exceptions;
using PruebaPráctica.Application.Common.Interfaces;
using PruebaPráctica.Domain.Entities;
using PruebaPráctica.Domain.Enums;

namespace PruebaPráctica.Application.Features.TransactionsHistory.CreateTransactionsHistory;

public sealed record CreateTransactionHistoryCommand(
    int WalletId,
    decimal Amount,
    OperationType Type,
    DateTime CreatedAt
) : IRequest<int>;

public sealed class CreateTransactionHistoryCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<CreateTransactionHistoryCommand, int>
{
    public async Task<int> Handle(CreateTransactionHistoryCommand request, CancellationToken cancellationToken)
    {
        Wallet? wallet = await dbContext.EntitySetFor<Wallet>().FirstOrDefaultAsync(w => w.Id == request.WalletId, cancellationToken);
        
        if (wallet is null)
        {
            throw new NotFoundException($"Wallet ID {request.WalletId} does not exist.");
        }
        
        if (request.Type == OperationType.Debit && wallet.Balance < request.Amount)
        {
            throw new InvalidOperationException("Insufficient balance to perform the debit.");
        }
        
        wallet.Balance += request.Type == OperationType.Credit
            ? request.Amount
            : -request.Amount;
        
        var transaction = new TransactionHistory
        {
            WalletId = request.WalletId,
            Amount = request.Amount,
            Type = request.Type,
            CreatedAt = request.CreatedAt
        };
        
        dbContext.EntitySetFor<TransactionHistory>().Add(transaction);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return transaction.Id;
    }
}