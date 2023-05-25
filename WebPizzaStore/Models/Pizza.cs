namespace WebPizzaStore.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

public class Pizza
{
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }
    public double? Price { get; set; }
    public string? Size { get; set;}
    public bool IsGlutenFree { get; set; }
    public Sauce? Sauce { get; set; }
    // [BindProperty]
    // public Pizza? pizza { get; set; }
    
    public ICollection<Topping>? Toppings { get; set; }
}