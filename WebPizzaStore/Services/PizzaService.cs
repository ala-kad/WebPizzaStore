using WebPizzaStore.Models;
using WebPizzaStore.Data;
using Microsoft.EntityFrameworkCore;

namespace WebPizzaStore.Services;

public class PizzaService
{
    // static List<Pizza>? Pizzas { get; }
    // static int nextId = 3;

    private readonly PizzaContext _context;
    // Service constructor & injecting DBContext class
    public PizzaService(PizzaContext context)
    {   _context = context;
        // Pizzas = new List<Pizza>
        // {
        //     new Pizza { Id = 1, Name = "Classic Italian", IsGlutenFree = false },
        //     new Pizza { Id = 2, Name = "Veggie", IsGlutenFree = true }
        // };
    }

    // Get all Pizzas
    public List<Pizza> GetAll()
    {
        return _context.Pizzas
            .AsNoTracking()
            .ToList();
    }

    // Get a pizza by id
    public Pizza? Get(int id) 
    {
        return _context.Pizzas
        .Include(p => p.Toppings)
        .Include(p => p.Sauce)
        .AsNoTracking()
        .SingleOrDefault(p => p.Id == id);
    }

    // Add a new pizza
    public Pizza Add(Pizza newPizza)
    {
       _context.Pizzas.Add(newPizza);
       _context.SaveChanges();

       return newPizza;
    }

    // Delete a pizza by id
    public void Delete(int id)
    {
        var pizzaToDelete = _context.Pizzas.Find(id);
        if(pizzaToDelete is not null)
        {
            _context.Pizzas.Remove(pizzaToDelete);
            _context.SaveChanges();
        }
    }

    public void Update(Pizza updatedPizza, int id)
    {
        var pizzaToUpdate = _context.Pizzas.Find(id);
        if(pizzaToUpdate is not null)
        {
            pizzaToUpdate =updatedPizza;
            _context.SaveChanges();
        }
    }

    // Update Sauce 
    public void UpdateSauce(int pizzaId, int sauceId)
    {
        var pizzaToUpdate = _context.Pizzas.Find(pizzaId);
        var sauceToUpdate = _context.Sauces.Find(sauceId);

        if (pizzaToUpdate is null || sauceToUpdate is null)
        {
            throw new InvalidOperationException("Pizza or sauce does not exist");
        }

        pizzaToUpdate.Sauce = sauceToUpdate;

        _context.SaveChanges();
    }

    // Add a Topping to Pizza
    public void AddTopping(int pizzaId, int toppingId)
    {
        var pizzaToUpdate = _context.Pizzas.Find(pizzaId);
        var toppingToAdd = _context.Toppings.Find(toppingId);

        if (pizzaToUpdate is null || toppingToAdd is null)
        {
            throw new InvalidOperationException("Pizza or topping does not exist");
        }

        if(pizzaToUpdate.Toppings is null)
        {
            pizzaToUpdate.Toppings = new List<Topping>();
        }

        pizzaToUpdate.Toppings.Add(toppingToAdd);

        _context.SaveChanges();
    }
}