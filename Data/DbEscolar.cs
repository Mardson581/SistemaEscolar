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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Turma>()
            .HasOne(t => t.Professor)
            .WithMany(p => p.Turmas)
            .HasForeignKey(p => p.ProfessorId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Escola>()
            .HasOne(e => e.Secretario)
            .WithMany(s => s.Escolas)
            .HasForeignKey(e => e.SecretarioId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Diretor>()
            .HasOne(d => d.Escola)
            .WithOne(e => e.Diretor)
            .HasForeignKey<Escola>(e => e.DiretorId);

        modelBuilder.Entity<Turma>()
            .HasOne(t => t.Escola)
            .WithMany(e => e.Turmas)
            .HasForeignKey(t => t.EscolaId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Aluno>().HasData(
            new Aluno{ IdAluno = 1, Nome = "Mardson", RA = 123123, Nascimento = DateTime.Now, Matricula = DateTime.Now}
        );
    }
}