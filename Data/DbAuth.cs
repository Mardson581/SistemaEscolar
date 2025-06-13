using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaEscolar.Models;

namespace SistemaEscolar.Data;

public class DbAuth : IdentityDbContext
{
    public DbAuth(DbContextOptions<DbAuth> options) : base(options) { }
    
    public DbSet<Gestor> Gestores { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        Gestor admin = new Gestor { UserName = "Admin", Email = "admin@email.com", NormalizedEmail = "ADMIN@EMAIL.COM"};
        admin.PasswordHash = new PasswordHasher<Gestor>().HashPassword(admin, "admin");

        builder.Entity<Gestor>().HasData(admin);
    }
}