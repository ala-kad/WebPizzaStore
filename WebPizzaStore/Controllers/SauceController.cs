using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPizzaStore.Data;
using WebPizzaStore.Models;

namespace WebPizzaStore.Controllers
{
    public class SauceController : Controller
    {
        private readonly PizzaContext _context;

        public SauceController(PizzaContext context)
        {
            _context = context;
        }

        // GET: /Sauce
        public async Task<IActionResult> Index()
        {
            List<Sauce> sauces = await _context.Sauces.ToListAsync();
            return View(sauces);
        }

        // GET: /Sauce/Details/5
       public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Sauce? sauce = await _context.Sauces.FirstOrDefaultAsync(s => s.Id == id);

            if (sauce == null)
            {
                return NotFound();
            }

            return View(sauce);
        }

        // GET: /Sauce/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Sauce/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,IsVegan")] Sauce sauce)
        {
            if (ModelState.IsValid)
            {
                _context.Sauces.Add(sauce);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(sauce);
        }

        // GET: /Sauce/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Sauce? sauce = await _context.Sauces.FirstOrDefaultAsync(s => s.Id == id);

            if (sauce == null)
            {
                return NotFound();
            }

            return View(sauce);
        }

        // POST: /Sauce/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IsVegan")] Sauce sauce)
        {
            if (id != sauce.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Entry(sauce).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(sauce);
        }

        // GET: /Sauce/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            Sauce? sauce = await _context.Sauces.FirstOrDefaultAsync(s => s.Id == id);

            if (sauce == null)
            {
                return NotFound();
            }

            return View(sauce);
        }

        // POST: /Sauce/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {   if(id == null)
            {
                return NotFound();
            }
            Sauce? sauce = await _context.Sauces.FirstOrDefaultAsync(s => s.Id == id);
            if(sauce == null)
            {
                return NotFound();
            }
            _context.Sauces.Remove(sauce);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
