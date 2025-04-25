using PruebaPráctica.Domain.Entities;

namespace PruebaPráctica.Application.Features.Wallets.GetWallets;

public sealed record GetWalletsDto(
    int Id,
    int DocumentId,
    string? Name,
    decimal Balance,
    DateOnly? CreatedAt,
    DateOnly? UpdatedAt
)
{
    internal static GetWalletsDto Map(Wallet wallet)
    {
        return new GetWalletsDto(
            Id: wallet.Id, 
            DocumentId: wallet.DocumentId, 
            Name: wallet.Name, 
            Balance: wallet.Balance, 
            CreatedAt: wallet.CreatedAt, 
            UpdatedAt: wallet.UpdatedAt
        );
    }
}

