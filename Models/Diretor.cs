namespace SistemaEscolar.Models;

public class Diretor : Usuario
{
    public Escola? Escola { get; set; }
    public long EscolaId { get; set; }

    public override string ToString()
    {
        return $"Usuario({UsuarioId}, {Nome}, {Telefone}, {Nascimento}, {Email}, {Escola})";
    }
}