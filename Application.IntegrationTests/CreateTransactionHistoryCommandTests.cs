using Microsoft.EntityFrameworkCore;
using PruebaPráctica.Application.Common.Exceptions;
using PruebaPráctica.Application.Common.Interfaces;
using PruebaPráctica.Application.Features.TransactionsHistory.CreateTransactionsHistory;
using PruebaPráctica.Domain.Entities;
using PruebaPráctica.Domain.Enums;
using Xunit;

namespace Application.IntegrationTests;

public sealed class CreateTransactionHistoryCommandTests : IClassFixture<IntegrationTestFixture>
{
    private readonly IApplicationDbContext _context;
    private readonly CreateTransactionHistoryCommandHandler _handler;

    public CreateTransactionHistoryCommandTests(IntegrationTestFixture fixture)
    {
        _context = fixture.DbContext;
        _handler = new CreateTransactionHistoryCommandHandler(_context);
    }

    [Fact]
    public async Task Should_Create_Transaction_And_Update_Wallet_Balance()
    {
        // Arrange
        var wallet = new Wallet { Id = 1, Balance = 100 };
        _context.EntitySetFor<Wallet>().Add(wallet);
        await _context.SaveChangesAsync();

        var command = new CreateTransactionHistoryCommand(
            WalletId: wallet.Id,
            Amount: 50,
            Type: OperationType.Debit,
            CreatedAt: DateTime.UtcNow
        );

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        TransactionHistory? transaction = await _context.EntitySetFor<TransactionHistory>().FirstOrDefaultAsync();
        Wallet? updatedWallet = await _context.EntitySetFor<Wallet>().FirstOrDefaultAsync(w => w.Id == wallet.Id);

        Assert.NotNull(transaction);
        Assert.Equal(50, updatedWallet!.Balance);
    }
    
    [Fact]
    public async Task Should_ThrowException_When_WalletDoesNotExist()
    {
        // Arrange
        var command = new CreateTransactionHistoryCommand(
            WalletId: 999,
            Amount: 100,
            Type: OperationType.Debit,
            CreatedAt: DateTime.UtcNow
        );

        // Act & Assert
        var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
            _handler.Handle(command, CancellationToken.None));

        Assert.Contains("Wallet ID 999", exception.Message);
    }
}