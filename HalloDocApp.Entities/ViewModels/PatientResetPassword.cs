using System.ComponentModel.DataAnnotations;

namespace HalloDocApp.Entities.ViewModels
{
    public class PatientResetPassword
    {

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
             ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password",ErrorMessage = "Password and confirm password should be same")]
        public string ConfirmPassword { get; set; }
    }
}
