using Microsoft.AspNetCore.Mvc;
using SistemaEscolar.Models;
using SistemaEscolar.Data;

namespace SistemaEscolar.Controllers;

[Route("/municipio/{idMunicipio:int}/escola/{idEscola:int}/diretor/{action=Index}/{id:int?}")]
public class DiretorController : Controller
{
    private DbEscolar db;

    public DiretorController(DbEscolar db)
    {
        this.db = db;
    }

    public ActionResult Index(long idMunicipio, long idEscola)
    {
        Diretor? diretor = db.Diretores
            .FirstOrDefault(d => d.EscolaId == idEscola);
        ViewBag.UrlBase = $"/municipio/{idMunicipio}/escola/{idEscola}";
        ViewBag.NomeEscola = 
            db.Municipios.FirstOrDefault(m => m.IdMunicipio == idMunicipio).Nome +
            " - " +
            db.Escolas.FirstOrDefault(e => e.IdEscola == idEscola).Nome;

        return View(diretor);
    }

    [HttpGet]
    public ActionResult Cadastrar(long idMunicipio, long idEscola)
    {
        ViewBag.UrlBase = $"/municipio/{idMunicipio}/escola/{idEscola}";
        ViewBag.NomeMunicipio = db.Municipios.FirstOrDefault(m => m.IdMunicipio == idMunicipio).Nome;
        return View();
    }

    [HttpPost]
    public ActionResult Cadastrar(long idMunicipio, long idEscola, Diretor diretor)
    {
        ViewBag.UrlBase = $"/municipio/{idMunicipio}/escola/{idEscola}";
        ViewBag.NomeMunicipio = db.Municipios.FirstOrDefault(m => m.IdMunicipio == idMunicipio).Nome;
        return Redirect($"/municipio/{idMunicipio}/escola/{idEscola}/aluno/");
    }

    [HttpGet]
    public ActionResult Designar(long idMunicipio, long idEscola)
    {
        IEnumerable<Diretor> diretores = db.Diretores
            .Where(d => d.Escola == null);
        ViewBag.NomeEscola = db.Escolas.First(e => e.IdEscola == idEscola).Nome;
        return View(diretores);
    }

    [HttpPost]
    public ActionResult Designar(long idMunicipio, long idEscola, long idDiretor)
    {
        ViewBag.UrlBase = $"/municipio/{idMunicipio}/escola/{idEscola}";
        ViewBag.NomeMunicipio = db.Municipios.FirstOrDefault(m => m.IdMunicipio == idMunicipio).Nome;
        return Redirect($"/municipio/{idMunicipio}/escola/{idEscola}/aluno/");
    }
}