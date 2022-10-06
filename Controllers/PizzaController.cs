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
    
    public IActionResult Update(int id)
    {
        Pizza? pizza = _ctx.Pizzas.FirstOrDefault(x => x.Id == id);

        if (pizza is null)
        {
            return View("Error");
        }

        return View(pizza);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(int id, Pizza newPizza)
    {
        Pizza? pizza = _ctx.Pizzas.FirstOrDefault(x => x.Id == id);

        if (pizza is null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(newPizza);
        }

        pizza.Name = newPizza.Name;
        pizza.Description = newPizza.Description;
        pizza.Photo = newPizza.Photo;
        pizza.Price = newPizza.Price;
        
        _ctx.SaveChanges();
        
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        Pizza? pizza = _ctx.Pizzas.FirstOrDefault(x => x.Id == id);

        if (pizza is null)
        {
            return NotFound();
        }

        _ctx.Remove(pizza);
        _ctx.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
}