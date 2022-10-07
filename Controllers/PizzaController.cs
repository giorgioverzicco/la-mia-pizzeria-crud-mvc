using la_mia_pizzeria_post.Data;
using la_mia_pizzeria_crud_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
        List<Pizza> pizzas = _ctx.Pizzas.ToList();
        return View(pizzas);
    }

    // GET: Pizza/Details/{id}
    public IActionResult Details(int id)
    {
        Pizza? pizza = 
            _ctx.Pizzas
                .Include(x => x.Category)
                .FirstOrDefault(x => x.Id == id);

        if (pizza is null)
        {
            return View("Error");
        }

        return View(pizza);
    }

    public IActionResult Create()
    {
        IEnumerable<SelectListItem> categories = 
            _ctx.Categories
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
        Pizza emptyPizza = new Pizza();
        CategoryPizzaViewModel categoryPizzaVm = new CategoryPizzaViewModel
        {
            Categories = categories,
            Pizza = emptyPizza
        };
        
        return View(categoryPizzaVm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CategoryPizzaViewModel categoryPizzaVm)
    {
        if (!ModelState.IsValid)
        {
            return View(categoryPizzaVm);
        }

        _ctx.Pizzas.Add(categoryPizzaVm.Pizza);
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
        
        IEnumerable<SelectListItem> categories = 
            _ctx.Categories
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });

        CategoryPizzaViewModel categoryPizzaVm = new CategoryPizzaViewModel
        {
            Categories = categories,
            Pizza = pizza
        };

        return View(categoryPizzaVm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(int id, CategoryPizzaViewModel categoryPizzaViewModel)
    {
        Pizza? pizza = _ctx.Pizzas.FirstOrDefault(x => x.Id == id);

        if (pizza is null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(categoryPizzaViewModel);
        }

        pizza.Name = categoryPizzaViewModel.Pizza.Name;
        pizza.Description = categoryPizzaViewModel.Pizza.Description;
        pizza.Photo = categoryPizzaViewModel.Pizza.Photo;
        pizza.Price = categoryPizzaViewModel.Pizza.Price;
        pizza.CategoryId = categoryPizzaViewModel.Pizza.CategoryId;
        
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