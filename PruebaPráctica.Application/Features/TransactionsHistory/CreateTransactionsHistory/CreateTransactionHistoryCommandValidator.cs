using FluentValidation;

namespace PruebaPráctica.Application.Features.TransactionsHistory.CreateTransactionsHistory;

public sealed class CreateTransactionHistoryCommandValidator : AbstractValidator<CreateTransactionHistoryCommand>
{
    public CreateTransactionHistoryCommandValidator()
    {
        RuleFor(x => x.WalletId)
            .NotEmpty();
        
        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("Amount must be greater than zero.");
    }
}