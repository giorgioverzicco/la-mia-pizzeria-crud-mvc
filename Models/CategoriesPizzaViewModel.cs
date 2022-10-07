using Microsoft.AspNetCore.Mvc.Rendering;

namespace la_mia_pizzeria_crud_mvc.Models;

public class CategoryPizzaViewModel
{
    public IEnumerable<SelectListItem>? Categories { get; set; }
    public Pizza Pizza { get; set; } = null!;
}