using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RotatingChores.Areas.Identity.Data;
using RotatingChores.Models;
using RotatingChores.Data;

namespace RotatingChores.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : BasePageModel
    {
        private readonly UserManager<RotatingChoresUser> _userManager;
        private readonly SignInManager<RotatingChoresUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;

        public IndexModel(
            UserManager<RotatingChoresUser> userManager,
            SignInManager<RotatingChoresUser> signInManager,
            ApplicationDbContext context,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _emailSender = emailSender;
        }

        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                DangerMessage = "Unable to load user";
                return RedirectToPage();
            }

            var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);

            Username = userName;

            Input = new InputModel
            {
                Email = email
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                DangerMessage = "An error occurred when changing email address.";
                return RedirectToPage();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                DangerMessage = "Unable to load user.";
                return RedirectToPage();
            }

            var oldemail = await _userManager.GetEmailAsync(user);
            var newemail = Input.Email;

            if (newemail != oldemail)
            {
                if (_context.Users.Any(u => u.Email == newemail))
                {
                    DangerMessage = "An error occurred when changing your email address.The new email address may already exist in our system.";
                    return RedirectToPage();
                }

                user.PendingEmail = newemail;
                await _userManager.UpdateAsync(user);

                var changeEmailToken = await _userManager.GenerateChangeEmailTokenAsync(user, Input.Email);

                var callbackUrl = Url.Page(
                    "/Account/ConfirmChangedEmail",
                    pageHandler: null,
                    values: new { userID = user.Id, changeEmailToken, newemail},
                    protocol: Request.Scheme
                    );

                await _emailSender.SendEmailAsync(
                    newemail,
                    "Verify your RevolvingChores account new email address.",
                    $"Please verify your RevolvingChores account new email address by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>."
                    );

                SuccessMessage = "An email has been sent to the new address you provided." +
                        "Please click on the link in that email to verify your new address." +
                        "Once the new address has been verified, you may login with that address.";

                return RedirectToPage();
            }

            //If we got this far, the email address entered was the same as the old address
            DangerMessage = "The email address you entered is the same as the address in our database.";
            return RedirectToPage();
        }
    }
}
