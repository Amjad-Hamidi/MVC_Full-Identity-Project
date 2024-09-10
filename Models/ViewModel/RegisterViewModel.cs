using System.ComponentModel.DataAnnotations;

namespace Identity1.Models.ViewModel
{
    public class RegisterViewModel    // database ما بتعامل ابدا مع  View فقط بتعامل مع ال 
    {
        [Required]
        [EmailAddress]
        [MaxLength(40)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(40)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(40)]
        [Compare(nameof(Password))] // اذا تطابقوا او لا Password مع  ConfirmPassword هيك من حاله بفهم لازم يقارن ال 
        public string ConfirmPassword { get; set; }
        public string Phone { get; set; }
        public string City { get; set; } 
        public string Gender { get; set; } 

    }
}
