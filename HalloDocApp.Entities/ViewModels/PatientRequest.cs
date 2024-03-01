using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HalloDocApp.Entities.ViewModels
{
    public class PatientRequest
    {

        [DisplayName("First Name")]
        [Required(ErrorMessage ="Enter your first name..")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Firstname should contain only characters..")]
        public string FirstName { get; set; } = null!;


        [Required]
        [DisplayName("Last Name")]
        [RegularExpression("^[A-Za-z]+$",ErrorMessage ="Lastname should contain only characters..")]
        public string LastName { get; set; }

        [DisplayName("Email")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$|^\+?\d{0,2}\-?\d{4,5}\-?\d{5,6}",
               ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }


        [DisplayName("Date of Birth")]
        public DateOnly DateOfBirth { get; set; }
        

        [DisplayName("Phone Number")]
        [Required(ErrorMessage = "Phone number is required...")]
        [RegularExpression("^\\d+$", ErrorMessage = "Phone number must have 10 numbers only..")]
        public string phone { get; set; }

        public string countryCode {  get; set; }

        [DisplayName("Street")]
        public string street { get; set; }

        [DisplayName("City")]
        public string city { get; set; }

        [DisplayName("State")]
        public string state { get; set; }


        [DisplayName("ZipCode")]
        public string zipcode { get; set; }

        [DisplayName("Room No")]
        public string? roomno { get; set; }


        [DisplayName("Symptoms")]
        public string? symptoms { get; set; }
       
        public List<IFormFile>? documents { get; set; }

    }
}
