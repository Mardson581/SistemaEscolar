using Microsoft.AspNetCore.Mvc;
using SistemaEscolar.Models;

namespace SistemaEscolar.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
