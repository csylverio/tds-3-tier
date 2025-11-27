using System;
using Moq;
using MyFinance.Business.Entity;
using MyFinance.Business.Repository;
using MyFinance.Business.Service;

namespace MyFinance.Tests.Bussiness.Service;

public class AccountServiceTests
{
    private readonly Mock<IAccountRepository> _repositoryMock;
    private readonly AccountService _service;

    public AccountServiceTests()
    {
        _repositoryMock = new Mock<IAccountRepository>();
        _service = new AccountService(_repositoryMock.Object);
    }

    [Fact]
    public async Task ExistsAsync_ReturnsTrue_WhenAccountExists()
    {
        _repositoryMock.Setup(r => r.GetByIdAsync(1))
                       .ReturnsAsync(new Account { Id = 1, Name = "Conta 1", Balance = 1000 });

        var result = await _service.ExistsAsync(1);

        Assert.True(result);
    }

    [Fact]
    public async Task ExistsAsync_ReturnsFalse_WhenAccountDoesNotExist()
    {
        _repositoryMock.Setup(r => r.GetByIdAsync(99))
                       .ReturnsAsync((Account?)null);

        var result = await _service.ExistsAsync(99);

        Assert.False(result);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsAccount_WhenExists()
    {
        var expected = new Account { Id = 1, Name = "Test", Balance = 100 };

        _repositoryMock.Setup(r => r.GetByIdAsync(1))
                       .ReturnsAsync(expected);

        var result = await _service.GetByIdAsync(1);

        Assert.Equal(expected.Id, result.Id);
        Assert.Equal(expected.Name, result.Name);
        Assert.Equal(expected.Balance, result.Balance);
    }

    [Fact]
    public async Task GetByIdAsync_ThrowsException_WhenNotFound()
    {
        _repositoryMock.Setup(r => r.GetByIdAsync(2))
                       .ReturnsAsync((Account?)null);

        var ex = await Assert.ThrowsAsync<Exception>(() => _service.GetByIdAsync(2));
        Assert.Equal("Conta não encontrada!", ex.Message);
    }

    [Fact]
    public async Task AddAsync_CallsRepositoryWithCorrectData()
    {
        var name = "Nova Conta";
        var balance = 500.0;

        await _service.AddAsync(name, balance);

        _repositoryMock.Verify(r => r.AddAsync(It.Is<Account>(a =>
            a.Name == name && a.Balance == balance)), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ChangesAccountDataAndCallsUpdate()
    {
        var id = 1;
        var original = new Account { Id = id, Name = "Antiga", Balance = 100 };
        _repositoryMock.Setup(r => r.GetByIdAsync(id))
                       .ReturnsAsync(original);

        await _service.UpdateAsync(id, "TTT Antiga", 999);

        _repositoryMock.Verify(r => r.UpdateAsync(It.Is<Account>(a =>
            a.Id == id && a.Name == "Teste TTT Antiga" && a.Balance == 999)), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_CallsRepositoryWithCorrectId()
    {
        var id = 42;

        await _service.DeleteAsync(id);

        _repositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
    }

    [Fact]
    public async Task GetListAsync_ReturnsAllAccounts()
    {
        var list = new List<Account>
            {
                new Account { Id = 1 , Name = "Conta 1", Balance = 1000 },
                new Account { Id = 2 , Name = "Conta 2", Balance = 2000 }
            };

        _repositoryMock.Setup(r => r.GetListAsync()).ReturnsAsync(list);

        var result = await _service.GetListAsync();

        Assert.Equal(2, result.Count);
    }
}