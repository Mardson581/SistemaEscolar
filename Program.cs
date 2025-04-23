using SistemaEscolar.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DbEscolar>(options =>
    options.UseSqlite("Data Source=database.db"));
builder.Services.AddSession();

var app = builder.Build();
app.UseStaticFiles();
app.UseSession();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
