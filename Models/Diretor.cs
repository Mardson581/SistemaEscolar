using SistemaEscolar.Models;

public class Diretor : Usuario
{
    public Diretor (long DiretorId, string Nome, string Telefone, string Email, string Senha) : base (DiretorId, Nome, Telefone, Email, Senha)
    { }
}