using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_crud_mvc.Controllers;

public class ExerciseController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}