using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaEscolar.Data;
using SistemaEscolar.Models;

namespace SistemaEscolar.Controllers;

public class MunicipioController : Controller
{
    private DbEscolar db;
    public MunicipioController(DbEscolar db)
    {
        this.db = db;
    }

    public ActionResult Index()
    {
        List<Municipio> municipios = 
            db.Municipios
            .Include(m => m.Escolas)
            .ToList();
        return View(municipios);
    }

    public ActionResult Detalhes(long id)
    {
        return View();
    }
}