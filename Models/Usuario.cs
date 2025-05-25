using System.ComponentModel.DataAnnotations;

namespace SistemaEscolar.Models;

public abstract class Usuario
{
    [Key]
    public long UsuarioId { get; set; }

    [Required]
    public string Nome { get; set; }

    [Required]
    [RegularExpression("^\\(\\d{2\\)9\\d{4}-\\d{4}$")]
    public string Telefone { get; set; }

    [Required]
    [RegularExpression("^\\w+@\\w+.\\w.\\w?")]
    public string Email { get; set; }

    [Required]
    [MinLength(8)]
    public string Senha { get; set; }

    public override string ToString()
    {
        return $"Usuario({UsuarioId}, {Nome}, {Telefone}, {Email})";
    }
}