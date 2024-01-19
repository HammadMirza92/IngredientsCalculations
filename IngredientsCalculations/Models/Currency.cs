using IngredientsCalculations.Models.ModelBase;
using Microsoft.EntityFrameworkCore;

namespace IngredientsCalculations.Models
{
    public class Currency : BaseModel
    {
        public string Code { get; set; } // Currency Code (e.g., USD, EUR, GBP)
        public double ExchangeRate { get; set; } // Exchange rate against the base currency (e.g., USD)
        public bool IsBase { get; set; } // Indicates whether this currency is the base currency
        public void SetAsBaseCurrency(List<Currency> allCurrencies)
        {
            if (IsBase)
            {
                // If this currency is set as base, set IsBase to false for all other currencies
                foreach (var currency in allCurrencies)
                {
                    if (currency.Id != Id)
                    {
                        currency.IsBase = false;
                    }
                }
            }
        }

    }
}
