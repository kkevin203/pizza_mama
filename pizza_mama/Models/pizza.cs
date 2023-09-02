using System.ComponentModel.DataAnnotations;

namespace pizza_mama.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        [Display(Name = "Nom")]
        public string Name { get; set; }
        [Display(Name = "Prix (€)")]
        public float Price { get; set; }

        [Display(Name = "Végétarienne")]
        public bool Vegetarian { get; set; }
        [Display(Name = "Ingrédients")]
        public string? Ingredient { get; set; }

    }
}
