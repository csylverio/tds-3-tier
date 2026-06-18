using System;
using MyFinance.Business.Entity;

namespace MyFinance.Business.Service;

public interface IAccountService
{
    Task<bool> ExistsAsync(int id);
    Task<Account> GetByIdAsync(int value);
    Task<List<Account>> GetListAsync();
    Task<Account> AddAsync(string name, decimal balance);
    Task UpdateAsync(int id, string name, decimal balance);
    Task DeleteAsync(int id);

}
