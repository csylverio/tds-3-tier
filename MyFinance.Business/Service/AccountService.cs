using System;
using MyFinance.Business.Entity;
using MyFinance.Business.Repository;

namespace MyFinance.Business.Service;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    public async Task<bool> ExistsAsync(int id)
    {
        Account? account = await _accountRepository.GetByIdAsync(id);
        return account != null;
    }

    public async Task<Account> GetByIdAsync(int value)
    {
        Account? account = await _accountRepository.GetByIdAsync(value);
        return account ?? throw new NotFoundException("Conta inválida!");
    }

    public Task<List<Account>> GetListAsync()
    {
        List<Account> accounts = _accountRepository.GetListAsync().Result;
        return Task.FromResult(accounts);
    }

    public async Task<Account> AddAsync(string name, double balance)
    {
        Account account = await _accountRepository.AddAsync(new Account
        {
            Name = name,
            Balance = balance
        });

        return account;
    }

    public async Task UpdateAsync(int id, string name, double balance)
    {
        Account entity = await GetByIdAsync(id);
        entity.Name = name;
        entity.Balance = balance;
        await _accountRepository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _accountRepository.DeleteAsync(id);
    }
}
