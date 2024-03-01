using HalloDocApp.Entities.ViewModels;
using HalloDocApp.Entities.DataModels;

namespace HalloDocApp.Entities.ViewModels
{
    public class PatientDashboardViewModel
    {

        public User User { get; set; } = new User();

        public IEnumerable<Request> RequestData { get; set; }

        public EditProfile editdata { get; set; } = new EditProfile();

    }
}
