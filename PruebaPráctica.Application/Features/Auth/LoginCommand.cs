using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PruebaPráctica.Application.Common.Exceptions;
using PruebaPráctica.Application.Common.Interfaces;
using PruebaPráctica.Domain.Entities;

namespace PruebaPráctica.Application.Features.Auth;

public record LoginCommand(string Username, string Password) : IRequest<string>;

public class LoginCommandHandler(IApplicationDbContext dbContext, IConfiguration configuration)
    : IRequestHandler<LoginCommand, string>
{
    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        User? user = await dbContext.EntitySetFor<User>().FirstOrDefaultAsync(u => u.Username == request.Username, cancellationToken);
        
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            throw new BadRequestException("Credenciales inválidas");
        }
        
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username!)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expirationMinutes = configuration.GetValue<int>("Jwt:AccessTokenExpiration");
        
        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}