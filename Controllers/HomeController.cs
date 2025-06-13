using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SistemaEscolar.Models;

namespace SistemaEscolar.Controllers;

[Route("/{action=Index}")]
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
                return Unauthorized("A senha é inválida!");
            }
        }

        return Unauthorized("O email é inválido!");
    }
}
