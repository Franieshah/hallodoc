using HalloDocApp.Entities.DataContext;
using HalloDocApp.Repositories.Repository.Interface;
using HalloDocApp.Entities.ViewModels;
using HalloDocApp.Entities.DataModels;

namespace HalloDocApp.Repositories.Repository.Implementation
{
    public class PatientLoginRepository : IPatientLoginRepository
    {
        private readonly ApplicationDBContext _context;
        public PatientLoginRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public string login(PatientLogin model)
        {
           Aspnetuser userdata = new Aspnetuser();
            userdata.Username = "Franie Shah";
            userdata.Email = model.Password;
            userdata.Passwordhash = model.Password;

            _context.Add( userdata );
            _context.SaveChanges();
            return "";
        }
    }
}
