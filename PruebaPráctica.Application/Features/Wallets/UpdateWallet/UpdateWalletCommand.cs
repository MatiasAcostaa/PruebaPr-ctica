using System.Text.Json.Serialization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PruebaPráctica.Application.Common.Exceptions;
using PruebaPráctica.Application.Common.Interfaces;
using PruebaPráctica.Domain.Entities;

namespace PruebaPráctica.Application.Features.Wallets.UpdateWallet;

public sealed record UpdateWalletCommand(
    int DocumentId,
    string? Name,
    decimal Balance,
    DateOnly UpdatedAt
) : IRequest<Unit>
{
    [JsonIgnore]
    public int Id { get; init; }
}

internal sealed class UpdateWalletCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<UpdateWalletCommand, Unit>
{
    public async Task<Unit> Handle(UpdateWalletCommand request, CancellationToken cancellationToken)
    {
        Wallet? wallet = await dbContext.EntitySetFor<Wallet>().FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken);

        if (wallet is null)
        {
            throw new NotFoundException(nameof(wallet), request.Id);
        }
        
        wallet.DocumentId = request.DocumentId;
        wallet.Name = request.Name;
        wallet.Balance = request.Balance;
        wallet.UpdatedAt = request.UpdatedAt;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}