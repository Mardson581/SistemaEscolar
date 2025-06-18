using SistemaEscolar.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SistemaEscolar.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DbEscolar>(options =>
    options.UseSqlite("Data Source=database.db"));

builder.Services.AddDbContext<DbAuth>(options =>
    options.UseSqlite("Data Source=database.db"));

builder.Services.AddIdentity<Gestor, IdentityRole>()
    .AddEntityFrameworkStores<DbAuth>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options => {
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.User.RequireUniqueEmail = true;
});

builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/login";
    options.LogoutPath = "/logout";
});

builder.Services.AddSession();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseSession();

app.UseExceptionHandler("/erro");
//app.UseStatusCodePagesWithReExecute("/erro");

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
