using MediatR;
using Microsoft.AspNetCore.Mvc;
using PruebaPráctica.Application.Features.Auth;

namespace PruebaPráctica.WebApi.Controllers;

[Route("api/auth")]
public class AuthController(ISender sender) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command) => Ok(await sender.Send(command));
}