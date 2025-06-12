using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MyFinance.DataAccess.Data;

namespace MyFinance.DataAccess;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MyFinanceContext>
{
    public MyFinanceContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MyFinanceContext>();

        // ✅ Ajuste a string de conexão conforme seu ambiente
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=102030");

        return new MyFinanceContext(optionsBuilder.Options);
    }
}

