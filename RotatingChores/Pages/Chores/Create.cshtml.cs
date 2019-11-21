using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RotatingChores.Data;
using RotatingChores.Models;

namespace RotatingChores.Pages.NewFolder.Chores
{
    public class CreateModel : PageModel
    {
        private readonly RotatingChores.Data.ApplicationDbContext _context;

        public CreateModel(RotatingChores.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Chore Chore { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Chores.Add(Chore);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}