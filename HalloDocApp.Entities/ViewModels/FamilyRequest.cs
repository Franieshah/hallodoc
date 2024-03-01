using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HalloDocApp.Entities.ViewModels
{
    public class FamilyRequest
    {
        [Required(ErrorMessage ="Please enter your first name")]
        [DisplayName("First Name")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Firstname should contain only characters..")]

        public string Firstname { get; set; }

        [Required]
        [DisplayName("Last Name")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Lastname should contain only characters..")]
        public string Lastname { get; set; }

        [Required]
        public string OtherCountryCode { get; set; }

        [DisplayName("Family Member's Phone Number")]
        [Required(ErrorMessage = "Phone number is required...")]
        [RegularExpression("^\\d+$", ErrorMessage = "Phone number must have digits only..")]
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
        public string patientLastName { get; set;}
        public string? symptoms { get; set; }

        [Required]
        [DisplayName("Date Of Birth")]
        public DateOnly dateOfBirth { get; set; }


        [Required]
        [DisplayName("Patient Email Address")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$|^\+?\d{0,2}\-?\d{4,5}\-?\d{5,6}",
              ErrorMessage = "Invalid Email Address")]
        public string patientEmail { get; set;}

        [DisplayName("Patient PhoneNumber")]
        [Required(ErrorMessage = "Phone number is required...")]
        [RegularExpression("^\\d+$", ErrorMessage = "Phone number must have 10 numbers only..")]
        public string patientPhone { get; set;}

        [Required]
        public string countryCode { get; set; }

        [Required]
        [DisplayName("Street")]
        public string patientStreet { get; set;}


        [Required]
        [DisplayName("City")]
        public string patientCity { get; set;}

        [Required]
        [DisplayName("State")]
        public string patientState {  get; set; }

        [Required]
        [DisplayName("ZipCode")]
        public string patientZipCode { get; set;}

        public int? roomno { get; set;} 

        public List<IFormFile>? documents { get; set;}   


    }
}
