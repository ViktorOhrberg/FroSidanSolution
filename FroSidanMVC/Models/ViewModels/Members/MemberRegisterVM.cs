using System.ComponentModel.DataAnnotations;

namespace FroSidanMVC.Models
{
    public class MemberRegisterVM
    {
        [Required(ErrorMessage = "Ange förnamn")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Ange efternamn")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Ange korrekt epostadress")]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "Lösenordet måste vara minst 6 tecken långt.", MinimumLength = 6)]
        [Required(ErrorMessage = "Ange ett lösenord")]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,15}$", ErrorMessage = "Måste innehålla minst 1 STOR bokstav, 1 liten bokstav samt 1 siffra")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Matchar ej angivet lösenord")]
        [DataType(DataType.Password)]
        [Display(Name = "Repeat password")]
        [Compare(nameof(MemberRegisterVM.Password))]
        public string PasswordRepeat { get; set; }

        public string UserName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }


    }
}