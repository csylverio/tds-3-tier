using Microsoft.AspNetCore.Mvc;
using MyFinance.Business.Service;
using MyFinance.Models;

namespace MyFinance.Controllers;

public class AccountsController : Controller
{
    private readonly IAccountService _accountService;

    public AccountsController(IAccountService accountService)
    {
        _accountService = accountService;
    }   
    
    // GET: Accounts
    public async Task<IActionResult> Index()
    {
        var list = await _accountService.GetListAsync();
        var accounts = list.Select(x => new Account()
        {
            Id = x.Id,
            Name = x.Name,
            Balance = x.Balance,
            DataExecucao = DateTime.Now
        }).ToList();

        return View(accounts);
    }

    // GET: Accounts/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        
        var entity = await _accountService.GetByIdAsync(id.Value);
        if (entity == null)
        {
            return NotFound();
        }

        var account = new Account()
        {
            Id = entity.Id,
            Name = entity.Name,
            Balance = entity.Balance
        };

        return View(account);
    }

    // GET: Accounts/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Accounts/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Balance")] Account account)
    {
        if (ModelState.IsValid)
        {
            await _accountService.AddAsync(account.Name, account.Balance);
            return RedirectToAction(nameof(Index));
        }
        return View(account);
    }

    // GET: Accounts/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        if (!await _accountService.ExistsAsync(id.Value))
        {
            return NotFound();
        }

        var entity = await _accountService.GetByIdAsync(id.Value);
        var account = new Account()
        {
            Id = entity.Id,
            Name = entity.Name,
            Balance = entity.Balance
        };

        return View(account);
    }

    // POST: Accounts/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Balance")] Account account)
    {
        if (id != account.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            if (!await _accountService.ExistsAsync(id))
            {
                return NotFound();
            }

            await _accountService.UpdateAsync(id, account.Name, account.Balance);

            return RedirectToAction(nameof(Index));
        }
        return View(account);
    }

    // GET: Accounts/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _accountService.GetByIdAsync(id.Value);
        if (entity == null)
        {
            return NotFound();
        }

        var account = new Account()
        {
            Id = entity.Id,
            Name = entity.Name,
            Balance = entity.Balance
        };

        return View(account);
    }

    // POST: Accounts/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (!await _accountService.ExistsAsync(id))
        {
            return NotFound();
        }
        await _accountService.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }
}
