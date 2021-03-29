using System.ComponentModel.DataAnnotations;

namespace FroSidanMVC.Models
{
    public class CreateVM
    {
        
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Repeat password")]
        [Compare(nameof(CreateVM.Password))]
        public string PasswordRepeat { get; set; }

    }
}