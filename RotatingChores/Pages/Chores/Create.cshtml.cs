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
using RotatingChores.Models.ViewModels;

namespace RotatingChores.Pages.NewFolder.Chores
{
    public class CreateModel : BasePageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly ApplicationDbContext _context;

        [BindProperty]
        public ChoreVM NewChoreVM { get; set; }

        public CreateModel(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                DangerMessage = "Your new chore did not save correctly.";
                return RedirectToPage();
            }

            Chore newChore = new Chore
            {
                UserID = _userManager.GetUserId(User),
                Name = NewChoreVM.Name,
                Description = NewChoreVM.Description,
                DaysToRepeat = NewChoreVM.DaysToRepeat,
                DateLastCompleted = NewChoreVM.DateLastCompleted,
                Priority = NewChoreVM.Priority
            };

            if (newChore.UserID == null)
            {
                DangerMessage = "Your new chore did not save correctly.";
                return RedirectToPage();
            }

            List<string> usedNames = _context.Chores
                .Where(ch => ch.UserID == newChore.UserID)
                .Select(ch => ch.Name)
                .ToList();

            if (usedNames.Contains(newChore.Name))
            {
                DangerMessage = "This Chore name is already used.";

                return RedirectToPage();
            }

            _context.Chores.Add(newChore);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}