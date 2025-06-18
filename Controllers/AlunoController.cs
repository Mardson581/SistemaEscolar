using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaEscolar.Data;
using SistemaEscolar.Models;

namespace SistemaEscolar.Controllers;

[Authorize]
[Route("/municipio/{municipioId:int}/escola/{escolaId:int}/aluno/{action=Index}/{id:int?}")]
public class AlunoController : Controller
{
    private DbEscolar db;

    public AlunoController(DbEscolar context)
    {
        this.db = context;
    }
    public ActionResult Index(long municipioId, long escolaId, string? search)
    {
        IEnumerable<Aluno> alunos = 
            db.Alunos.Where(a => 
                a.Turma.Escola.MunicipioId == municipioId &&
                a.Turma.EscolaId == escolaId
            );
        ViewBag.UrlBase = $"/municipio/{municipioId}/escola/{escolaId}";
        ViewBag.Escola = db.Escolas.FirstOrDefault(e => e.IdEscola == escolaId);
        ViewBag.NomeEscola = 
            db.Municipios.FirstOrDefault(m => m.IdMunicipio == municipioId).Nome +
            " - " +
            db.Escolas.FirstOrDefault(e => e.IdEscola == escolaId).Nome;

        if (search != null)
        {
            alunos = alunos.Where(a => a.Nome.ToLower().Contains(search.ToLower()));
        }
        return View(alunos);
    }

    [HttpGet]
    public ActionResult Cadastrar(long municipioId, long escolaId)
    {
        IEnumerable<Turma> turmas = 
            db.Turmas.Where(t => 
                t.Escola.MunicipioId == municipioId &&
                t.EscolaId == escolaId
            );
        ViewBag.UrlBase = $"/municipio/{municipioId}/escola/{escolaId}";
        ViewBag.Escola = db.Escolas.FirstOrDefault(e => e.IdEscola == escolaId);
        return View(turmas);
    }

    [HttpPost]
    public IActionResult Create(long municipioId, long escolaId, Aluno aluno)
    {
        Turma? turma = db.Turmas.FirstOrDefault(t => t.IdTurma == aluno.IdTurma);
        if (turma == null)
            return Unauthorized();
        aluno.Turma = turma;
        db.Alunos.Add(aluno);
        db.SaveChanges();
        return RedirectToAction("Index", "Aluno");
    }

    [HttpGet]
    public ActionResult Editar(long municipioId, long escolaId, long id)
    {
        Aluno aluno = db.Alunos
            .Include(a => a.Turma)
            .First(a => a.IdAluno == id);
        if (aluno == null)
            return NotFound();

        ViewBag.UrlBase = $"/municipio/{municipioId}/escola/{escolaId}";
        ViewBag.Escola = db.Escolas.FirstOrDefault(e => e.IdEscola == escolaId);
        ViewBag.MunicipioId = municipioId;
        ViewBag.EscolaId = escolaId;

        return View(aluno);
    }

    [HttpPost]
    public ActionResult Editar(long municipioId, long escolaId, Aluno aluno)
    {
        db.Alunos.Update(aluno);
        db.SaveChanges();
        
        ViewBag.UrlBase = $"/municipio/{municipioId}/escola/{escolaId}";
        ViewBag.Escola = db.Escolas.FirstOrDefault(e => e.IdEscola == escolaId);
        ViewBag.NomeMunicipio = db.Municipios.FirstOrDefault(m => m.IdMunicipio == municipioId).Nome;
        return Redirect($"/municipio/{municipioId}/escola/{escolaId}/aluno/");
    }

    public ActionResult Deletar(long municipioId, long escolaId, long id)
    {
        Aluno aluno = db.Alunos.FirstOrDefault(a => a.IdAluno == id);
        if (aluno == null)
            return NotFound();

        db.Alunos.Remove(aluno);
        db.SaveChanges();
        
        ViewBag.UrlBase = $"/municipio/{municipioId}/escola/{escolaId}";
        ViewBag.Escola = db.Escolas.FirstOrDefault(e => e.IdEscola == escolaId);
        ViewBag.NomeMunicipio = db.Municipios.FirstOrDefault(m => m.IdMunicipio == municipioId).Nome;
        return Redirect($"/municipio/{municipioId}/escola/{escolaId}/aluno/");
    }
}