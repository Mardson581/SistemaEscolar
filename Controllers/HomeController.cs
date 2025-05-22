using Microsoft.AspNetCore.Mvc;

namespace SistemaEscolar.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
