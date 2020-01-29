using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RotatingChores.Models;
using Microsoft.AspNetCore.Identity;
using RotatingChores.Areas.Identity.Data;

namespace RotatingChores.Pages
{
    public class IndexModel : BasePageModel
    {
        //private readonly UserManager<RotatingChoresUser> _userMangaer;
        //private readonly SignInManager<RotatingChoresUser> _signInManager;

        //public IndexModel(UserManager<RotatingChoresUser> userManager, SignInManager<RotatingChoresUser> signInManager)
        //{
        //    _userMangaer = userManager;
        //    _signInManager = signInManager;
        //}

        public IActionResult OnGet()
        {
            //if (_signInManager.IsSignedIn(User))
            //{
            //    return RedirectToPage("./Chores/Index");
            //}
            //else
            //{
            return Page();
            //}

        }
    }
}