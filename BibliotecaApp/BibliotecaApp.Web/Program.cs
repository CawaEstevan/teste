using Microsoft.EntityFrameworkCore;
using BibliotecaApp.Infrastructure.Data;
using BibliotecaApp.Domain.Interfaces;
using BibliotecaApp.Infrastructure.Repositories;
using BibliotecaApp.Application.Interfaces;
using BibliotecaApp.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurar DbContext com SQLite (funciona em Linux/Windows/Mac)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("BibliotecaApp.Infrastructure")
    ));

// Injeção de Dependências (DI) - Inversão de Controle (IoC)
builder.Services.AddScoped<ILivroRepository, LivroRepository>();
builder.Services.AddScoped<ILivroService, LivroService>();

// Adicionar serviços MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline
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
    pattern: "{controller=Livros}/{action=Index}/{id?}");

app.Run();