using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Api.Authentication;
using MyFinance.Api.DTOs;
using MyFinance.Business.Service;

namespace MyFinance.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AccountController(IAccountService accountService) : ControllerBase
{
    private readonly IAccountService _accountService = accountService;

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetList()
    {
        // Deveria receber um PaginationFilter para parametro
        // utilizar _context.Accounts.AsQueryable(); no repositorio
        var list = await _accountService.GetListAsync();
        var accounts = list.Select(x => new AccountDTO()
        {
            Id = x.Id,
            Name = x.Name,
            Balance = x.Balance
        }).ToList();

        return Ok(accounts);
    }

    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var account = await _accountService.GetByIdAsync(id);
            return Ok(new AccountDTO(account.Id, account.Name, account.Balance));
        }
        catch (NotFoundException ex)
        {
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Conta não encontrada!",
                Detail = ex.Message
            });
        }

    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateAccountDTO accountDTO)
    {
        var createdAccount = await _accountService.AddAsync(accountDTO.Name, accountDTO.Balance);
        return CreatedAtAction(nameof(GetById), new { id = createdAccount.Id }, createdAccount);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Edit(int id, [FromBody] AccountDTO accountDTO)
    {
        if (!await _accountService.ExistsAsync(id))
            return NotFound();

        await _accountService.UpdateAsync(id, accountDTO.Name, accountDTO.Balance);
        return Ok(accountDTO);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Privileged")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!await _accountService.ExistsAsync(id))
            return NotFound();

        await _accountService.DeleteAsync(id);
        return NoContent();
    }
}
/*
Pontos de melhorias
1. Falta de tratamento centralizado de erros
2. Documentação Swagger incompleta
3. Validações redundantes nos controllers
4. Lógica de mapeamento manual (poderia usar AutoMapper) para conversões de entidades
*/