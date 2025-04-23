namespace SistemaEscolar.Models;

public class Aluno
{
    public long IdAluno { get; set; }
    public string Nome { get; set; }
    public long RA { get; set; }
    public DateTime Nascimento { get; set; }
    public DateTime Matricula { get; set; }
}