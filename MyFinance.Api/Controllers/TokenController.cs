using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Api.Authentication;
using MyFinance.Api.DTOs;

namespace MyFinance.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly ITokenService tokenService;

    public TokenController(ITokenService tokenService)
    {
        this.tokenService = tokenService;
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult Post([FromBody] LoginDTO loginDTO)
    {
        var token = tokenService.GetToken(loginDTO);

        if (!string.IsNullOrEmpty(token))
        {
            return Ok(token);
        }

        return Unauthorized();
    }
}