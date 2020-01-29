using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RotatingChores.Data;
using RotatingChores.Models;
using Microsoft.AspNetCore.Identity;
using RotatingChores.Areas.Identity.Data;

namespace RotatingChores.Pages.Chores
{
    public class CreateModel : BasePageModel
    {
        private readonly UserManager<RotatingChoresUser> _userManager;

        private readonly ApplicationDbContext _context;

        public Chore Chore { get; set; }

        public CreateModel(UserManager<RotatingChoresUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Chore newChore = new Chore
            {
                RotatingChoresUserID = _userManager.GetUserId(User)
            };

            var modelDidUpdate = await TryUpdateModelAsync(
               newChore,
               "",
               c => c.Name,
               c => c.DateLastCompleted,
               c => c.FrequencyValue,
               c => c.FrequencyUnits,
               c => c.Notes,
               c => c.IsHighPriority);
            
            if (modelDidUpdate)
            {
                _context.Chores.Add(newChore);
                await _context.SaveChangesAsync();
                SuccessMessage = $"'{newChore.Name}' chore successfully added";
                return RedirectToPage("./Index");
            }

            DangerMessage = "New chore did not save correctly.";
            return RedirectToPage();
        }
    }
}