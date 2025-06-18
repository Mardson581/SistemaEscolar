using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaEscolar.Data;
using SistemaEscolar.Models;

namespace SistemaEscolar.Controllers;

[Authorize]
[Route("/municipio/{municipioId:int}/escola/{idEscola:int}/turma/{action=Index}/{id:int?}")]
public class TurmaController : Controller
{
    private DbEscolar db;

    public TurmaController(DbEscolar db)
    {
        this.db = db;
    }


    public ActionResult Index(long municipioId, long idEscola)
    {
        IEnumerable<Turma> turmas = db.Turmas
            .Where(t => t.EscolaId == idEscola)
            .OrderBy(t => t.Nome);
        ViewBag.Title = "Turmas";
        ViewBag.UrlBase = $"/municipio/{municipioId}/escola/{idEscola}";
        ViewBag.Escola = db.Escolas.FirstOrDefault(e => e.IdEscola == idEscola);
        return View(turmas);
    }

    [HttpGet]
    public ActionResult Cadastrar(long municipioId, long idEscola)
    {
        IEnumerable<Professor> professores = db.Professores;
        ViewBag.Title = "Turmas";
        ViewBag.UrlBase = $"/municipio/{municipioId}/escola/{idEscola}";
        ViewBag.Escola = db.Escolas.FirstOrDefault(e => e.IdEscola == idEscola);
        return View(professores);
    }

    [HttpPost]
    public ActionResult Cadastrar(long municipioId, long idEscola, long[] professorId, Turma turma)
    {
        Escola? escola = db.Escolas.FirstOrDefault(e => e.IdEscola == idEscola);

        if (escola == null)
            return NotFound();

        if (escola.MunicipioId == municipioId)
            turma.EscolaId = escola.IdEscola;
        else
            return NotFound();

        if (turma.Professores == null)
            turma.Professores = new List<Professor>();
        foreach (long id in professorId)
        {
            Professor? professor = db.Professores.FirstOrDefault(p => p.UsuarioId == id);

            if (professor == null)
                return NotFound();
            turma.Professores.Add(professor);
        }
        db.Turmas.Add(turma);
        db.SaveChanges();

        ViewBag.Title = "Turmas";
        ViewBag.UrlBase = $"/municipio/{municipioId}/escola/{idEscola}";
        ViewBag.Escola = escola;
        return Redirect($"/municipio/{municipioId}/escola/{idEscola}/turma/");
    }

    [HttpGet]
    public IActionResult Editar(long municipioId, long idEscola, long id)
    {
        Turma? turma = db.Turmas
            .Include(t => t.Professores)
            .FirstOrDefault(t => t.IdTurma == id);

        if (turma != null)
        {
            ViewBag.Professores = db.Professores.ToList();
            ViewBag.UrlBase = $"/municipio/{municipioId}/escola/{idEscola}";
            return View(turma);
        }
        return NotFound();
    }

    [HttpPost]
    public IActionResult Editar(long municipioId, long idEscola, long[] professorId, long id, Turma turma)
    {
        Escola? escola = db.Escolas.FirstOrDefault(e => e.IdEscola == idEscola);

        if (escola == null)
            return NotFound();

        if (escola.MunicipioId != municipioId)
            return NotFound();
        
        Turma? turmaExistente = db.Turmas
            .Include(t => t.Professores)
            .FirstOrDefault(t => t.IdTurma == id && t.EscolaId == idEscola);

        if (turmaExistente == null)
            return NotFound();
        turmaExistente.Nome = turma.Nome;
        turmaExistente.Ano = turma.Ano;

        turmaExistente.Professores.Clear();
        foreach (long _id in professorId)
        {
            Professor? professor = db.Professores.FirstOrDefault(p => p.UsuarioId == _id);

            if (professor == null)
                return NotFound();
            turmaExistente.Professores.Add(professor);
        }

        db.SaveChanges();
        return Redirect($"/municipio/{municipioId}/escola/{idEscola}/turma");
    }
}