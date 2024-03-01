using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HalloDocApp.Entities.ViewModels
{
    public class PatientLogin
    {
        [Required(ErrorMessage = "Please enter your email address..")]
        [DisplayName("Email")]
        public String Username { get; set; }



        [Required(ErrorMessage = "Please enter your Password..")]
        [DataType(DataType.Password)]
        public String Password { get; set; }
    }
}
