using System.ComponentModel.DataAnnotations;

namespace SistemaEscolar.Models;

public class Turma
{
    [Key]
    public long IdTurma { get; set; }
    public string Nome { get; set; }
    public short Ano { get; set; }
    public List<Aluno> Alunos { get; set; }
    public List<Professor> Professores { get; set; }
    public Escola Escola { get; set; }
    public long EscolaId { get; set; }
}