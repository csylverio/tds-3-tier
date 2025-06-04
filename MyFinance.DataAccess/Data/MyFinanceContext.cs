using Microsoft.EntityFrameworkCore;

namespace MyFinance.DataAccess.Data
{
    public class MyFinanceContext : DbContext
    {
        public MyFinanceContext (DbContextOptions<MyFinanceContext> options)
            : base(options)
        {
        }

        public DbSet<Business.Entity.Account> Account { get; set; } = default!;
    }
}
