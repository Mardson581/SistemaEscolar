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

    [HttpPost]
    public ActionResult Create(Aluno aluno)
    {
        return RedirectToAction("Index", "Aluno");
    }
}