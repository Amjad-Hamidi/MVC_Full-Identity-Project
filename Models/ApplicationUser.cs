using Microsoft.AspNetCore.Identity;

namespace Identity1.Models  // Note : this class is made just to add some attribtes to the default attributes that is exists in (IdentityUser)
{
    public class ApplicationUser : IdentityUser // DB هاد بتعامل مع 
    {
        public string? City { get; set; }
        public string? Gender { get; set; }
    }
}
