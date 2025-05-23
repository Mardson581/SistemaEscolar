using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaEscolar.Data;
using SistemaEscolar.Models;

namespace SistemaEscolar.Controllers;

[Route("/municipio/{municipioId:int}/escola/{escolaId:int}/aluno/{action=Index}/{id:int?}")]
public class AlunoController : Controller
{
    private DbEscolar db;

    public AlunoController(DbEscolar context)
    {
        this.db = context;
    }
    public ActionResult Index(long municipioId, long escolaId)
    {
        IEnumerable<Aluno> alunos = 
            db.Alunos.Where(a => 
                a.Turma.Escola.MunicipioId == municipioId &&
                a.Turma.EscolaId == escolaId
            );
        ViewBag.MunicipioId = municipioId;
        ViewBag.EscolaId = escolaId;

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
        return View(turmas);
    }

    public ActionResult Editar(long id)
    {
        Aluno aluno = db.Alunos
            .Include(a => a.Turma)
            .First(a => a.IdAluno == id);
        if (aluno == null)
            return NotFound();
        
        return View(aluno);
    }

    [HttpPost]
    public ActionResult Create(Aluno aluno)
    {
        db.Alunos.Add(aluno);
        db.SaveChanges();
        return RedirectToAction("Index", "Aluno");
    }
}