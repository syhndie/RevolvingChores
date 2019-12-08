using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RotatingChores.Data;
using RotatingChores.Models;

namespace RotatingChores.Pages.Chores
{
    public class EditModel : PageModel
    {
        private readonly RotatingChores.Data.ApplicationDbContext _context;

        public EditModel(RotatingChores.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Chore Chore { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Chore = await _context.Chores.FirstOrDefaultAsync(m => m.ID == id);

            if (Chore == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Chore).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChoreExists(Chore.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ChoreExists(int id)
        {
            return _context.Chores.Any(e => e.ID == id);
        }
    }
}
