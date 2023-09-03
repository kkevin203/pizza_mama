using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace pizza_mama.Models
{
    public class Pizza
    {
        [JsonIgnore]
        public int Id { get; set; }
        [Display(Name = "Nom")]
        public string Name { get; set; }
        [Display(Name = "Prix (€)")]
        public float Price { get; set; }

        [Display(Name = "Végétarienne")]
        public bool Vegetarian { get; set; }
        [Display(Name = "Ingrédients")]
        [JsonIgnore]
        public string? Ingredient { get; set; }

        [NotMapped]
        [JsonPropertyName("Ingrédients")]
        public string[] listIngredients
        {
            get
            {
                if ((Ingredient == null) || (Ingredient.Count() == 0))
                {
                    return null; 
                }
                return Ingredient.Split(", ");
            }
        }

    }
}
