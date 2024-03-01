using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HalloDocApp.Entities.ViewModels
{
    public class ConciergeRequest
    {

        [DisplayName("First Name")]
        [Required(ErrorMessage = "Enter your first name..")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Firstname should contain only characters..")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Enter your last name..")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Lastname should contain only characters..")]
        public string LastName { get; set; }

        [DisplayName("Concierge Phone Number")]
        [Required(ErrorMessage = "Phone number is required...")]
        [RegularExpression("^\\d+$", ErrorMessage = "Phone number must have 10 numbers only..")]

        public string otherCountryCode { get; set; }
        public string Phone {  get; set; }
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$|^\+?\d{0,2}\-?\d{4,5}\-?\d{5,6}",
              ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

       
        [DisplayName("Hotel Name")]
        public string? hotelname { get; set; }

        [Required]
        [DisplayName("Concierge Street")]
        public string street { get; set; }

        [Required]
        [DisplayName("Concierge City")]
        public string city { get; set; }

        [Required]
        [DisplayName("Concierge State")]
        public string state { get; set; }

        [Required]
        [DisplayName("Concierge zip Code")]
        public string zipcode { get; set; }


        [DisplayName("Patient First Name")]
        [Required(ErrorMessage = "Enter your first name..")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Firstname should contain only characters..")]
        public string patientFirstName { get; set; }

        [DisplayName("Patient Last Name")]
        [Required(ErrorMessage = "Enter your last name..")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Lastname should contain only characters..")]
        public string patientLastName { get; set; }

        [DisplayName("Symptoms")]
        public string? symptoms { get; set; }

        [Required]
        [DisplayName("Date of Birth")]
        public DateOnly dateOfBirth { get; set; }


        [DisplayName("Patient Email Address")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$|^\+?\d{0,2}\-?\d{4,5}\-?\d{5,6}",
              ErrorMessage = "Invalid Email Address")]
        public string patientEmail { get; set; }

        public string countryCode { get; set; }

        [DisplayName("Patient Phone Number")]
        [Required(ErrorMessage = "Phone number is required...")]
        [RegularExpression("^\\d+$", ErrorMessage = "Phone number must have 10 numbers only..")]
        public string patientPhone { get; set; }
       
        public int? roomno {  get; set; }

     
    }
}
