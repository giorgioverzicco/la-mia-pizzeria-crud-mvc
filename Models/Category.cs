using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_crud_mvc.Models;

public class Category
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "{0} is required.")]
    public string Name { get; set; } = null!;

    public virtual ICollection<Pizza> Pizzas { get; set; } = null!;
}