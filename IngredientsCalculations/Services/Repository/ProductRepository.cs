using IngredientsCalculations.AppDbContext;
using IngredientsCalculations.Models;
using IngredientsCalculations.Services.IRepository;
using IngredientsCalculations.Services.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace IngredientsCalculations.Services.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<IEnumerable<Product>> GetAll()
        {
            var allProducts = await _context.Products
                .Include(i => i.ProductIngredients)
                .OrderBy(p => p.productName) // Order by Product Name in ascending order
                .ToListAsync();

            return allProducts;
        }

        public override async Task<Product> GetById(Guid id)
        {
            var productById = await _context.Products
              .Include(p => p.ProductIngredients)
              .ThenInclude(i=> i.Ingredient).FirstOrDefaultAsync(d => d.Id == id);

            return productById; 
        }
    }
}
