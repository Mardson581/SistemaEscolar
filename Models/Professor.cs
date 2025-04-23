namespace SistemaEscolar.Models;

public class Professor : Usuario
{
    public Professor (long ProfessorId, string Nome, string Telefone, string Email, string Senha) : base (ProfessorId, Nome, Telefone, Email, Senha)
    { }
}