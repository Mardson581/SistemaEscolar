using System.ComponentModel.DataAnnotations;

namespace SistemaEscolar.Models;

public class Aluno
{
    [Key]
    public long IdAluno { get; set; }
    public string Nome { get; set; }
    public long RA { get; set; }
    public DateOnly Nascimento { get; set; }
    public DateOnly Matricula { get; set; }
    public Turma Turma { get; set; }
    public long IdTurma { get; set; }
}