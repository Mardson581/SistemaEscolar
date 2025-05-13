using Microsoft.AspNetCore.Mvc;
using SistemaEscolar.Models;
using SistemaEscolar.Data;

public class DiretorController : Controller
{
    private DbEscolar db;

    public DiretorController(DbEscolar db)
    {
        this.db = db;
    }

    public ActionResult Index()
    {
        return View(db.Diretores.ToList());
    }

    public ActionResult Cadastrar(Diretor diretor)
    {
        return View();
    }
}