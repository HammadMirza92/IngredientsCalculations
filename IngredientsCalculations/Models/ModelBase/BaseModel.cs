using System.ComponentModel.DataAnnotations;

namespace IngredientsCalculations.Models.ModelBase
{
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; }
    }
}
