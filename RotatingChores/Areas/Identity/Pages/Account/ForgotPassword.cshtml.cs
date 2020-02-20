using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RotatingChores.Models;
using RotatingChores.Areas.Identity.Data;

namespace RotatingChores.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : BasePageModel
    {
        private readonly UserManager<RotatingChoresUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(UserManager<RotatingChoresUser> userManager, IEmailSender emailSender)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user))) 
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    SuccessMessage = "If the email address you entered is associated with a verified account, instructions to reset your password were sent.";
                    return RedirectToPage("./Login");
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { code },
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(
                    Input.Email,
                    "Reset password",
                    $"Please reset your RevolvingChores password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                SuccessMessage = "If the email address you entered is associated with a verified account, instructions to reset your password were sent.";
                return RedirectToPage("./login");
            }

            SuccessMessage = "If the email address you entered is associated with a verified account, instructions to reset your password were sent.";
            return RedirectToPage("./Login");
        }
    }
}
