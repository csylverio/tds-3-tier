using Microsoft.EntityFrameworkCore;
using MyFinance.Business.Entity;

namespace MyFinance.DataAccess.Data
{
    public class MyFinanceContext : DbContext
    {
        public MyFinanceContext (DbContextOptions<MyFinanceContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Account { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(account => account.Balance)
                    .HasColumnType("numeric(18,2)");
            });
        }
    }
}
