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
        var pizzaVm = new PizzaViewModel
        {
            Pizza = new Pizza(), 
            Categories = _ctx.Categories
                .Select(x => 
                    new SelectListItem
                    {
                        Value = x.Id.ToString(), 
                        Text = x.Name
                    }).ToList()
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
        
        Pizza? pizza = _ctx.Pizzas.Find(id);

        if (pizza is null)
        {
            return NotFound();
        }
        
        var pizzaVm = new PizzaViewModel
        {
            Pizza = pizza, 
            Categories = _ctx.Categories
                .Select(x => 
                    new SelectListItem
                    {
                        Value = x.Id.ToString(), 
                        Text = x.Name,
                        Selected = x.Id == pizza.CategoryId
                    }).ToList()
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

        _ctx.Pizzas.Update(pizzaVm.Pizza);
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