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
    public class IndexModel : PageModel
    {
        private readonly RotatingChores.Data.ApplicationDbContext _context;

        public IndexModel(RotatingChores.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Chore> Chore { get;set; }

        public async Task OnGetAsync()
        {
            Chore = await _context.Chores.ToListAsync();
        }
    }
}
