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

namespace RotatingChores.Pages.Chores
{
    public class IndexModel : BasePageModel
    {
        private readonly UserManager<IdentityUser> _userMangaer;

        private readonly ApplicationDbContext _context;

        public IList<Chore> Chores { get; set; }

        public IndexModel(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userMangaer = userManager;
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Chores = await _context.Chores
                .Where(ch => ch.UserID == _userMangaer.GetUserId(User))
                .OrderBy(ch => ch.DueDate)
                .ThenByDescending(ch => ch.IsHighPriority)
                .ThenBy(ch => ch.Name)
                .ToListAsync();
        }
    }
}
