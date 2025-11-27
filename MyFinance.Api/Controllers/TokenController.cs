using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Api.Authentication;
using MyFinance.Api.DTOs;

namespace MyFinance.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController(ITokenService tokenService) : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    public IActionResult Post([FromBody] LoginDTO loginDto)
    {
        var token = tokenService.GetToken(loginDto);
        if (!string.IsNullOrEmpty(token))
        {
            return Ok(token);
        }
        return Unauthorized();
    }
}