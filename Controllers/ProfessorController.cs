using Microsoft.AspNetCore.Mvc;
using SistemaEscolar.Models;
using SistemaEscolar.Data;

namespace SistemaEscolar.Controllers;
public class ProfessorController : Controller
{
    private DbEscolar db;

    public ProfessorController(DbEscolar db)
    {
        this.db = db;
    }

    public ActionResult Index()
    {
        return View(db.Professores.ToList());
    }

    public ActionResult Cadastrar(Professor professor)
    {
        return View();
    }
}