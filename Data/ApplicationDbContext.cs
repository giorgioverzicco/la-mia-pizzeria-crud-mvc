using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using la_mia_pizzeria_post;
using la_mia_pizzeria_crud_mvc.Models;

namespace la_mia_pizzeria_post.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Pizza> Pizzas { get; set; } = null!;

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=db-pizzeria;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasData(
                    new Category
                    {
                        Id = 1,
                        Name = "Pizze classiche"
                    },
                    new Category
                    {
                        Id = 2,
                        Name = "Pizze bianche"
                    },
                    new Category
                    {
                        Id = 3,
                        Name = "Pizze vegetariane"
                    },
                    new Category
                    {
                        Id = 4,
                        Name = "Pizze di mare"
                    });
            
            modelBuilder.Entity<Pizza>()
                .HasData(
                    new Pizza
                    {
                        Id = 1,
                        Name = "Margherita",
                        Description = "La classica pizza",
                        Photo = "margherita.png",
                        Price = 4.00M,
                        CategoryId = 1
                    },
                    new Pizza
                    {
                        Id = 2,
                        Name = "Diavola",
                        Description = "La classica pizza piccante",
                        Photo = "diavola.png",
                        Price = 5.50M,
                        CategoryId = 1
                    },
                    new Pizza
                    {
                        Id = 3,
                        Name = "Cotto",
                        Description = "La classica pizza con prosciutto cotto",
                        Photo = "cotto.png",
                        Price = 5.00M,
                        CategoryId = 1
                    },
                    new Pizza
                    {
                        Id = 4,
                        Name = "Salsiccia",
                        Description = "La classica pizza con la salsiccia",
                        Photo = "salsiccia.png",
                        Price = 6.50M,
                        CategoryId = 1
                    },
                    new Pizza
                    {
                        Id = 5,
                        Name = "Ortolana",
                        Description = "La classica pizza vegetariana",
                        Photo = "ortolana.png",
                        Price = 6.50M,
                        CategoryId = 3
                    },
                    new Pizza
                    {
                        Id = 6,
                        Name = "Marinara",
                        Description = "La classica pizza bianca",
                        Photo = "marinara.png",
                        Price = 5.00M,
                        CategoryId = 2
                    },
                    new Pizza
                    {
                        Id = 7,
                        Name = "Salmone e Rucola",
                        Description = "La classica pizza di mare",
                        Photo = "salmone_e_rucola.png",
                        Price = 8.00M,
                        CategoryId = 4
                    });
        }
    }
}
