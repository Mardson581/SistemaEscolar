using Microsoft.EntityFrameworkCore;
using SistemaEscolar.Models;

namespace SistemaEscolar.Data;

public class DbEscolar : DbContext
{
    public DbEscolar(DbContextOptions options) : base(options) { }

    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Professor> Professores { get; set; }
    public DbSet<Secretario> Secretarios { get; set; }
    public DbSet<Diretor> Diretores { get; set;}
    public DbSet<Escola> Escolas { get; set; }
    public DbSet<Turma> Turmas { get; set; }
    public DbSet<Municipio> Municipios{ get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>()
            .HasDiscriminator<string>("TipoUsuario")
            .HasValue<Diretor>("Diretor")
            .HasValue<Secretario>("Secretario")
            .HasValue<Professor>("Professor");

        modelBuilder.Entity<Turma>()
            .HasMany(t => t.Professores)
            .WithMany(p => p.Turmas);

        modelBuilder.Entity<Turma>()
            .HasMany(t => t.Alunos)
            .WithOne(a => a.Turma)
            .HasForeignKey(a => a.IdTurma);

        modelBuilder.Entity<Escola>()
            .HasOne(e => e.Secretario)
            .WithMany(s => s.Escolas)
            .HasForeignKey(e => e.SecretarioId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Diretor>()
            .HasOne(d => d.Escola)
            .WithOne(e => e.Diretor)
            .HasForeignKey<Escola>(e => e.DiretorId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Turma>()
            .HasOne(t => t.Escola)
            .WithMany(e => e.Turmas)
            .HasForeignKey(t => t.EscolaId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Escola>()
            .HasOne(e => e.Municipio)
            .WithMany(m => m.Escolas)
            .HasForeignKey(e => e.MunicipioId)
            .OnDelete(DeleteBehavior.NoAction);

        // Populando o banco de dados para teste
        modelBuilder.Entity<Diretor>().HasData(
            new Diretor { UsuarioId = 1, Nome = "Dezani", Telefone = "1799999999", Email = "dezani@email.com", Senha = "123456" }
        );

        modelBuilder.Entity<Secretario>().HasData(
            new Secretario { UsuarioId = 2, Nome = "Dezani", Telefone = "1799999999", Email = "dezani@email.com", Senha = "123456" }
        );

        modelBuilder.Entity<Municipio>().HasData(
            new Municipio { Nome = "São José do Rio Preto", Estado = "SP", IdMunicipio = 1 }
        );

        modelBuilder.Entity<Escola>().HasData(
            new Escola
            {
                IdEscola = 1,
                Nome = "Escola Teste",
                CEP = "12315808",
                MunicipioId = 1,
                Endereco = "Nada",
                DiretorId = 1,
                SecretarioId = 1
            }
        );
        modelBuilder.Entity<Turma>().HasData(
            new Turma { IdTurma = 1, Nome = "1º Ano", Ano = 2025, EscolaId = 1 }
        );
    }
}