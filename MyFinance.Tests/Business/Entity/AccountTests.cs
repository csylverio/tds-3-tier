using MyFinance.Business.Entity;

namespace MyFinance.Tests.Business.Entity;

public class AccountTests
{
    [Fact]
    public void IsBalanceValid_ReturnsFalse_WhenBalanceIsNegative()
    {
        var account = new Account { Name = "Conta", Balance = 0 };

        var result = account.IsBalanceValid(-1);

        Assert.False(result);
    }

    [Fact]
    public void IsBalanceValid_ReturnsTrue_WhenBalanceIsZeroOrPositive()
    {
        var account = new Account { Name = "Conta", Balance = 0 };

        Assert.True(account.IsBalanceValid(0));
        Assert.True(account.IsBalanceValid(10));
    }
}
