using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaEscolar.Data;
using SistemaEscolar.Models;

namespace SistemaEscolar.Controllers;

[Route("/municipio/{idMunicipio:int}/escola/{action}/{idEscola:int?}")]
public class EscolaController : Controller
{
    private DbEscolar db;

    public EscolaController(DbEscolar db)
    {
        this.db = db;
    }

    [HttpGet]
    public ActionResult Cadastrar(long idMunicipio)
    {
        ViewBag.IdMunicipio = idMunicipio;
        ViewBag.Title = db.Municipios.First(m => m.IdMunicipio == idMunicipio).Nome;
        ViewBag.Secretarios = db.Secretarios.ToList();

        ViewBag.Diretores = db.Diretores
            .Include(d => d.Escola)
            .Where(d => d.Escola == null);
        return View();
    }

    [HttpPost]
    public ActionResult Cadastrar(long idMunicipio, Escola escola)
    {
        escola.MunicipioId = idMunicipio;
        db.Escolas.Add(escola);
        db.SaveChanges();
        return Redirect($"/municipio/detalhes/{idMunicipio}");
    }
}