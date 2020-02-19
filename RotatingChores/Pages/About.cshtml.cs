using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using RotatingChores.Areas.Identity.Data;
using RotatingChores.Models;
using RotatingChores.Data;

namespace RotatingChores.Pages
{
    [AllowAnonymous]
    public class AboutModel : BasePageModel
    {
        private readonly SignInManager<RotatingChoresUser> _signInManager;

        private readonly UserManager<RotatingChoresUser> _userMangaer;

        private readonly ApplicationDbContext _context;

        public bool IsSignedIn { get; set; }

        public bool FoundAChore { get; set; }

        public AboutModel(SignInManager<RotatingChoresUser> signInManager, UserManager<RotatingChoresUser> userManager, ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _userMangaer = userManager;
            _context = context;
        }

        public void OnGet()
        {
            IsSignedIn = _signInManager.IsSignedIn(User);

            FoundAChore = false;

            if (IsSignedIn)
            {
                string userid = _userMangaer.GetUserId(User);

                FoundAChore = _context.Chores.Any(ch => ch.RotatingChoresUserID == userid);
            }
        }
    }
}