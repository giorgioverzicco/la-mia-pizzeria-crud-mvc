using Microsoft.EntityFrameworkCore;
using la_mia_pizzeria_crud_mvc.Models;

namespace la_mia_pizzeria_crud_mvc.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
        
    }

    public DbSet<Pizza> Pizzas { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
}