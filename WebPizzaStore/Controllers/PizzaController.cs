using WebPizzaStore.Models;
using WebPizzaStore.Services;
using Microsoft.AspNetCore.Mvc;
using WebPizzaStore.Data;
using Microsoft.EntityFrameworkCore;

namespace WebPizzaStore.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : Controller
{   
    private readonly PizzaContext _context;
    PizzaService myPizzaService;
    
    public PizzaController(PizzaService PizzaService, PizzaContext _context)
    {
        myPizzaService = PizzaService;
        this._context = _context;
    }

    [HttpGet("index")]
     public async Task<IActionResult> Index()
    {
        var pizzas = await _context.Pizzas
        
        .ToListAsync();
        return View(pizzas);
    }

    // GET all pizzas action
    [HttpGet("list")]
    public async Task<ActionResult> List(int id) {
        
        Pizza? pizza = await _context.Pizzas.FirstOrDefaultAsync(p => p.Id == id);

        if (pizza == null)
        {
            return NotFound();
        }

        return View(pizza);
    }

    // GET by Id action
    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id)
    {
        var pizza = myPizzaService.Get(id);

        if(pizza == null)
            return NotFound();

        return pizza;
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST action
    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {
        if (ModelState.IsValid)
        {
            myPizzaService.Add(pizza);
            return RedirectToAction("Index");
        }

        return View(pizza);
    }

    // PUT action
    [HttpPut("{id}")]
    public IActionResult Edit(int id, Pizza pizza)
    {
        // This code will update the pizza and return a result
        if (id != pizza.Id)
        return BadRequest();
           
        var existingPizza = myPizzaService.Get(id);
        if(existingPizza is null)
            return NotFound();

        myPizzaService.Update(pizza, id);           
        return NoContent();
    }

    // Get /Pizza/Delete/id
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }
        Pizza? pizza = await _context.Pizzas.FirstOrDefaultAsync(p => p.Id == id);
        if(pizza == null)
        {
            return NotFound();
        }
        return View(pizza);
    }

    // DELETE action    
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]    
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        // This code will delete the pizza and return a result
       
        var pizza = _context.Pizzas.FirstOrDefault(p => p.Id == id);

        if(pizza == null)
        {
            return NotFound();
        }
    
        // myPizzaService.Delete(id);
        // await _context.SaveChangesAsync();
        _context.Pizzas.Remove(pizza);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

}