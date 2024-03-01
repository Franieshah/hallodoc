using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HalloDocApp.Entities.ViewModels
{
    public class EditProfile
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        
        public DateOnly dateofbirth { get; set; }

        public string countryCode { get; set; }
        public string phone { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipcode { get; set; }
    }
}
