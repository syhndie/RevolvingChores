using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RotatingChores.Data;
using RotatingChores.Models;
using Microsoft.AspNetCore.Identity;

namespace RotatingChores.Pages.NewFolder.Chores
{
    public class IndexModel : BasePageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public IList<Chore> Chores { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            string userId = _userManager.GetUserId(User);

            Chores = await _context.Chores
                .Where(ch => ch.UserID == userId)
                .AsNoTracking()
                .ToListAsync();

            return Page();
        }
    }
}
