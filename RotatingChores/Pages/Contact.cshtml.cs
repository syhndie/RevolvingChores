using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RotatingChores.Models;
using Microsoft.AspNetCore.Authorization;

namespace RotatingChores.Pages
{
    [AllowAnonymous]
    public class ContactModel : BasePageModel
    {
        public void OnGet()
        {

        }
    }
}