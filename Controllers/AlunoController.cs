using Microsoft.AspNetCore.Mvc;
using SistemaEscolar.Data;
using SistemaEscolar.Models;

public class AlunoController : Controller
{
    private DbEscolar db;

    public AlunoController(DbEscolar context)
    {
        this.db = context;
    }
    public ActionResult Index()
    {
        return View(db.Alunos.ToList());
    }

    public ActionResult Cadastrar()
    {
        IEnumerable<Turma> turmas = db.Turmas.ToList();
        return View(turmas);
    }

    [Route("/aluno/editar/{id}")]
    public ActionResult Editar(long? id)
    {
        if (id == null)
            return NotFound();
        
        Aluno aluno = db.Alunos.First(a => a.IdAluno == id);
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