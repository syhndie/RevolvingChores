using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RotatingChores.Data;
using RotatingChores.Models;

namespace RotatingChores.Pages.NewFolder.Chores
{
    public class DetailsModel : PageModel
    {
        private readonly RotatingChores.Data.ApplicationDbContext _context;

        public DetailsModel(RotatingChores.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
