using IngredientsCalculations.Models.ModelBase;
using System.ComponentModel.DataAnnotations;

namespace IngredientsCalculations.Models
{
    public class Ingredients : BaseModel
    {
        [Required]
        public string ingredientName { get; set; }
        public string? scientificName { get; set; } = "N/A";
        public string? scientificNumber { get; set; } = "N/A";

        public ICollection<ProductIngredients> ProductIngredients { get; set; }

    }
}
