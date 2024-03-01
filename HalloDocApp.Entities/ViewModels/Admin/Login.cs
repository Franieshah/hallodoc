using System.ComponentModel.DataAnnotations;

namespace HalloDocApp.Entities.ViewModels
{
    public class Login
    {
        [Required(ErrorMessage ="Please enter your email address..")]
        public int email {  get; set; }

        [Required(ErrorMessage = " Please enter your password..")]
        public string password { get; set; }
    }
}
