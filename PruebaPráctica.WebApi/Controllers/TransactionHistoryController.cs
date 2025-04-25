using MediatR;
using Microsoft.AspNetCore.Mvc;
using PruebaPráctica.Application.Features.TransactionsHistory.CreateTransactionsHistory;
using PruebaPráctica.Application.Features.TransactionsHistory.GetTransactionsHistory;


namespace PruebaPráctica.WebApi.Controllers;

[Route("api/transactionHistory")]
public sealed class TransactionHistoryController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await sender.Send(new GetTransactionHistoryQuery()));
    
    [HttpGet("filter")]
    public async Task<IActionResult> GetFilter([FromQuery] GetTransactionHistoryQueryFilter filter)
    {
        var query = new GetTransactionHistoryQueryFilter()
        {
            WalletId = filter.WalletId,
            CreatedAt = filter.CreatedAt,
            Type = filter.Type
        };
    
        List<GetTransactionsHistoryDto> result = await sender.Send(query);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTransactionHistoryCommand command) => Ok(await sender.Send(command));
}