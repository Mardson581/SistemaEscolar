using System.ComponentModel.DataAnnotations;

namespace SistemaEscolar.Models;

public class Escola
{
    [Key]
    public long IdEscola { get; set; }
    public string Nome { get; set; }
    public string CEP { get; set; }
    public string Telefone { get; set; }
    public string Endereco { get; set; }
    public List<Turma> Turmas { get; set; }
    public Secretario Secretario { get; set; }
    public long SecretarioId { get; set; }
    public Diretor Diretor { get; set; }
    public long DiretorId { get; set; }
    public Municipio Municipio { get; set; }
    public long MunicipioId { get; set; }
}