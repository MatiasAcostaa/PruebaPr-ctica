using MediatR;
using Microsoft.EntityFrameworkCore;
using PruebaPráctica.Application.Common.Interfaces;
using PruebaPráctica.Domain.Entities;

namespace PruebaPráctica.Application.Features.Wallets.GetWallets;

public sealed record GetWalletsQuery : IRequest<List<GetWalletsDto>>;

internal sealed class GetWalletsQueryHandler(IApplicationDbContext dbContext) : IRequestHandler<GetWalletsQuery, List<GetWalletsDto>>
{
    public async Task<List<GetWalletsDto>> Handle(GetWalletsQuery request, CancellationToken cancellationToken)
    {
        return await dbContext
            .EntitySetFor<Wallet>()
            .Select(w => GetWalletsDto.Map(w))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}