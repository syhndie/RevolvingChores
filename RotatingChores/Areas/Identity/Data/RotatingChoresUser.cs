using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using RotatingChores.Models;

namespace RotatingChores.Areas.Identity.Data
{
    public class RotatingChoresUser : IdentityUser
    {
        public ICollection<Chore> Chores { get; set; }
    }
}
