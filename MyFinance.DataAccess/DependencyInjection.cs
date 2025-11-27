using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyFinance.Business.Repository;
using MyFinance.DataAccess.Data;
using MyFinance.DataAccess.Repository;

namespace MyFinance.DataAccess;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MyFinanceContext")
            ?? throw new InvalidOperationException("Connection string 'MyFinanceContext' not found.");

        services.AddDbContext<MyFinanceContext>(options =>
            options.UseNpgsql(connectionString));

        // Registra os repositórios da camada de dados
        services.AddScoped<IAccountRepository, AccountRepository>();

        return services;
    }
}

