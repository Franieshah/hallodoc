using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HalloDocApp.Entities.ViewModels
{
    public class BusinessRequest
    {
        [DisplayName("First Name")]
        [Required(ErrorMessage ="First name is required")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Firstname should contain only characters..")]
        public string FirstName { get; set; }

        [Required(ErrorMessage ="Lastname is required")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Lastname should contain only characters..")]
        public string LastName { get; set; }

        public string otherCountryCode { get; set; }

        [DisplayName("Business Phone")]
        [Required(ErrorMessage = "Phone number is required...")]
        [RegularExpression("^\\d+$", ErrorMessage = "Phone number must have 10 numbers only..")]
        public string Phone { get; set; }


        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$|^\+?\d{0,2}\-?\d{4,5}\-?\d{5,6}",
             ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }

        [Required]
        [DisplayName("Patient First Name")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Firstname should contain only characters..")]
        public string patientFirstName { get; set; }

        [Required]
        [DisplayName("Patient Last Name")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Lastname should contain only characters..")]
        public string patientLastName { get; set; }

        [Required]
        [DisplayName("Symptoms")]
        public string? symptoms { get; set; }

        [Required]
        [DisplayName("Date Of Birth")]
        public DateOnly dateOfBirth { get; set; }

        [DisplayName("Patient Email Address")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$|^\+?\d{0,2}\-?\d{4,5}\-?\d{5,6}",
             ErrorMessage = "Invalid Email Address")]
        public string patientEmail { get; set; }

        public string countryCode {  get; set; }

        [Required(ErrorMessage = "Phone number is required..")]
        [DisplayName("Patient Phone Number")]
        [RegularExpression("^\\d+$", ErrorMessage = "Phone number must have 10 numbers only..")]
        public string patientPhone { get; set; }

        [Required]
        [DisplayName("Street")]
        public string street { get; set; }

        [Required]
        [DisplayName("City")]
        public string city { get; set; }

        [Required]
        [DisplayName("State")]
        public string state { get; set; }

        [Required]
        [DisplayName("Zip Code")]
        public string zipcode { get; set; }

        public int? roomno { get; set; }

      
    }
}
