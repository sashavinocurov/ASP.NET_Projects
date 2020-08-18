using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
namespace Chefs_Dishes.Models{
    public class Dish{
    [Key]
    public int DishId {get;set;}
    [Required]
    [MinLength(2)]
    public string Name {get;set;}
    [Required]
    [Range(0,5000)]
    public int Calories {get;set;}
    [Required]
    [Range(1,5)]
    public int Tastiness {get;set;}
    [Required]
    [MinLength(10)]
    public string Description {get;set;}
    public int ChefId {get;set;}
    public Chef Creator {get;set;}
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}