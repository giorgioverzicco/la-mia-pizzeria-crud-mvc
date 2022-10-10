using Microsoft.AspNetCore.Mvc.Rendering;

namespace la_mia_pizzeria_crud_mvc.Models;

public class PizzaViewModel
{
    public Pizza Pizza { get; set; } = null!;
    public IEnumerable<SelectListItem>? Categories { get; set; }
    public IEnumerable<int>? IngredientIds { get; set; }
    public IEnumerable<SelectListItem>? Ingredients { get; set; }
}