using System;
using Microsoft.EntityFrameworkCore;
using MyFinance.Business.Entity;
using MyFinance.Business.Repository;
using MyFinance.DataAccess.Data;

namespace MyFinance.DataAccess.Repository;

public class AccountRepository : IAccountRepository
{
    private readonly MyFinanceContext _context;

    public AccountRepository(MyFinanceContext context)
    {
        _context = context;
    }

    public async Task<Account?> GetByIdAsync(int id)
    {
        return await _context.Account.FirstOrDefaultAsync(account => account.Id == id);
    }

    public async Task<List<Account>> GetListAsync()
    {
        return await _context.Account.ToListAsync();
    }

    public async Task AddAsync(Account account)
    {
        _context.Add(account);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Account.FirstOrDefaultAsync(account => account.Id == id);
        if (entity != null)
        {
            _context.Account.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateAsync(Account account)
    {
        _context.Update(account);
        await _context.SaveChangesAsync();
    }
}
