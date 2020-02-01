using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using RotatingChores.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RotatingChores.Data;
using RotatingChores.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Transactions;

namespace RotatingChores.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmChangedEmailModel : BasePageModel
    {
        private readonly UserManager<RotatingChoresUser> _userManager;
        private readonly ApplicationDbContext _context;
        
        public ConfirmChangedEmailModel(
            UserManager<RotatingChoresUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(string userId, string changeEmailToken, string newemail)
        {
            if (userId == null || changeEmailToken == null || newemail == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                DangerMessage = "Unable to load user.";
                return RedirectToPage("/Index");
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var oldemail = await _userManager.GetEmailAsync(user);
                    if (newemail != oldemail)
                    {
                        IdentityResult changeEmailResult = await _userManager.ChangeEmailAsync(user, newemail, changeEmailToken);
                        IdentityResult changeNameResult = await _userManager.SetUserNameAsync(user, newemail);
                        user.PendingEmail = null;
                        await _userManager.UpdateAsync(user);                      

                        if (changeEmailResult.Succeeded && changeNameResult.Succeeded)
                        {
                            transaction.Commit();
                            SuccessMessage = "Thank you for verifying your new email address.";
                            return RedirectToPage("./Login");
                        }

                        DangerMessage = "An error occurred when changing your email address. Email address was not changed.";
                        return RedirectToPage("./Login");
                    }

                    return RedirectToPage("./Login");
                }
                catch(Exception)
                {
                    throw;
                }
            }
        }
    }
}