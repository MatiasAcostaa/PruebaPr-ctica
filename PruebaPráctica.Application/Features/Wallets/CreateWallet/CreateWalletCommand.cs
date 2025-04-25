using MediatR;
using Microsoft.EntityFrameworkCore;
using PruebaPráctica.Application.Common.Exceptions;
using PruebaPráctica.Application.Common.Interfaces;
using PruebaPráctica.Domain.Entities;

namespace PruebaPráctica.Application.Features.Wallets.CreateWallet;

public sealed record CreateWalletCommand(
    int DocumentId,
    string Name,
    decimal Balance,
    DateOnly CreatedAt
) : IRequest<int>;

internal sealed class CreateWalletCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<CreateWalletCommand, int>
{
    public async Task<int> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
    {
        var wallet = new Wallet
        {
            DocumentId = request.DocumentId,
            Name = request.Name,
            Balance = request.Balance,
            CreatedAt = request.CreatedAt
        };
        
        bool walletExists = await dbContext.EntitySetFor<Wallet>().AnyAsync(w => w.DocumentId == request.DocumentId, cancellationToken);

        if (walletExists)
        {
            throw new ResourceAlreadyExistsException();
        }

        dbContext.EntitySetFor<Wallet>().Add(wallet);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return wallet.Id;
    }
}