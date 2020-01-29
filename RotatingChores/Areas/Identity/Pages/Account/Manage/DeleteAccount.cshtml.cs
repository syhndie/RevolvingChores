using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RotatingChores.Models;
using RotatingChores.Areas.Identity.Data;

namespace RotatingChores.Areas.Identity.Pages.Account.Manage
{
    public class DeleteAccountModel : BasePageModel
    {
        private readonly UserManager<RotatingChoresUser> _userManager;
        private readonly SignInManager<RotatingChoresUser> _signInManager;
        private readonly ILogger<DeleteAccountModel> _logger;

        public DeleteAccountModel(
            UserManager<RotatingChoresUser> userManager,
            SignInManager<RotatingChoresUser> signInManager,
            ILogger<DeleteAccountModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public void  OnGet()
        {
         
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("../Login");
            }

            if (!await _userManager.CheckPasswordAsync(user, Input.Password))
            {
                DangerMessage = "Password not correct.";
                return RedirectToPage();
            }

            var result = await _userManager.DeleteAsync(user);
            var userId = await _userManager.GetUserIdAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleteing user with ID '{userId}'.");
            }

            await _signInManager.SignOutAsync();

            _logger.LogInformation("User with ID '{UserId}' deleted themselves.", userId);

            return Redirect("~/");
        }
    }
}