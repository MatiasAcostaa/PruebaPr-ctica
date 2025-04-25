using FluentValidation;

namespace PruebaPráctica.Application.Features.Wallets.CreateWallet;

public sealed class CreateWalletCommandValidator : AbstractValidator<CreateWalletCommand>
{
    public CreateWalletCommandValidator()
    {
        RuleFor(w => w.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}