using IngredientsCalculations.AppDbContext;
using IngredientsCalculations.Models;
using IngredientsCalculations.Services.IRepository;
using IngredientsCalculations.Services.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace IngredientsCalculations.Services.Repository
{
    public class CurrencyRepository : BaseRepository<Currency>, ICurrencyRepository
    {
        private readonly ApplicationDbContext _context;
        public CurrencyRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public override async Task Update(Currency entity)
        {

            // Retrieve the existing entity from the database
            var existingEntity = await _context.Currency.FindAsync(entity.Id);

            if (existingEntity == null)
            {
                // Handle the case where the entity with the given Id is not found
                // You might want to return an error or handle it based on your application's logic
                return;
            }

            // Update the properties of the existing entity with the new values
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);

            // If the entity is marked as the new base currency, update other currencies
            if (entity.IsBase)
            {
                var otherCurrencies = await _context.Currency
                    .Where(c => c.Id != entity.Id && c.IsBase)
                    .ToListAsync();

                foreach (var currency in otherCurrencies)
                {
                    currency.IsBase = false;
                }
            }

            // Save changes to the database
            await _context.SaveChangesAsync();
        }
        public async Task<Currency> GetBaseCurrency()
        {
            return await _context.Currency.FirstOrDefaultAsync(c => c.IsBase);
        }

    }
}
