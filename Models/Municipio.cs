using System.ComponentModel.DataAnnotations;

namespace SistemaEscolar.Models;

public class Municipio
{
    [Key]
    public long IdMunicipio { get; set; }
    public List<Escola> Escolas { get; set; }
    public String Nome { get; set; }
    public String Estado { get; set; }
}