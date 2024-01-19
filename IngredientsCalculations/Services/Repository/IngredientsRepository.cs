using IngredientsCalculations.AppDbContext;
using IngredientsCalculations.Models;
using IngredientsCalculations.Services.IRepository;
using IngredientsCalculations.Services.Repository.Base;

namespace IngredientsCalculations.Services.Repository
{
    public class IngredientsRepository : BaseRepository<Ingredients>, IIngredientsRepository
    {
        private readonly ApplicationDbContext _context;
        public IngredientsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
