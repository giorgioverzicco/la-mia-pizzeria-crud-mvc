using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using la_mia_pizzeria_crud_mvc.Data;
using la_mia_pizzeria_crud_mvc.Models;

namespace la_mia_pizzeria_crud_mvc.Controllers;

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
        List<Pizza> pizzas = 
            _ctx.Pizzas
                .Include(x => x.Category)
                .Include(x => x.Ingredients)
                .ToList();
        return View(pizzas);
    }
    
    // GET: Pizza/Details/{id?}
    public IActionResult Details(int? id)
    {
        if (id is null or 0)
        {
            return NotFound();
        }

        Pizza? pizza = 
            _ctx.Pizzas
                .Include(x => x.Category)
                .Include(x => x.Ingredients)
                .FirstOrDefault(x => x.Id == id);

        if (pizza is null)
        {
            return NotFound();
        }
        
        return View(pizza);
    }
    
    // GET: Pizza/Create
    public IActionResult Create()
    {
        var categories =
            _ctx.Categories
                .Select(x =>
                    new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name
                    }).ToList();
        
        var ingredients =
            _ctx.Ingredients
                .Select(x =>
                    new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name
                    }).ToList();
        
        var pizzaVm = new PizzaViewModel
        {
            Pizza = new Pizza(), 
            Categories = categories,
            Ingredients = ingredients
        };
        
        return View(pizzaVm);
    }
    
    // POST: Pizza/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(PizzaViewModel pizzaVm)
    {
        if (!ModelState.IsValid)
        {
            return View(pizzaVm);
        }

        var ingredients = 
            _ctx.Ingredients
                .Where(x => pizzaVm.IngredientIds!.Contains(x.Id))
                .ToList();

        foreach (var ingredient in ingredients)
        {
            pizzaVm.Pizza.Ingredients!.Add(ingredient);
        }

        _ctx.Pizzas.Add(pizzaVm.Pizza);
        _ctx.SaveChanges();
        
        return RedirectToAction(nameof(Index));
    }
    
    // GET: Pizza/Edit/{id?}
    public IActionResult Edit(int? id)
    {
        if (id is null or 0)
        {
            return NotFound();
        }

        Pizza? pizza = 
            _ctx.Pizzas
                .Include(x => x.Ingredients)
                .FirstOrDefault(x => x.Id == id);

        if (pizza is null)
        {
            return NotFound();
        }

        var categories =
            _ctx.Categories
                .Select(x =>
                    new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name,
                        Selected = x.Id == pizza.CategoryId
                    }).ToList();

        var ingredients =
            _ctx.Ingredients
                .Select(x =>
                    new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name,
                    }).ToList();
        
        var pizzaVm = new PizzaViewModel
        {
            Pizza = pizza, 
            Categories = categories,
            Ingredients = ingredients
        };
        
        return View(pizzaVm);
    }
    
    // POST: Pizza/Edit/{id?}
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(PizzaViewModel pizzaVm)
    {
        if (!ModelState.IsValid)
        {
            return View(pizzaVm);
        }

        var pizza = 
            _ctx.Pizzas
                .Include(x => x.Ingredients)
                .FirstOrDefault(x => x.Id == pizzaVm.Pizza.Id)!;
        var newIngredients = 
            _ctx.Ingredients
                .Where(x => pizzaVm.IngredientIds!.Contains(x.Id))
                .ToList();
        var ingredients = pizza.Ingredients.ToList();

        ingredients.RemoveAll(x => !newIngredients.Contains(x));
        ingredients.AddRange(
            newIngredients.Where(x => !ingredients.Contains(x)));

        pizza.Ingredients = ingredients;

        _ctx.Pizzas.Update(pizza);
        _ctx.SaveChanges();
        
        return RedirectToAction(nameof(Index));
    }
    
    // POST: Pizza/Delete/{id?}
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int? id)
    {
        if (id is null or 0)
        {
            return NotFound();
        }
        
        Pizza? pizza = _ctx.Pizzas.Find(id);

        if (pizza is null)
        {
            return NotFound();
        }
        
        _ctx.Pizzas.Remove(pizza);
        _ctx.SaveChanges();
        
        return RedirectToAction(nameof(Index));
    }
}