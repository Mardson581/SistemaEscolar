using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaEscolar.Data;
using SistemaEscolar.Models;

namespace SistemaEscolar.Controllers;

[Route("/municipio/{idMunicipio:int}/escola/{idEscola}/{action?}")]
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

    [HttpGet]
    public ActionResult Editar(long idMunicipio, long idEscola)
    {
        Escola? escola = db.Escolas
            .Include(e => e.Diretor)
            .Include(e => e.Secretario)
            .FirstOrDefault(e => e.IdEscola == idEscola && e.MunicipioId == idMunicipio);

        if (escola == null) {
            return NotFound();
        }
        ViewBag.Secretarios = db.Secretarios.ToList();
        ViewBag.Diretores = db.Diretores
            .Include(e => e.Escola)
            .Where(e => e.Escola == null);
        ViewBag.Escola = escola;
        ViewBag.IdMunicipio = idMunicipio;
        ViewBag.Title = db.Municipios.First(m => m.IdMunicipio == idMunicipio).Nome;

        return View(escola);
    }

    [HttpPost]
    public ActionResult Editar(long idMunicipio, long idEscola, Escola escola)
    {
        escola.IdEscola = idEscola;
        escola.MunicipioId = idMunicipio;
        db.Escolas.Update(escola);
        db.SaveChanges();
        return Redirect($"/municipio/{idMunicipio}/escola/{idEscola}/aluno");
    }

    [HttpPost]
    public ActionResult Deletar(long idMunicipio, long idEscola)
    {
        Escola? escola = db.Escolas
            .FirstOrDefault(e => e.IdEscola == idEscola && e.MunicipioId == idMunicipio);
        
        if (escola != null)
            db.Escolas.Remove(escola);
        db.SaveChanges();
        return Redirect($"/municipio/detalhes/{idMunicipio}/");
    }
}