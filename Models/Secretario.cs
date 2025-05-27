namespace SistemaEscolar.Models;

public class Secretario : Usuario
{
    public List<Escola>? Escolas { get; set; }

    public override string ToString()
    {
        return $"Usuario({UsuarioId}, {Nome}, {Telefone}, {Email}, {Escolas})";
    }
}