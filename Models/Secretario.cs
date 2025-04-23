namespace SistemaEscolar.Models;

public class Secretario : Usuario
{
    public string Municipio { get; set; }

    public Secretario (long SecretarioId, string Nome, string Telefone, string Email, string Senha) : base (SecretarioId, Nome, Telefone, Email, Senha)
    { }
}