using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyFinance.Models;

namespace MyFinance.Data
{
    public class MyFinanceContext : DbContext
    {
        public MyFinanceContext (DbContextOptions<MyFinanceContext> options)
            : base(options)
        {
        }

        public DbSet<MyFinance.Models.Account> Account { get; set; } = default!;
    }
}
