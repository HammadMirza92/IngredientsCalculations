using IngredientsCalculations.AppDbContext;
using IngredientsCalculations.Models;
using IngredientsCalculations.Services.IRepository;
using IngredientsCalculations.Services.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace IngredientsCalculations.Services.Repository
{
    public class ProductIngredientsRepository : BaseRepository<ProductIngredients>, IProductIngredientsRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductIngredientsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<IEnumerable<ProductIngredients>> GetAll()
        {
            var allProductIngredients = await _context.ProductIngredients
                .Include(p => p.Product)
                .Include(i => i.Ingredient)
                .OrderBy(pi => pi.Product.productName) // Order by Product Name in ascending order
                .ToListAsync();

            return allProductIngredients;
        }

        public override async Task<ProductIngredients> GetById(Guid id)
        {
            var productIngredientsById = await _context.ProductIngredients
              .Include(p => p.Product)
              .Include(i => i.Ingredient).FirstOrDefaultAsync(d=> d.Id == id);

            return productIngredientsById;
        }

    }
}
