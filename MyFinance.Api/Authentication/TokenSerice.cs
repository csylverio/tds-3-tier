using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MyFinance.Api.DTOs;

namespace MyFinance.Api.Authentication;

public class TokenSerice(IConfiguration configuration) : ITokenService
{
    private readonly IConfiguration configuration = configuration;

    public string GetToken(LoginDTO loginDTO)
    {
        var hasLogin = LoginManager.Logins.Any(x => loginDTO.Username.Equals(x.Username) && loginDTO.Password.Equals(x.Password));
        if (!hasLogin) {
            return string.Empty;
        }

        Api.Authentication.Login login = LoginManager.Logins.First(x => loginDTO.Username.Equals(x.Username) && loginDTO.Password.Equals(x.Password));
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("SecretJWT") ?? throw new Exception("Secret JWT não configurada!"));
        var tokenProperties = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, login.Username),
                new Claim(ClaimTypes.Role, login.AccessType.ToString()),
                new Claim("ClaimPersonalizada", "Conteúdo Personalizado")
            }),
            Expires = DateTime.UtcNow.AddHours(24),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenProperties);
        return tokenHandler.WriteToken(token);
    }

}
