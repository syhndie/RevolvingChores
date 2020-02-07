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

        public IActionResult OnGet()
        {
            return RedirectToPage("./Chores/Index");
        }
    }
}