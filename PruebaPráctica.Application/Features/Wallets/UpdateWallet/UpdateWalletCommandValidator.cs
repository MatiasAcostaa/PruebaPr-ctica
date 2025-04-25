using FluentValidation;

namespace PruebaPráctica.Application.Features.Wallets.UpdateWallet;

public sealed class UpdateWalletCommandValidator : AbstractValidator<UpdateWalletCommand>
{
    public UpdateWalletCommandValidator()
    {
        RuleFor(w => w.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}