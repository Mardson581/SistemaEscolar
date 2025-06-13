using Microsoft.AspNetCore.Mvc;
using SistemaEscolar.Models;
using SistemaEscolar.Data;
using Microsoft.AspNetCore.Authorization;

namespace SistemaEscolar.Controllers;

[Authorize]
[Route("/municipio/{idMunicipio:int}/escola/{idEscola:int}/professor/{action=Index}/{id:int?}")]
public class ProfessorController : Controller
{
    private DbEscolar db;

    public ProfessorController(DbEscolar db)
    {
        this.db = db;
    }

    public ActionResult Index(long idMunicipio, long idEscola)
    {
        IEnumerable<Professor> professores = db.Professores.ToList();
        ViewBag.UrlBase = $"/municipio/{idMunicipio}/escola/{idEscola}";
        ViewBag.Escola = db.Escolas.FirstOrDefault(e => e.IdEscola == idEscola);
        ViewBag.NomeEscola =
            db.Municipios.FirstOrDefault(m => m.IdMunicipio == idMunicipio).Nome +
            " - " +
            db.Escolas.FirstOrDefault(e => e.IdEscola == idEscola).Nome;
        return View(professores);
    }

    [HttpGet]
    public ActionResult Cadastrar(long idMunicipio, long idEscola)
    {
        ViewBag.UrlBase = $"/municipio/{idMunicipio}/escola/{idEscola}";
        ViewBag.Escola = db.Escolas.FirstOrDefault(e => e.IdEscola == idEscola);
        ViewBag.NomeEscola =
            db.Municipios.FirstOrDefault(m => m.IdMunicipio == idMunicipio).Nome +
            " - " +
            db.Escolas.FirstOrDefault(e => e.IdEscola == idEscola).Nome;
        return View();
    }

    [HttpPost]
    public ActionResult Cadastrar(Professor professor, long idMunicipio, long idEscola)
    {
        db.Professores.Add(professor);
        db.SaveChanges();
        
        ViewBag.UrlBase = $"/municipio/{idMunicipio}/escola/{idEscola}";
        ViewBag.Escola = db.Escolas.FirstOrDefault(e => e.IdEscola == idEscola);
        ViewBag.NomeEscola =
            db.Municipios.FirstOrDefault(m => m.IdMunicipio == idMunicipio).Nome +
            " - " +
            db.Escolas.FirstOrDefault(e => e.IdEscola == idEscola).Nome;
        return Redirect($"/municipio/{idMunicipio}/escola/{idEscola}/professor/");
    }

    [HttpGet]
    public ActionResult Editar(long idMunicipio, long idEscola, long id)
    {
        Professor professor = db.Professores.FirstOrDefault(p => p.UsuarioId == id);

        if (professor == null)
            return NotFound();

        ViewBag.UrlBase = $"/municipio/{idMunicipio}/escola/{idEscola}";
        ViewBag.Escola = db.Escolas.FirstOrDefault(e => e.IdEscola == idEscola);
        ViewBag.MunicipioId = idMunicipio;
        ViewBag.EscolaId = idEscola;

        return View(professor);
    }

    [HttpPost]
    public ActionResult Editar(long idMunicipio, long idEscola, long idProfessor, Professor professor)
    {
        db.Professores.Update(professor);
        db.SaveChanges();

        ViewBag.UrlBase = $"/municipio/{idMunicipio}/escola/{idEscola}";
        ViewBag.Escola = db.Escolas.FirstOrDefault(e => e.IdEscola == idEscola);
        ViewBag.NomeEscola =
            db.Municipios.FirstOrDefault(m => m.IdMunicipio == idMunicipio).Nome +
            " - " +
            db.Escolas.FirstOrDefault(e => e.IdEscola == idEscola).Nome;
        return Redirect($"/municipio/{idMunicipio}/escola/{idEscola}/professor/");
    }

    [HttpPost]
    public ActionResult Deletar(long idMunicipio, long idEscola, long id)
    {
        db.Professores.Remove(db.Professores.FirstOrDefault(p => p.UsuarioId == id));
        db.SaveChanges();

        ViewBag.UrlBase = $"/municipio/{idMunicipio}/escola/{idEscola}";
        ViewBag.Escola = db.Escolas.FirstOrDefault(e => e.IdEscola == idEscola);
        ViewBag.NomeEscola =
            db.Municipios.FirstOrDefault(m => m.IdMunicipio == idMunicipio).Nome +
            " - " +
            db.Escolas.FirstOrDefault(e => e.IdEscola == idEscola).Nome;
        return Redirect($"/municipio/{idMunicipio}/escola/{idEscola}/professor/");
    }
}