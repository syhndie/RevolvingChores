using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RotatingChores.Models;
using Microsoft.AspNetCore.Identity;

namespace RotatingChores.Pages
{
    public class IndexModel : BasePageModel
    {
        //private readonly UserManager<IdentityUser> _userMangaer;
        //private readonly SignInManager<IdentityUser> _signInManager;

        //public IndexModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
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

//@using Microsoft.AspNetCore.Identity
// @inject SignInManager<IdentityUser> SignInManager
//@inject UserManager<IdentityUser> UserManager

//<ul class="navbar-nav">
//    @if(SignInManager.IsSignedIn(User))
//{
//        < li class="nav-item">
//            <a class="nav-link text-dark" asp-area="Identity" asp-page="/Account/Manage/Index" title="Manage">Hello @User.Identity.Name!</a>
//        </li>
//        <li class="nav-item">
//            <form class="form-inline" asp-area="Identity" asp-page="/Account/Logout" asp-route-returnUrl="@Url.Page("/", new { area = "" })" method="post">
//                <button type = "submit" class="nav-link btn btn-link text-dark">Logout</button>
//            </form>
//        </li>
//    }
//    else
//    {
//        <li class="nav-item">
//            <a class="nav-link text-dark" asp-area="Identity" asp-page="/Account/Register">Register</a>
//        </li>
//        <li class="nav-item">
//            <a class="nav-link text-dark" asp-area="Identity" asp-page="/Account/Login">Login</a>
//        </li>
//    }
//</ul>

