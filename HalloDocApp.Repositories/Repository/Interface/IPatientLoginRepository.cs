using HalloDocApp.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocApp.Repositories.Repository.Interface
{
    public interface IPatientLoginRepository
    {
        public string login(PatientLogin model);
    }
}
