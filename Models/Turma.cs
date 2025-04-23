using System.ComponentModel.DataAnnotations;

namespace SistemaEscolar.Models;

public class Turma
{
    [Key]
    public long IdTurma { get; set; }
    public string Nome { get; set; }
    public byte Ano { get; set; }
    public List<Aluno> Alunos { get; set; }
    public Professor Professor { get; set; }
    public long ProfessorId { get; set; }
    public Escola Escola { get; set; }
    public long EscolaId { get; set; }
}