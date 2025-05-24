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
        Municipio municipio = 
            db.Municipios
            .Include(m => m.Escolas)
            .FirstOrDefault(m => m.IdMunicipio == id);

        if (municipio == null) {
            return RedirectToAction("Index");
        }

        ViewBag.Title = municipio.Nome;
        ViewBag.IdMunicipio = id;
        return View(municipio);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        ViewBag.Title = "Cadastro de Municípios";
        return View();
    }

    [HttpPost]
    public ActionResult Cadastrar(Municipio municipio)
    {
        db.Municipios.Add(municipio);
        db.SaveChanges();
        ViewBag.Message = "Municipio cadastrado com sucesso";
        return RedirectToAction("Index");
    }
}