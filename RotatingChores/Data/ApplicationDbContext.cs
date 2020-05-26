using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RotatingChores.Models;
using Microsoft.AspNetCore.Identity;
using RotatingChores.Areas.Identity.Data;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RotatingChores.Data
{
    public class ApplicationDbContext : IdentityDbContext<RotatingChoresUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Chore>()
                .HasIndex(ch => new { ch.RotatingChoresUserID, ch.Name})
                .IsUnique();

            base.OnModelCreating(builder);
        }
        public DbSet<Chore> Chores { get; set; }
        
    }
}
