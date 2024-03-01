using HalloDocApp.Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocApp.Repositories.Repository.Interface
{
    public interface IPatientRepository
    {
        public IDictionary<string,bool> PatientCreateRequest(PatientRequest requestData);

        public string SignUp(PatientSignUp signUpData);
        public string Login(PatientLogin user,HttpContext httpContext);

        public object dashboard(HttpContext httpContext);

        public object viewDoc(int reqId,ViewDocuments model);

        public string FamilyCreateRequest(FamilyRequest familyRequestData);

        public string ConciergeCreateRequest(ConciergeRequest conciergeRequestData);

        public string BusinessCreateRequest(BusinessRequest businessRequestData);

        public string UploadDoc(int reqId,ViewDocuments model);

        public string EditData(PatientDashboardViewModel model, HttpContext httpContext);

        public string SomeoneElse(PatientRequest data);

        public string ForMe(PatientRequest data);


    }
}
