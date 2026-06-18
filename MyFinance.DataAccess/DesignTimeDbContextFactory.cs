using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MyFinance.DataAccess.Data;

namespace MyFinance.DataAccess;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MyFinanceContext>
{
    public MyFinanceContext CreateDbContext(string[] args)
    {
        var apiSettingsPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../MyFinance.Api"));
        var appSettingsPath = Path.Combine(apiSettingsPath, "appsettings.json");
        using var appSettings = JsonDocument.Parse(File.ReadAllText(appSettingsPath));

        var connectionString = appSettings.RootElement
            .GetProperty("ConnectionStrings")
            .GetProperty("MyFinanceContext")
            .GetString()
            ?? throw new InvalidOperationException("Connection string 'MyFinanceContext' not found.");

        var optionsBuilder = new DbContextOptionsBuilder<MyFinanceContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new MyFinanceContext(optionsBuilder.Options);
    }
}

