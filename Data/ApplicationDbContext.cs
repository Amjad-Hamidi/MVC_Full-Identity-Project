using Identity1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity1.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>   // ApplicationUser بدي اغيره ل  <IdentityUser> الافتراضي بدون ال 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
    }
}
