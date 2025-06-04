using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MyFinance.DataAccess.Data
{
    public class MyFinanceContext : DbContext
    {
        public MyFinanceContext (DbContextOptions<MyFinanceContext> options)
            : base(options)
        {
        }

        public DbSet<MyFinance.Business.Entity.Account> Account { get; set; } = default!;
    }
}
