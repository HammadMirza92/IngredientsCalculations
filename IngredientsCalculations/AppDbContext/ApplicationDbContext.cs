using IngredientsCalculations.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IngredientsCalculations.AppDbContext
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Ingredients> Ingredients { get; set; }
        public DbSet<ProductIngredients> ProductIngredients { get; set; }
        public DbSet<Currency> Currency { get; set; }
    }
}
