using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text.Encodings.Web;
using RotatingChores.Models;

namespace RotatingChores.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResendVerificationModel : BasePageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ResendVerificationModel(UserManager<IdentityUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);

                if (user == null)
                {
                    // Don't reveal that the user does not exist   
                    SuccessMessage = "If the email address you entered is associated with an unverified account, a new verification email message was sent. " +
                        "Click on the link in the email message to verify your account.";

                    return RedirectToPage("./Login");
                }

                if (await _userManager.IsEmailConfirmedAsync(user))
                {
                    // Don't reveal that user does exist
                    SuccessMessage = "If the email address you entered is associated with an unverified account, a new verification email message was sent. " +
                        "Click on the link in the email message to verify your account.";

                    return RedirectToPage("./Login");
                }


                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { userId = user.Id, code },
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(
                    Input.Email,
                    "Verify your RotatingChores account email address",
                    $"Please verify the email you provided to RotatingChores by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                // Don't reveal that the user does exist
                SuccessMessage = "If the email address you entered is associated with an unverified account, a new verification email message was sent. " +
                    "Click on the link in the email message to verify your account.";
                
                return RedirectToPage("./Login");
            }

            SuccessMessage = "If the email address you entered is associated with an unverified account, a new verification email message was sent. " +
                "Click on the link in the email message to verify your account.";

            return RedirectToPage("./Login");
        }
    }
}