using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Xunit;
using PruebaPráctica.Application.Features.TransactionsHistory.CreateTransactionsHistory;
using PruebaPráctica.Application.Common.Exceptions;
using PruebaPráctica.Domain.Entities;
using PruebaPráctica.Domain.Enums;
using PruebaPráctica.Infrastructure.Persistence;

namespace Common.UnitTests;

public sealed class CreateTransactionHistoryCommandHandlerTests
{
    private static ApplicationDbContext CreateInMemoryContext()
    {
        DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task Should_ThrowException_When_WalletDoesNotExist()
    {
        // Arrange
        ApplicationDbContext context = CreateInMemoryContext();

        var handler = new CreateTransactionHistoryCommandHandler(context);

        var command = new CreateTransactionHistoryCommand(
            WalletId: 999,
            Amount: 100,
            Type: OperationType.Debit,
            CreatedAt: DateTime.UtcNow
        );

        // Act & Assert
        var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(command, CancellationToken.None));

        Assert.Contains("Wallet ID 999", exception.Message);
    }
    
    [Fact]
    public async Task Should_ThrowException_When_InsufficientBalance()
    {
        // Arrange
        DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        await using var context = new ApplicationDbContext(options);

        context.Add(new Wallet { Id = 1, Balance = 50 });
        await context.SaveChangesAsync();

        var handler = new CreateTransactionHistoryCommandHandler(context);

        var command = new CreateTransactionHistoryCommand(
            WalletId: 1,
            Amount: 100,
            Type: OperationType.Debit,
            CreatedAt: DateTime.UtcNow
        );

        // Act & Assert
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            handler.Handle(command, CancellationToken.None));

        Assert.Equal("Insufficient balance to perform the debit.", ex.Message);
    }
    
    [Fact]
    public void Should_HaveValidationError_When_AmountIsZero()
    {
        var validator = new CreateTransactionHistoryCommandValidator();

        var command = new CreateTransactionHistoryCommand(
            WalletId: 1,
            Amount: 0,
            Type: OperationType.Credit,
            CreatedAt: DateTime.UtcNow
        );

        ValidationResult? result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Amount");
    }
}