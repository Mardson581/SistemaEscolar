using System.ComponentModel.DataAnnotations;

namespace SistemaEscolar.Models;

public abstract class Usuario
{
    [Key]
    public long UsuarioId { get; set; }
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
}