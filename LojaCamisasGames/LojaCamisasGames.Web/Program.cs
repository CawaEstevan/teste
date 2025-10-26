using LojaCamisasGames.Application.Interfaces;
using LojaCamisasGames.Application.Services;
using LojaCamisasGames.Domain.Interfaces;
using LojaCamisasGames.Infrastructure.Data;
using LojaCamisasGames.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração do DbContext com SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Injeção de Dependências - Repositories
builder.Services.AddScoped<ICamisaGameRepository, CamisaGameRepository>();

// Injeção de Dependências - Services
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