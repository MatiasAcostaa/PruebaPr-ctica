using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaPráctica.Application.Features.Wallets.CreateWallet;
using PruebaPráctica.Application.Features.Wallets.DeleteWallet;
using PruebaPráctica.Application.Features.Wallets.GetWallets;
using PruebaPráctica.Application.Features.Wallets.UpdateWallet;

namespace PruebaPráctica.WebApi.Controllers;

[Route("api/wallets")]
public sealed class WalletController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await sender.Send(new GetWalletsQuery()));
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateWalletCommand command) => Ok(await sender.Send(command));
    
    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateWalletCommand command)
    {
        command = command with { Id = id };
        await sender.Send(command);
        
        return NoContent();
    }
    
    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await sender.Send(new DeleteWalletCommand(id));

        return NoContent();
    }
}