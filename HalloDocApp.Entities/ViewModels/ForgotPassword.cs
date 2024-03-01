using System.ComponentModel.DataAnnotations;

namespace HalloDocApp.Entities.ViewModels
{
    public class ForgotPassword
    {

        [Required(ErrorMessage = "Please enter ypur email address..")] 
        public string Email { get; set; }   
    }
}
