using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_crud_mvc.Models;

public class Category
{
    public Category()
    {
        Pizzas = new List<Pizza>();
    }
    
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public virtual ICollection<Pizza> Pizzas { get; set; }
}