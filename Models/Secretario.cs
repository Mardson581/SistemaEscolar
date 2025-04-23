namespace SistemaEscolar.Models;

public class Secretario : Usuario
{
    public List<Escola> Escolas { get; set; }
}