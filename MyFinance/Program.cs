using Microsoft.EntityFrameworkCore;
using MyFinance.Business.Repository;
using MyFinance.Business.Service;
using MyFinance.DataAccess.Data;
using MyFinance.DataAccess.Repository;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MyFinanceContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MyFinanceContext") 
    ?? throw new InvalidOperationException("Connection string 'MyFinanceContext' not found.")));

// mapeia classes para Inversão de Dependencia (DI)
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
