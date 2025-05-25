using System.ComponentModel.DataAnnotations;

namespace SistemaEscolar.Models;

public class Escola
{
    [Key]
    public long IdEscola { get; set; }

    [Required]
    public string Nome { get; set; }

    [Required]
    [RegularExpression("^\\d{5}-\\d{3}$")]
    public string CEP { get; set; }

    [Required]
    [RegularExpression("^\\(\\d{2\\)9\\d{4}-\\d{4}$")]
    public string Telefone { get; set; }

    [Required]
    public string Endereco { get; set; }

    [Required]
    public List<Turma> Turmas { get; set; }
    public Secretario? Secretario { get; set; }
    public long? SecretarioId { get; set; }
    public Diretor? Diretor { get; set; }
    public long? DiretorId { get; set; }

    [Required]
    public Municipio Municipio { get; set; }
    public long MunicipioId { get; set; }
}