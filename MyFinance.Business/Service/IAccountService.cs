using System;
using MyFinance.Business.Entity;

namespace MyFinance.Business.Service;

public interface IAccountService
{
    Task<bool> ExistsAsync(int id);
    Task<Account> GetByIdAsync(int value);
    Task<List<Account>> GetListAsync();
    Task AddAsync(string name, double balance);
    Task UpdateAsync(int id, string name, double balance);
    Task DeleteAsync(int id);

}
