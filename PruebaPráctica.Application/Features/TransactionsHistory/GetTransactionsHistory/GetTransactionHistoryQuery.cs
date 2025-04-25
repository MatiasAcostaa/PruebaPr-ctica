using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaPráctica.Application.Common.Interfaces;
using PruebaPráctica.Domain.Entities;
using PruebaPráctica.Domain.Enums;

namespace PruebaPráctica.Application.Features.TransactionsHistory.GetTransactionsHistory;

public sealed class GetTransactionHistoryQuery : IRequest<List<GetTransactionsHistoryDto>>;
public sealed class GetTransactionHistoryQueryFilter : IRequest<List<GetTransactionsHistoryDto>>
{
    public int? WalletId { get; init; }
    public DateTime? CreatedAt { get; init; }
    public OperationType? Type { get; init; }
}

internal sealed class GetTransactionHistoryQueryHandler(IApplicationDbContext dbContext)
    : IRequestHandler<GetTransactionHistoryQuery, List<GetTransactionsHistoryDto>>
{
    public async Task<List<GetTransactionsHistoryDto>> Handle(GetTransactionHistoryQuery request, CancellationToken cancellationToken)
    {
        return await dbContext
            .EntitySetFor<TransactionHistory>()
            .Select(th => GetTransactionsHistoryDto.Map(th))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}

internal sealed class GetTransactionHistoryQueryFilterHandler(IApplicationDbContext dbContext) : IRequestHandler<GetTransactionHistoryQueryFilter, List<GetTransactionsHistoryDto>>
{
    public async Task<List<GetTransactionsHistoryDto>> Handle(GetTransactionHistoryQueryFilter request, CancellationToken cancellationToken)
    {
        IQueryable<TransactionHistory> query = dbContext.EntitySetFor<TransactionHistory>().AsQueryable();

        if (request.WalletId != null)
            query = query.Where(th => th.WalletId == request.WalletId);
        
        if (request.CreatedAt.HasValue)
            query = query.Where(th => th.CreatedAt.Date == request.CreatedAt.Value.Date);
        
        if (request.Type.HasValue)
            query = query.Where(th => th.Type.Value == request.Type.Value);
        
        return await query
            .Select(th => GetTransactionsHistoryDto.Map(th))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}