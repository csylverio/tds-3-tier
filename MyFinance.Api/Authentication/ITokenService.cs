using System;
using MyFinance.Api.DTOs;

namespace MyFinance.Api.Authentication;

public interface ITokenService
{
    public string GetToken(LoginDTO loginDTO);
}
