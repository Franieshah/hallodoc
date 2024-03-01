using HalloDocApp.Entities.DataModels;
using HalloDocApp.Repositories.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocApp.Repositories.Repository.Implementation
{
    public class CreateRequestByPatientRepository : ICreateRequestByPatientRepository
    {
        private readonly IAspnetuserRepository _aspnetuserRepository;

        public CreateRequestByPatientRepository(IAspnetuserRepository aspnetuserRepository)
        {
            _aspnetuserRepository = aspnetuserRepository;
        }

        public async Task PatientCreateRequest(Aspnetuser aspnetuser)
        {
            await _aspnetuserRepository.Add(aspnetuser);
        }
    }
}
