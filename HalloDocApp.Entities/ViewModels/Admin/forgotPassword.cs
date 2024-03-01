using System.ComponentModel.DataAnnotations;

namespace HalloDocApp.Entities.ViewModels
{
    public class forgotPassword
    {
        [Required(ErrorMessage = "Please enter your email address..")]
        public string email { get; set; }
    }
}
