namespace SistemaEscolar.Models;

public class Turma
{
    public long IdTurma { get; set; }
    public string Nome { get; set; }
    public byte Ano { get; set; }
    public List<Aluno> Alunos { get; set; }
}