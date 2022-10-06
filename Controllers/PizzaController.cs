using la_mia_pizzeria_post.Data;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers;

public class PizzaController : Controller
{
    private readonly ApplicationDbContext _ctx;

    public PizzaController(ApplicationDbContext ctx)
    {
        _ctx = ctx;
    }

    // GET
    public IActionResult Index()
    {
        List<Pizza> pizzas = _ctx.Pizzas.ToList();
        return View(pizzas);
    }

    // GET: Pizza/Details/{id}
    public IActionResult Details(int id)
    {
        Pizza? pizza = _ctx.Pizzas.FirstOrDefault(x => x.Id == id);

        if (pizza is null)
        {
            return View("Error");
        }

        return View(pizza);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Pizza pizza)
    {
        if (!ModelState.IsValid)
        {
            return View(pizza);
        }

        _ctx.Pizzas.Add(pizza);
        _ctx.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
}