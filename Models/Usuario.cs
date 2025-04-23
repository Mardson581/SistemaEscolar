namespace SistemaEscolar.Models;

public abstract class Usuario
{
    public long UsuarioId { get; set; }
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }

    public Usuario(long UsuarioId, string Nome, string Telefone, string Email, string Senha)
    {
        this.UsuarioId = UsuarioId;
        this.Nome = Nome;
        this.Telefone = Telefone;
        this.Email = Email;
        this.Senha = Senha;
    }

    public override string ToString()
    {
        return $"Usuario[{UsuarioId}, {Nome}, {Telefone}, {Email}, {Senha}]";
    }
}