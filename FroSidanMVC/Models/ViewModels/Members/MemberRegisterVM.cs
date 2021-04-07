using System.ComponentModel.DataAnnotations;

namespace FroSidanMVC.Models
{
    public class MemberRegisterVM
    {
        [StringLength(100, ErrorMessage = "Lösenordet måste vara minst 6 tecken långt.", MinimumLength = 6)]
        [Required(ErrorMessage = "Ange ett lösenord")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Matchar ej angivet lösenord")]
        [DataType(DataType.Password)]
        [Display(Name = "Repeat password")]
        [Compare(nameof(MemberRegisterVM.Password))]
        public string PasswordRepeat { get; set; }

        public string UserName { get; set; }
        [Required(ErrorMessage = "Ej korrekt angiven epostadress")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Ange förnamn")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Ange efternamn")]
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }


    }
}