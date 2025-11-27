using System;
using MyFinance.Business.Entity;
using MyFinance.Business.Repository;

namespace MyFinance.Business.Service;

public class AccountService(IAccountRepository accountRepository) : IAccountService
{
    public async Task<bool> ExistsAsync(int id)
    {
        var account = await accountRepository.GetByIdAsync(id);
        return account != null;
    }

    public async Task<Account> GetByIdAsync(int value)
    {
        var account = await accountRepository.GetByIdAsync(value);
        return account ?? throw new Exception("Conta não encontrada!");
    }

    public async Task<List<Account>> GetListAsync()
    {
        return await accountRepository.GetListAsync();
    }

    public async Task<Account> AddAsync(string name, double balance)
    {
        var account = await accountRepository.AddAsync(new Account
        {
            Name = name,
            Balance = balance
        });
        return account;
    }

    public async Task UpdateAsync(int id, string name, double balance)
    {
        var entity = await GetByIdAsync(id);
        entity.Name = name.StartsWith("TTT") ? $"Teste {name}" : name;
        entity.Balance = balance;
        await accountRepository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await accountRepository.DeleteAsync(id);
    }
}
