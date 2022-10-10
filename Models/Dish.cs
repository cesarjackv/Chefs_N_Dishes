#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
public class Dish
{
    [Key]
    public int DishId {get; set;}

    [Required]
    public int ChefId {get; set;}
    
    public Chef? ChefName {get; set;}

    [Required]
    [Display(Name = "Dish's Name")]
    [MaxLength(45)]
    public string DishName {get; set;}

    [Required]
    [Display(Name = "Calories")]
    [Range(1, 99999)]
    public int Calories {get; set;}

    [Required]
    [Display(Name = "Tastiness")]
    [Range(1, 5)]
    public int Tastiness {get; set;}

    // [Display(Name = "Description")]
    // [MinLength(20)]
    // public string? Description {get; set;}

    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;
}