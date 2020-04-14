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
using System.ComponentModel.DataAnnotations;
using RotatingChores.Areas.Identity.Data;
using RotatingChores.Helpers;

namespace RotatingChores.Pages.Chores
{
    public class IndexModel : BasePageModel
    {
        private readonly UserManager<RotatingChoresUser> _userMangaer;

        private readonly ApplicationDbContext _context;

        public List<Chore> Chores { get; set; }

        [BindProperty]
        public int ChoreID { get; set; }

        public IndexModel(UserManager<RotatingChoresUser> userManager, ApplicationDbContext context)
        {
            _userMangaer = userManager;
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            string userid = _userMangaer.GetUserId(User);

            List<SortableChore> sortableChores = await _context.Chores
                .Where(ch => ch.RotatingChoresUserID == userid)
                .Select(ch => new SortableChore(ch, false))
                .ToListAsync();

            foreach (SortableChore sortableChore in sortableChores)
            {
                if (sortableChore.Chore.DueDate <= DateTime.Today && sortableChore.Chore.IsHighPriority)
                {
                    sortableChore.ListFirst = true;
                }
            }

            Chores = sortableChores
                .OrderByDescending(sc => sc.ListFirst)
                .ThenBy(sc => sc.Chore.DueDate)
                .ThenByDescending(sc => sc.Chore.IsHighPriority)
                .ThenBy(sc => sc.Chore.Name)
                .Select(sc => sc.Chore)
                .ToList();            

            return Page();
        }

        public async Task<IActionResult> OnPostUpdateAsync()
        {
            string userID = _userMangaer.GetUserId(User);

            Chore choreToEdit = await _context.Chores
                .Where(ch => ch.RotatingChoresUserID == userID)
                .Where(ch => ch.ID == ChoreID)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            try
            {
                choreToEdit.DueDate = choreToEdit.FrequencyUnits switch
                {
                    TimeIntervals.days => DateTime.Today.AddDays(choreToEdit.FrequencyValue),
                    TimeIntervals.weeks => DateTime.Today.AddDays(choreToEdit.FrequencyValue * 7),
                    TimeIntervals.months => DateTime.Today.AddMonths(choreToEdit.FrequencyValue),
                    TimeIntervals.years => DateTime.Today.AddYears(choreToEdit.FrequencyValue),
                    _ => throw new ArgumentException("Chore has invalid Frequency Unit")
                };
            }
            catch
            {
                if (choreToEdit is null)
                {
                    DangerMessage = "The chore you tried to update has been deleted.";
                    return RedirectToPage();
                }
                else
                {
                    throw;
                }
            }

            _context.Attach(choreToEdit).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            SuccessMessage = $"'{choreToEdit.Name}' chore Date Last Completed updated to today.";
           
            return RedirectToPage();
        }
    }
}
