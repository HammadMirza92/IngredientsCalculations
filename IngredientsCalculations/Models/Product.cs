using IngredientsCalculations.Models.ModelBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IngredientsCalculations.Models
{
    public class Product : BaseModel
    {
        [Required]
        public string productName { get; set; }
        public string weight { get; set; }

        // Base Price in Base Currency
        public double BasePrice { get; set; }
        [NotMapped]
        public string BaseCurrency { get; set; }
        public ICollection<ProductIngredients> ProductIngredients { get; set; }

    }
}
