using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Api.Authentication;
using MyFinance.Api.DTOs;
using MyFinance.Business.Service;

namespace MyFinance.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AccountController(IAccountService accountService) : ControllerBase
{
    private readonly Guid _guid = Guid.NewGuid();
    private readonly IAccountService _accountService = accountService;

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetList()
    {
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
    public async Task<IActionResult> GetById(int id)
    {
        if (id <= 0)
            return BadRequest("Id deve ser maior que 0.");
        if (!await _accountService.ExistsAsync(id))
            return NotFound();

        var entity = await _accountService.GetByIdAsync(id);
        return Ok(new AccountDTO()
        {
            Id = entity.Id,
            Name = entity.Name,
            Balance = entity.Balance
        });
    }

    [HttpPost]
    [Authorize]
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
