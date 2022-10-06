using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using la_mia_pizzeria_post;
using la_mia_pizzeria_static.Models;

namespace la_mia_pizzeria_post.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; }

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
            modelBuilder.Entity<Pizza>()
                .HasData(
                    new Pizza
                    {
                        Id = 1,
                        Name = "Margherita",
                        Description = "La classica pizza",
                        Photo = "margherita.png",
                        Price = 4.00M
                    },
                    new Pizza
                    {
                        Id = 2,
                        Name = "Diavola",
                        Description = "La classica pizza piccante",
                        Photo = "diavola.png",
                        Price = 5.50M
                    },
                    new Pizza
                    {
                        Id = 3,
                        Name = "Cotto",
                        Description = "La classica pizza con prosciutto cotto",
                        Photo = "cotto.png",
                        Price = 5.00M
                    },
                    new Pizza
                    {
                        Id = 4,
                        Name = "Salsiccia",
                        Description = "La classica pizza con la salsiccia",
                        Photo = "salsiccia.png",
                        Price = 6.50M
                    });
        }
    }
}
