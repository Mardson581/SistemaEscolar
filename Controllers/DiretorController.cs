using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaEscolar.Models;
using SistemaEscolar.Data;
using Microsoft.EntityFrameworkCore;

namespace SistemaEscolar.Controllers;

[Authorize]
[Route("/municipio/{idMunicipio:int}/escola/{idEscola:int}/diretor/{action=Index}/{id:int?}")]
public class DiretorController : Controller
{
    private DbEscolar db;

    public DiretorController(DbEscolar db)
    {
        this.db = db;
    }

    public ActionResult Index(long idMunicipio, long idEscola)
    {
        Diretor? diretor = db.Diretores
            .Include(d => d.Escola)
            .FirstOrDefault(d => d.EscolaId == idEscola);

        ViewBag.UrlBase = $"/municipio/{idMunicipio}/escola/{idEscola}";
        ViewBag.Escola = db.Escolas.FirstOrDefault(e => e.IdEscola == idEscola);
        ViewBag.NomeEscola =
            db.Municipios.FirstOrDefault(m => m.IdMunicipio == idMunicipio).Nome +
            " - " +
            db.Escolas.FirstOrDefault(e => e.IdEscola == idEscola).Nome;

        return View(diretor);
    }

    [HttpGet]
    public ActionResult Cadastrar(long idMunicipio, long idEscola)
    {
        ViewBag.UrlBase = $"/municipio/{idMunicipio}/escola/{idEscola}";
        ViewBag.Escola = db.Escolas.FirstOrDefault(e => e.IdEscola == idEscola);
        ViewBag.NomeMunicipio = db.Municipios.FirstOrDefault(m => m.IdMunicipio == idMunicipio).Nome;
        return View();
    }

    [HttpPost]
    public ActionResult Cadastrar(long idMunicipio, long idEscola, Diretor diretor)
    {
        db.Diretores.Add(diretor);
        db.SaveChanges();

        ViewBag.UrlBase = $"/municipio/{idMunicipio}/escola/{idEscola}";
        ViewBag.Escola = db.Escolas.FirstOrDefault(e => e.IdEscola == idEscola);
        ViewBag.NomeMunicipio = db.Municipios.FirstOrDefault(m => m.IdMunicipio == idMunicipio).Nome;
        return Redirect($"/municipio/{idMunicipio}/escola/{idEscola}/diretor/");
    }

    [HttpGet]
    public ActionResult Designar(long idMunicipio, long idEscola)
    {
        IEnumerable<Diretor> diretores = db.Diretores
            .Include(d => d.Escola)
            .Where(d => d.Escola == null);

        ViewBag.DiretorAtual = db.Diretores.FirstOrDefault(d => d.EscolaId == idEscola);

        ViewBag.NomeEscola = db.Escolas.First(e => e.IdEscola == idEscola).Nome;
        ViewBag.Escola = db.Escolas.FirstOrDefault(e => e.IdEscola == idEscola);
        ViewBag.NomeMunicipio = db.Municipios.FirstOrDefault(m => m.IdMunicipio == idMunicipio).Nome;
        return View(diretores);
    }

    [HttpPost]
    public ActionResult Designar(long idMunicipio, long idEscola, long idDiretor)
    {
        Escola? escola = db.Escolas
            .Include(e => e.Diretor)
            .FirstOrDefault(e => e.IdEscola == idEscola);

        if (escola == null)
        {
            return NotFound();
        }

        if (idDiretor != 0)
        {
            Diretor? diretor = db.Diretores.FirstOrDefault(d => d.UsuarioId == idDiretor);
            if (diretor != null)
            {
                diretor.EscolaId = idEscola;
                escola.DiretorId = idDiretor;
            }
        }
        else
        {
            if (escola.Diretor != null)
                escola.Diretor.EscolaId = null;
            escola.DiretorId = null;
        }
        db.SaveChanges();

        ViewBag.UrlBase = $"/municipio/{idMunicipio}/escola/{idEscola}";
        ViewBag.Escola = db.Escolas.FirstOrDefault(e => e.IdEscola == idEscola);
        ViewBag.NomeMunicipio = db.Municipios.FirstOrDefault(m => m.IdMunicipio == idMunicipio)?.Nome;

        return Redirect($"/municipio/{idMunicipio}/escola/{idEscola}/diretor/");
    }

    [HttpPost]
    public ActionResult Deletar(long idMunicipio, long idEscola, long id)
    {
        db.Diretores.Remove(db.Diretores.FirstOrDefault(d => d.UsuarioId == id));
        db.SaveChanges();

        ViewBag.UrlBase = $"/municipio/{idMunicipio}/escola/{idEscola}";
        ViewBag.Escola = db.Escolas.FirstOrDefault(e => e.IdEscola == idEscola);
        ViewBag.NomeEscola =
            db.Municipios.FirstOrDefault(m => m.IdMunicipio == idMunicipio).Nome +
            " - " +
            db.Escolas.FirstOrDefault(e => e.IdEscola == idEscola).Nome;

        return Redirect($"/municipio/{idMunicipio}/escola/{idEscola}/diretor/");
    }

    [HttpGet]
    public ActionResult Editar(long idMunicipio, long idEscola, long id)
    {
        Diretor? diretor = db.Diretores
            .FirstOrDefault(d => d.UsuarioId == id);

        if (diretor == null)
            return NotFound();
        
        return View(diretor);
    }

    [HttpPost]
    public ActionResult Editar(long idMunicipio, long idEscola, Diretor diretor)
    {
        db.Diretores.Update(diretor);
        diretor.Escola = db.Escolas.FirstOrDefault(e => e.IdEscola == diretor.EscolaId);
        db.SaveChanges();
        
        return RedirectToAction("Index", "Diretor");
    }
}