using IngredientsCalculations.Enums;
using IngredientsCalculations.Models.ModelBase;
using System.ComponentModel.DataAnnotations.Schema;

namespace IngredientsCalculations.Models
{
    public class ProductIngredients : BaseModel
    {
        public Guid ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
        public Guid IngredientId { get; set; }
        [ForeignKey(nameof(IngredientId))]
        public Ingredients Ingredient { get; set; }
        public scaleType Scale { get; set; }
        public double weight { get; set; }

    }
}
