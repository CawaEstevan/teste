using LojaCamisasGames.Application.Interfaces;
using LojaCamisasGames.Application.Services;
using LojaCamisasGames.Domain.Interfaces;
using LojaCamisasGames.Infrastructure.Data;
using LojaCamisasGames.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração do DbContext com SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("LojaCamisasGames.Infrastructure")
    ));

// Injeção de Dependências (DI) - Inversão de Controle (IoC)
// Repositories
builder.Services.AddScoped<ICamisaGameRepository, CamisaGameRepository>();

// Services
builder.Services.AddScoped<ICamisaGameService, CamisaGameService>();

// Adiciona serviços MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuração do pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=CamisasGames}/{action=Index}/{id?}");

app.Run();