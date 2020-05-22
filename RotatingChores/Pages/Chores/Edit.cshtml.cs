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
using Microsoft.AspNetCore.Identity;
using RotatingChores.Areas.Identity.Data;

namespace RotatingChores.Pages.Chores
{
    public class EditModel : BasePageModel
    {
        private readonly UserManager<RotatingChoresUser> _userManager;

        private readonly ApplicationDbContext _context;

        public Chore Chore { get; set; }

        public EditModel(UserManager<RotatingChoresUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Chore = await _context.Chores
                .Where(ch => ch.RotatingChoresUserID == _userManager.GetUserId(User))
                .SingleOrDefaultAsync(ch => ch.ID == id);

            if (Chore == null)
            {
                DangerMessage = "The chore you tried to update has been deleted.";
                return RedirectToPage("./Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string userId = _userManager.GetUserId(User);

            Chore = await _context.Chores
                .Where(ch => ch.RotatingChoresUserID == userId)
                .SingleOrDefaultAsync(ch => ch.ID == id);

            if (Chore is null)
            {
                DangerMessage = "The chore you tried to update has been deleted.";
                return RedirectToPage("./Index");
            }

            var modelDidUpdate = await TryUpdateModelAsync(
                Chore,
                "",
                c => c.Name,
                c => c.DueDate,
                c => c.FrequencyValue,
                c => c.FrequencyUnits,
                c => c.Notes,
                c => c.IsHighPriority);

            if (modelDidUpdate)
            {
                List<string> usedChoreNames = _context.Chores
                    .Where(ch => ch.RotatingChoresUserID == userId)
                    .Where(ch => ch.ID != id)
                    .Select(ch => ch.Name)
                    .ToList();

                if (usedChoreNames.Contains(Chore.Name))
                {
                    DangerMessage = $"The Chore '{Chore.Name}' already exists.";

                    return RedirectToPage(new { id });
                }


                Chore.DateLastModiied = DateTime.Now;
                _context.Attach(Chore).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                SuccessMessage = $"'{Chore.Name}' chore successfully updated.";
                return RedirectToPage("./Index");
            }

            DangerMessage = "Chore did not update successfully.";
            return RedirectToPage(new { id });
        }
    }
}