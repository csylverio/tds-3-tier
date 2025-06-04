using Microsoft.EntityFrameworkCore;
using MyFinance.Business.Entity;
using MyFinance.DataAccess.Data;
using MyFinance.DataAccess.Repository;

namespace MyFinance.Tests;

public class AccountRepositoryTests
{
    private async Task<MyFinanceContext> GetInMemoryDbContextAsync()
    {
        var options = new DbContextOptionsBuilder<MyFinanceContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb_{System.Guid.NewGuid()}")
            .Options;

        var context = new MyFinanceContext(options);

        // Adiciona dados iniciais
        context.Account.AddRange(
            new Account { Id = 1, Name = "Conta 1", Balance = 1000 },
            new Account { Id = 2, Name = "Conta 2", Balance = 2000 }
        );
        await context.SaveChangesAsync();

        return context;
    }

    // Exemplo de Testes de Integração para Serviços e Bancos de Dados
    [Fact]
    public async Task GetByIdAsync_DeveRetornarConta_QuandoIdValidoInformado()

    {
        // Arrange
        var context = await GetInMemoryDbContextAsync();
        var repository = new AccountRepository(context);

        // Act
        var account = await repository.GetByIdAsync(1);

        // Assert
        Assert.NotNull(account);
        Assert.Equal("Conta 1", account!.Name);
    }

    [Fact]
    public async Task AddAsync_DeveAdicionarNovaConta_QuandoAccountInformadoComSucesso()
    {
        var context = await GetInMemoryDbContextAsync();
        var repository = new AccountRepository(context);

        var novaConta = new Account { Id = 3, Name = "Conta 3", Balance = 3000 };
        await repository.AddAsync(novaConta);

        var conta = await context.Account.FindAsync(3);
        Assert.NotNull(conta);
        Assert.Equal("Conta 3", conta!.Name);
        Assert.Equal(3000, conta!.Balance);
    }

    [Fact]
    public async Task DeleteAsync_DeveRemoverConta_QuandoIdValidoInformado()
    {
        var context = await GetInMemoryDbContextAsync();
        var repository = new AccountRepository(context);

        await repository.DeleteAsync(1);

        var conta = await context.Account.FindAsync(1);
        Assert.Null(conta);
    }

    [Fact]
    public async Task UpdateAsync_DeveAtualizarConta_QuandoAccountInformadoComSucesso()
    {
        var context = await GetInMemoryDbContextAsync();
        var repository = new AccountRepository(context);

        var conta = await repository.GetByIdAsync(1);
        conta!.Name = "Conta Atualizada";
        await repository.UpdateAsync(conta);

        var contaAtualizada = await context.Account.FindAsync(1);
        Assert.Equal("Conta Atualizada", contaAtualizada!.Name);
    }
}