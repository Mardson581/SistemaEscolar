namespace SistemaEscolar.Models;

public class Professor : Usuario
{
    public List<Turma> Turmas { get; set; }
}