using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using pizza_mama.Data;
using pizza_mama.Models;

namespace pizza_mama.Pages.Admin.Pizzas
{
    [Authorize]

    public class DeleteModel : PageModel
    {
        private readonly pizza_mama.Data.DataContext _context;

        public DeleteModel(pizza_mama.Data.DataContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Pizza Pizza { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Pizzas == null)
            {
                return NotFound();
            }

            var pizza = await _context.Pizzas.FirstOrDefaultAsync(m => m.Id == id);

            if (pizza == null)
            {
                return NotFound();
            }
            else 
            {
                Pizza = pizza;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Pizzas == null)
            {
                return NotFound();
            }
            var pizza = await _context.Pizzas.FindAsync(id);

            if (pizza != null)
            {
                Pizza = pizza;
                _context.Pizzas.Remove(Pizza);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
