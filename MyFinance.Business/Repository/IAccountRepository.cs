using System;
using MyFinance.Business.Entity;

namespace MyFinance.Business.Repository;

public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(int id);
    Task<List<Account>> GetListAsync();
    Task AddAsync(Account account);
    Task UpdateAsync(Account entity);
    Task DeleteAsync(int id);
}
