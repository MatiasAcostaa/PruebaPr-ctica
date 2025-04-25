using MediatR;
using Microsoft.EntityFrameworkCore;
using PruebaPráctica.Application.Common.Exceptions;
using PruebaPráctica.Application.Common.Interfaces;
using PruebaPráctica.Domain.Entities;

namespace PruebaPráctica.Application.Features.Wallets.DeleteWallet;

public record DeleteWalletCommand(int Id) : IRequest<Unit>;

internal sealed class DeleteWalletCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<DeleteWalletCommand, Unit>
{
    public async Task<Unit> Handle(DeleteWalletCommand request, CancellationToken cancellationToken)
    {
        Wallet? wallet = await dbContext.EntitySetFor<Wallet>().FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken);
        
        if (wallet is null)
            throw new NotFoundException(nameof(wallet), request.Id);
        
        dbContext.EntitySetFor<Wallet>().Remove(wallet!);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}