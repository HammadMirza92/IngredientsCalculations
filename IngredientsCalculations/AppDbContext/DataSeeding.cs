using IngredientsCalculations.Models;
using Microsoft.EntityFrameworkCore;

namespace IngredientsCalculations.AppDbContext
{
    public class DataSeeding
    {
        public static async Task Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();
                context.Database.Migrate(); // apply all migrations


                if (!context.Products.Any())
                {
                    /*----------------------- Currency  ----------------------------------*/
                    var currency1 = new Currency()
                    {
                        Id = Guid.Parse("c1443ed2-4d7e-4536-b20c-b1fe299f83a4"),
                        Code = "USD",
                        ExchangeRate = 1.00,
                        IsBase = true,
                    };
                    var addCurrency1 = context.Currency.Add(currency1).Entity;
                     
                    var currency2 = new Currency()
                    {
                        Id = Guid.Parse("e0afc496-90bb-4cca-a467-8593eb6b72e2"),
                        Code = "EUR",
                        ExchangeRate = 1.09
                    };
                    var addCurrency2 = context.Currency.Add(currency2).Entity;

                    /*----------------------- Products  ----------------------------------*/

                    var product1 = new Product()
                    {
                        Id = Guid.Parse("609ce6fa-1d5d-4234-9a69-dd866b5cb1e9"),
                        productName = "Demo product 1",
                        weight = "12kg",
                        BasePrice = 12.99,
                    };

                    var addProduct1 = context.Products.Add(product1).Entity;

                    /*----------------------- Ingredients  ----------------------------------*/
                    var ingredient1 = new Ingredients()
                    {
                        Id = Guid.Parse("1ff68791-16b2-40fd-b33e-fbbd0a608fe2"),
                        ingredientName = "calcium 1",
                    };
                    var addIngredient1 = context.Ingredients.Add(ingredient1).Entity;
                    var ingredient2 = new Ingredients()
                    {
                        Id = Guid.Parse("f6f20893-d41c-4efe-b73f-4775b9eaadad"),
                        ingredientName = "calcium 2",
                    };
                    var addIngredient2 = context.Ingredients.Add(ingredient2).Entity;
                    var ingredient3 = new Ingredients()
                    {
                        Id = Guid.Parse("6872c9b2-81b3-42ca-9a6d-b9559ee57e4a"),
                        ingredientName = "calcium 3",
                    };
                    var addIngredient3 = context.Ingredients.Add(ingredient3).Entity;

                    /*----------------------- Products Ingredients  ----------------------------------*/
                    var productIngredient1 = new ProductIngredients()
                    {
                        Id = Guid.Parse("14bc2ae6-b879-4fd4-aeda-d2dc258652aa"),
                        ProductId = Guid.Parse("609ce6fa-1d5d-4234-9a69-dd866b5cb1e9"),
                        IngredientId = Guid.Parse("1ff68791-16b2-40fd-b33e-fbbd0a608fe2"),
                        Scale = Enums.scaleType.Gram,
                        weight = 20,
                    };
                    var addproductIngredient1 = context.ProductIngredients.Add(productIngredient1).Entity;

                    var productIngredient2 = new ProductIngredients()
                    {
                        Id = Guid.Parse("c9898986-dfdf-4b6f-bdcb-beeece9e0859"),
                        ProductId = Guid.Parse("609ce6fa-1d5d-4234-9a69-dd866b5cb1e9"),
                        IngredientId = Guid.Parse("f6f20893-d41c-4efe-b73f-4775b9eaadad"),
                        Scale = Enums.scaleType.Gram,
                        weight = 25,
                    };
                    var addproductIngredient2 = context.ProductIngredients.Add(productIngredient2).Entity;

                    context.SaveChanges();

                }
            }
        }
    }
}
