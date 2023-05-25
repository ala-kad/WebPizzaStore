using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebPizzaStore.Models;
using WebPizzaStore.Services;

namespace WebPizzaStore.Pages
{
    public class PizzaListModel : PageModel
    {
        private readonly PizzaService _service;
        [BindProperty]
        public Pizza NewPizza { get; set; } = default!;
        public IEnumerable<Pizza> PizzaList { get;set; } = default!;

        public PizzaListModel(PizzaService service)
        {
            _service = service;
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || NewPizza == null)
            {
                return Page();
            }

            _service.Add(NewPizza);

            return RedirectToAction("Get");
        }
        public void OnGet()
        {
            PizzaList = _service.GetAll();
        }
        public IActionResult OnPostDelete(int id)
        {
            _service.Delete(id);

            return RedirectToAction("Get");
        }
    }
}
