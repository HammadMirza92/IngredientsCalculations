using IngredientsCalculations.Models;
using IngredientsCalculations.Services.IRepository.Base;

namespace IngredientsCalculations.Services.IRepository
{
    public interface ICurrencyRepository : IBaseRepository<Currency>
    {
        Task<Currency> GetBaseCurrency();
    }
}
