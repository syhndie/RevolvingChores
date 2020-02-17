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
    public class SetPasswordModel : BasePageModel
    {
        private readonly UserManager<RotatingChoresUser> _userManager;
        private readonly SignInManager<RotatingChoresUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;

        public SetPasswordModel(
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

        public bool IsEmailConfirmed { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);

            if (hasPassword)
            {
                return RedirectToPage("./ChangePassword");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                DangerMessage = "An error occurred when adding password.";
                return RedirectToPage();

            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                DangerMessage = "Unable to load user.";
                return RedirectToPage();
            }

            var addPasswordResult = await _userManager.AddPasswordAsync(user, Input.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                foreach (var error in addPasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = user.Id, code },
                protocol: Request.Scheme);

            await _emailSender.SendEmailAsync(user.Email, "Verify your email",
                $"Please verify your local account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
            
            await _signInManager.RefreshSignInAsync(user);

            SuccessMessage = "Your password has been set. An email has been sent to the address you provided when creating this account. " +
                "Please click on the link in that email to verify your address, and create a local account. " +
                "Once your address has been verified, you may login using your email address and password. " +
                "You may still continue to login using your Google account as well.";

            return RedirectToPage();
        }
    }
}
