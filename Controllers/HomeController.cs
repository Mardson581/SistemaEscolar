using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SistemaEscolar.Models;

namespace SistemaEscolar.Controllers;

[Route("/{action=Login}")]
public class HomeController : Controller
{
    private SignInManager<Gestor> signInManager;
    private UserManager<Gestor> userManager;

    public HomeController(SignInManager<Gestor> signInManager, UserManager<Gestor> userManager)
    {
        this.signInManager = signInManager;
        this.userManager = userManager;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Login(string email, string password)
    {
        Gestor? gestor = await userManager.FindByEmailAsync(email);

        if (gestor != null)
        {
            var result = await signInManager.PasswordSignInAsync(gestor, password, true, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "municipio");
            }
            else
            {
                ViewBag.Message = "A senha é inválida!";
                return View();
            }
        }

        ViewBag.Message = "O email é inválido!";
        return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult Cadastrar()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Cadastrar(Gestor gestor, string password)
    {
        gestor.NormalizedEmail = gestor.Email.ToUpper();
        gestor.PasswordHash = new PasswordHasher<Gestor>().HashPassword(gestor, password);
        
        var result = await userManager.CreateAsync(gestor);
        if (result.Succeeded)
        {
            await signInManager.SignInAsync(gestor, true);
            return RedirectToAction("index", "municipio");
        }

        return Unauthorized();
    }

    [Route("/erro")]
    public IActionResult Erro()
    {
        return View();
    }
}
