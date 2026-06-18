using Microsoft.Extensions.Configuration;
using MyFinance.Api.Authentication;
using MyFinance.Api.DTOs;

namespace MyFinance.Tests.Api.Authentication;

public class TokenServiceTests
{
    [Fact]
    public void GetToken_ReturnsEmptyString_WhenCredentialsAreInvalid()
    {
        LoginManager.Logins =
        [
            new Login { Id = 1, Username = "Admin", Password = "Abc123", AccessType = AccessType.Privileged }
        ];
        var service = new TokenService(CreateConfiguration());

        var token = service.GetToken(new LoginDTO { Username = "Admin", Password = "wrong-password" });

        Assert.Equal(string.Empty, token);
    }

    [Fact]
    public void GetToken_ReturnsJwt_WhenCredentialsAreValid()
    {
        LoginManager.Logins =
        [
            new Login { Id = 1, Username = "Admin", Password = "Abc123", AccessType = AccessType.Privileged }
        ];
        var service = new TokenService(CreateConfiguration());

        var token = service.GetToken(new LoginDTO { Username = "Admin", Password = "Abc123" });

        Assert.False(string.IsNullOrWhiteSpace(token));
    }

    private static IConfiguration CreateConfiguration()
    {
        return new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["SecretJWT"] = "CarroDePalhacoComElefanteBrancoDentro"
            })
            .Build();
    }
}
