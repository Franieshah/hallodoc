using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HalloDocApp.Entities.ViewModels
{
    public class PatientSignUp
    {
        [DisplayName("Email")]
        public String Username { get; set; }
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
             ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        [DataType(DataType.Password)]
        public String Password { get; set; }


        [Required]
        [Compare("Password",ErrorMessage ="Password and confirm password doesn't match..")]
        public String ConfirmPassword { get; set; }
    }
}
