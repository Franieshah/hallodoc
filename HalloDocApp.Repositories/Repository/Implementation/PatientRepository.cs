using HalloDocApp.Entities.DataContext;
using HalloDocApp.Entities.DataModels;
using HalloDocApp.Entities.Enum;
using HalloDocApp.Entities.ViewModels;
using HalloDocApp.Repositories.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HalloDocApp.Repositories.Repository.Implementation
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDBContext _context;
        public PatientRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public IDictionary<string,bool> PatientCreateRequest(PatientRequest requestData)
        {

            var existingpatient = _context.Aspnetusers.FirstOrDefault(p => p.Email == requestData.Email);
            if (existingpatient != null)
            {

                if (!string.IsNullOrEmpty(existingpatient.Passwordhash))
                {

                    var firsttwocharsfromfname = requestData.FirstName.Substring(0, 2);
                    var lasttwocharsfromlname = requestData.LastName.Substring(0, 2);
                    var stateabbr = requestData.state.Substring(0, 2);
                    var date = DateTime.Now.Day.ToString("00");
                    var month = DateTime.Now.Month.ToString("00");
                    var totalRequests = _context.Requests.Where(r => r.Createddate.Date == DateTime.Now.Date).Count().ToString("0000");

                    User userFromDb = _context.Users.FirstOrDefault(u => u.Email == requestData.Email);

                    Request requestTableData = new Request();
                    requestTableData.Requesttypeid = (int)TypeOfRequest.Patient;

                    requestTableData.Requestid = (int)(_context.Requests.Max(a => a.Requestid)) + 1;
                    requestTableData.Userid = userFromDb.Userid;
                    requestTableData.Firstname = requestData.FirstName;
                    requestTableData.Lastname = requestData.LastName;
                    requestTableData.Email = requestData.Email;
                    requestTableData.Phonenumber = requestData.phone;
                    requestTableData.CountryCode = int.Parse(requestData.countryCode);
                    requestTableData.Status = (int)StatusOfRequest.New;
                    requestTableData.Createduserid = 1;
                    requestTableData.Createddate = DateTime.Now;
                    requestTableData.Isurgentemailsent = true;
                    requestTableData.Confirmationnumber = (stateabbr + date + month + lasttwocharsfromlname + firsttwocharsfromfname + totalRequests).ToUpper();
                    _context.Add(requestTableData);
                    _context.SaveChanges();

                    Requestclient requestclientdata = new Requestclient();
                    requestclientdata.Requestid = requestTableData.Requestid;
                    requestclientdata.Firstname = requestData.FirstName;
                    requestclientdata.Lastname = requestData.LastName;
                    requestclientdata.Phonenumber = requestData.phone;
                    requestclientdata.CountryCode = int.Parse(requestData.countryCode);
                    requestclientdata.Email = requestData.Email;
                    requestclientdata.Street = requestData.street;
                    requestclientdata.City = requestData.city;
                    requestclientdata.State = requestData.state;
                    requestclientdata.Zipcode = requestData.zipcode;
                    requestclientdata.Strmonth = requestData.DateOfBirth.Month.ToString();
                    requestclientdata.Intdate = requestData.DateOfBirth.Day;
                    requestclientdata.Intyear = requestData.DateOfBirth.Year;
                    requestclientdata.Notes = requestData.symptoms;
                    _context.Add(requestclientdata);
                    _context.SaveChanges();
                    if (requestData.documents != null && requestData.documents.Count() > 0)
                    {
                        foreach (var file in requestData.documents)
                        {
                            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }


                            string fileNameWithPath = Path.Combine(path, file.FileName);
                            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }

                            Requestwisefile fileData = new Requestwisefile();
                            fileData.Filename = file.FileName;
                            fileData.Requestid = requestTableData.Requestid;
                            _context.Requestwisefiles.Add(fileData);
                            _context.SaveChanges();
                        }
                    }
                    return new Dictionary<string, bool> { {"IsUserNull",false },{ "IsPasswordNull",false} };
                }
                else
                {

                    var userFromDb = _context.Users.FirstOrDefault(a => a.Email == existingpatient.Email);

                    var firsttwocharsfromfname = requestData.FirstName.Substring(0, 2);
                    var lasttwocharsfromlname = requestData.LastName.Substring(0, 2);
                    var stateabbr = requestData.state.Substring(0, 2);
                    var date = DateTime.Now.Day.ToString("00");
                    var month = DateTime.Now.Month.ToString("00");
                    var totalRequests = _context.Requests.Where(r => r.Createddate.Date == DateTime.Now.Date).Count().ToString("0000");

                    Request requestTableData = new Request();
                    requestTableData.Requesttypeid = (int)TypeOfRequest.Patient;
                    requestTableData.Requestid = (int)(_context.Requests.Max(a => a.Requestid)) + 1;
                    requestTableData.Userid = userFromDb.Userid;
                    requestTableData.Firstname = requestData.FirstName;
                    requestTableData.Lastname = requestData.LastName;
                    requestTableData.Email = requestData.Email;
                    requestTableData.Status = (int)StatusOfRequest.New;
                    requestTableData.Createduserid = 1;
                    requestTableData.Createddate = DateTime.Now;
                    requestTableData.Isurgentemailsent = true;
                    requestTableData.Phonenumber = requestData.phone;
                    requestTableData.CountryCode = int.Parse(requestData.countryCode);
                    requestTableData.Confirmationnumber = (stateabbr + date + month + lasttwocharsfromlname + firsttwocharsfromfname + totalRequests).ToUpper();
                    _context.Add(requestTableData);

                    Requestclient requestclientdata = new Requestclient();
                    requestclientdata.Requestid = requestTableData.Requestid;
                    requestclientdata.Firstname = requestData.FirstName;
                    requestclientdata.Lastname = requestData.LastName;
                    requestclientdata.Phonenumber = requestData.phone;
                    requestclientdata.CountryCode = int.Parse(requestData.countryCode);
                    requestclientdata.Email = requestData.Email;
                    requestclientdata.Street = requestData.street;
                    requestclientdata.City = requestData.city;
                    requestclientdata.State = requestData.state;
                    requestclientdata.Zipcode = requestData.zipcode;
                    requestclientdata.Strmonth = requestData.DateOfBirth.Month.ToString();
                    requestclientdata.Intdate = requestData.DateOfBirth.Day;
                    requestclientdata.Intyear = requestData.DateOfBirth.Year;
                    requestclientdata.Notes = requestData.symptoms;
                    _context.Add(requestclientdata);
                    _context.SaveChanges();

                    if (requestData.documents != null && requestData.documents.Count() > 0)
                    {
                        foreach (var file in requestData.documents)
                        {
                            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }

                            string fileNameWithPath = Path.Combine(path, file.FileName);
                            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }

                            Requestwisefile fileData = new Requestwisefile();
                            fileData.Filename = file.FileName;
                            fileData.Requestid = requestTableData.Requestid;
                            _context.Requestwisefiles.Add(fileData);
                            _context.SaveChanges();
                        }
                    }

                    return new Dictionary<string, bool> { { "IsUserNull", false }, { "IsPasswordNull", true } };
                }
            }
            else
            {
                var firsttwocharsfromfname = requestData.FirstName.Substring(0, 2);
                var lasttwocharsfromlname = requestData.LastName.Substring(0, 2);
                var stateabbr = requestData.state.Substring(0, 2);
                var date = DateTime.Now.Day.ToString("00");
                var month = DateTime.Now.Month.ToString("00");
                var totalRequests = _context.Requests.Where(r => r.Createddate.Date == DateTime.Now.Date).Count().ToString("0000");

                Aspnetuser aspusers = new Aspnetuser();
                aspusers.Username = requestData.FirstName + " " + requestData.LastName;
                aspusers.Email = requestData.Email;
                aspusers.Phonenumber = requestData.phone;
                aspusers.CountryCode = int.Parse(requestData.countryCode);
                aspusers.Createddate = DateTime.Now;
                _context.Aspnetusers.Add(aspusers);
                _context.SaveChanges();
                var id = _context.Aspnetusers.FirstOrDefault(a => a.Email == requestData.Email);

                User user = new User();
                user.Firstname = requestData.FirstName;
                user.Aspnetuserid = id.Id;
                user.Lastname = requestData.LastName;
                user.Email = requestData.Email;
                user.DateofBirth = requestData.DateOfBirth;
                user.Mobile = requestData.phone;
                user.CountryCode = int.Parse(requestData.countryCode);
                user.City = requestData.city;
                user.Street = requestData.street;
                user.State = requestData.state;
                user.Zipcode = requestData.zipcode;
                _context.Add(user);
                _context.SaveChanges();
                User userFromDb = _context.Users.FirstOrDefault(u => u.Email == requestData.Email);

                Request requestTableData = new Request();
                requestTableData.Requesttypeid = (int)TypeOfRequest.Patient;

                requestTableData.Requestid = (int)(_context.Requests.Max(a => a.Requestid)) + 1;
                requestTableData.Userid = userFromDb.Userid;
                requestTableData.Firstname = requestData.FirstName;
                requestTableData.Lastname = requestData.LastName;
                requestTableData.Email = requestData.Email;
                requestTableData.Status = (int)StatusOfRequest.New;
                requestTableData.Createduserid = 1;
                requestTableData.Createddate = DateTime.Now;
                requestTableData.Isurgentemailsent = true;
                requestTableData.Phonenumber = requestData.phone;
                requestTableData.CountryCode = int.Parse(requestData.countryCode);
                requestTableData.Confirmationnumber = (stateabbr + date + month + lasttwocharsfromlname + firsttwocharsfromfname + totalRequests).ToUpper();
                _context.Add(requestTableData);

                Requestclient requestclientdata = new Requestclient();
                requestclientdata.Requestid = requestTableData.Requestid;
                requestclientdata.Firstname = requestData.FirstName;
                requestclientdata.Lastname = requestData.LastName;
                requestclientdata.Phonenumber = requestData.phone;
                requestclientdata.CountryCode = int.Parse(requestData.countryCode);
                requestclientdata.Email = requestData.Email;
                requestclientdata.Street = requestData.street;
                requestclientdata.City = requestData.city;
                requestclientdata.State = requestData.state;
                requestclientdata.Zipcode = requestData.zipcode;
                requestclientdata.Strmonth = requestData.DateOfBirth.Month.ToString();
                requestclientdata.Intdate = requestData.DateOfBirth.Day;
                requestclientdata.Intyear = requestData.DateOfBirth.Year;
                requestclientdata.Notes = requestData.symptoms;
                _context.Add(requestclientdata);
                _context.SaveChanges();


                if (requestData.documents != null && requestData.documents.Count() > 0)
                {
                    foreach (var file in requestData.documents)
                    {
                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        string fileNameWithPath = Path.Combine(path, file.FileName);
                        using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        Requestwisefile fileData = new Requestwisefile();
                        fileData.Filename = file.FileName;
                        fileData.Requestid = requestTableData.Requestid;
                        _context.Requestwisefiles.Add(fileData);
                        _context.SaveChanges();
                    }
                }
                return new Dictionary<string, bool> { { "IsUserNull", true }, { "IsPasswordNull", true } };
            }
        }

        public string SignUp(PatientSignUp signUpData)
        {
            Aspnetuser aspnetuser = _context.Aspnetusers.FirstOrDefault(a => a.Email == signUpData.Username);
            if (aspnetuser != null)
            {

                aspnetuser.Passwordhash = signUpData.Password;
                aspnetuser.Createddate = DateTime.Now;
                _context.Aspnetusers.Update(aspnetuser);
                _context.SaveChanges();
            }
            return "Success";
        }

        public string Login(PatientLogin user, HttpContext httpContext)
        {
            var aspNetUserFromDb = _context.Aspnetusers.FirstOrDefault(a => a.Email == user.Username);
            if (aspNetUserFromDb != null && aspNetUserFromDb.Passwordhash == user.Password)
            {
                User userFromDb = _context.Users.FirstOrDefault(a => a.Aspnetuserid == aspNetUserFromDb.Id);
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Secure = true;
                cookieOptions.Expires = DateTime.Now.AddYears(10);
                httpContext.Response.Cookies.Append("UserId", userFromDb.Userid.ToString(), cookieOptions);
                httpContext.Response.Cookies.Append("FirstName", userFromDb.Firstname, cookieOptions);
                httpContext.Response.Cookies.Append("LastName", userFromDb.Lastname, cookieOptions);

                return "Success";
            }
            else if (aspNetUserFromDb == null)
            {
                return "InvalidUsername";
            }
            else
            {
                return "InvalidPassword";
            }
        }

        public object dashboard(HttpContext httpContext)
        {
            int userId = int.Parse(httpContext.Request.Cookies["UserId"]);
            PatientDashboardViewModel dashboardData = new PatientDashboardViewModel();
            dashboardData.User = _context.Users.FirstOrDefault(a => a.Userid == userId);
            dashboardData.RequestData = _context.Requests.Where(b => b.Userid == userId).ToList();
            return dashboardData;
        }

        public object viewDoc(int reqId,ViewDocuments model)
        {
            model.requestWiseFiles = _context.Requestwisefiles.Where(u => u.Requestid == reqId).ToList();
            model.request = _context.Requests.FirstOrDefault(u => u.Requestid == reqId);
            model.requestClient = _context.Requestclients.FirstOrDefault(u => u.Requestid == reqId);
            return model;
        }

        public string FamilyCreateRequest(FamilyRequest familyRequestData)
        {
            var firsttwocharsfromfname = familyRequestData.patientFirstName.Substring(0, 2);
            var lasttwocharsfromlname = familyRequestData.patientLastName.Substring(0, 2);
            var stateabbr = familyRequestData.patientState.Substring(0, 2);
            var date = DateTime.Now.Day.ToString("00");
            var month = DateTime.Now.Month.ToString("00");
            var totalRequests = _context.Requests.Where(r => r.Createddate.Date == DateTime.Now.Date).Count().ToString("0000");

            Request requestData = new Request();
            requestData.Requestid = (int)(_context.Requests.Max(a => a.Requestid)) + 1;
            requestData.Requesttypeid = (int)TypeOfRequest.Family;
            requestData.Firstname = familyRequestData.Firstname;
            requestData.Lastname = familyRequestData.Lastname;
            requestData.Phonenumber = familyRequestData.Phone;
            requestData.CountryCode = int.Parse(familyRequestData.OtherCountryCode);
            requestData.Email = familyRequestData.email;
            requestData.Status = 1;
            requestData.Createddate = DateTime.Now;
            requestData.Createduserid = 1;
            requestData.Confirmationnumber = (stateabbr + date + month + lasttwocharsfromlname + firsttwocharsfromfname + totalRequests).ToUpper();
            _context.Add(requestData);

            Requestclient requestClientData = new Requestclient();
            requestClientData.Requestid = requestData.Requestid;
            requestClientData.Firstname = familyRequestData.patientFirstName;
            requestClientData.Lastname = familyRequestData.patientLastName;
            requestClientData.Email = familyRequestData.patientEmail;
            requestClientData.Phonenumber = familyRequestData.patientPhone;
            requestClientData.CountryCode = int.Parse(familyRequestData.countryCode);
            requestClientData.Intdate = familyRequestData.dateOfBirth.Day;
            requestClientData.Strmonth = familyRequestData.dateOfBirth.Month.ToString();
            requestClientData.Intyear = familyRequestData.dateOfBirth.Year;
            requestClientData.Street = familyRequestData.patientStreet;
            requestClientData.City = familyRequestData.patientCity;
            requestClientData.State = familyRequestData.patientState;
            requestClientData.Zipcode = familyRequestData.patientZipCode;
            requestClientData.Notes = familyRequestData.symptoms;

            if (familyRequestData.documents != null && familyRequestData.documents.Count() > 0)
            {
                foreach (var file in familyRequestData.documents)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    string fileNameWithPath = Path.Combine(path, file.FileName);
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    Requestwisefile fileData = new Requestwisefile();
                    fileData.Filename = file.FileName;
                    fileData.Requestid = requestData.Requestid;
                    _context.Requestwisefiles.Add(fileData);
                    _context.SaveChanges();
                }
            }

            _context.Add(requestClientData);
            _context.SaveChanges();
            return "Success";
        }

        public string ConciergeCreateRequest(ConciergeRequest conciergeRequestData)
        {
            var firsttwocharsfromfname = conciergeRequestData.patientFirstName.Substring(0, 2);
            var lasttwocharsfromlname = conciergeRequestData.patientLastName.Substring(0, 2);
            var stateabbr = conciergeRequestData.state.Substring(0, 2);
            var date = DateTime.Now.Day.ToString("00");
            var month = DateTime.Now.Month.ToString("00");
            var totalRequests = _context.Requests.Where(r => r.Createddate.Date == DateTime.Now.Date).Count().ToString("0000");

            Request requestData = new Request();
            requestData.Requestid = (int)(_context.Requests.Max(a => a.Requestid)) + 1;
            requestData.Requesttypeid = (int)TypeOfRequest.Concierge;
            requestData.Firstname = conciergeRequestData.FirstName;
            requestData.Lastname = conciergeRequestData.LastName;
            requestData.Phonenumber = conciergeRequestData.Phone;
            requestData.CountryCode = int.Parse(conciergeRequestData.countryCode);
            requestData.Email = conciergeRequestData.Email;    
            requestData.Status = 1;
            requestData.Createddate = DateTime.Now;
            requestData.Createduserid = 1;
            requestData.Confirmationnumber = (stateabbr + date + month + lasttwocharsfromlname + firsttwocharsfromfname + totalRequests).ToUpper();
            _context.Add(requestData);

            Requestclient requestClientData = new Requestclient();
            requestClientData.Requestid = requestData.Requestid;
            requestClientData.Firstname = conciergeRequestData.patientFirstName;
            requestClientData.Lastname = conciergeRequestData.patientLastName;
            requestClientData.Email = conciergeRequestData.patientEmail;
            requestClientData.Phonenumber = conciergeRequestData.patientPhone;
            requestClientData.CountryCode = int.Parse(conciergeRequestData.countryCode);
            requestClientData.Intdate = conciergeRequestData.dateOfBirth.Day;
            requestClientData.Strmonth = conciergeRequestData.dateOfBirth.Month.ToString();
            requestClientData.Intyear = conciergeRequestData.dateOfBirth.Year;
            requestClientData.Notes = conciergeRequestData.symptoms;
            requestClientData.Street = conciergeRequestData.street;
            requestClientData.City = conciergeRequestData.city;
            requestClientData.State = conciergeRequestData.state;
            requestClientData.Zipcode = conciergeRequestData.zipcode;
            _context.Add(requestClientData);
            _context.SaveChanges();

            return "Success";
        }

        public string BusinessCreateRequest(BusinessRequest businessRequestData)
        {
            var firsttwocharsfromfname = businessRequestData.patientFirstName.Substring(0, 2);
            var lasttwocharsfromlname = businessRequestData.patientLastName.Substring(0, 2);
            var stateabbr = businessRequestData.state.Substring(0, 2);
            var date = DateTime.Now.Day.ToString("00");
            var month = DateTime.Now.Month.ToString("00");
            var totalRequests = _context.Requests.Where(r => r.Createddate.Date == DateTime.Now.Date).Count().ToString("0000");


            Request requestData = new Request();
            requestData.Requestid = (int)(_context.Requests.Max(a => a.Requestid)) + 1;
            requestData.Requesttypeid = (int)TypeOfRequest.Business;
            requestData.Firstname = businessRequestData.FirstName;
            requestData.Lastname = businessRequestData.LastName;
            requestData.Phonenumber = businessRequestData.Phone;
            requestData.CountryCode = int.Parse(businessRequestData.otherCountryCode);
            requestData.Email = businessRequestData.email;
            requestData.Status = 1;
            requestData.Createddate = DateTime.Now;
            requestData.Createduserid = 1;
            requestData.Confirmationnumber = (stateabbr + date + month + lasttwocharsfromlname + firsttwocharsfromfname + totalRequests).ToUpper();

            _context.Add(requestData);
            _context.SaveChanges();


            Requestclient requestClientData = new Requestclient();
            requestClientData.Requestid = requestData.Requestid;
            requestClientData.Firstname = businessRequestData.patientFirstName;
            requestClientData.Lastname = businessRequestData.patientLastName;
            requestClientData.Email = businessRequestData.patientEmail;
            requestClientData.Phonenumber = businessRequestData.patientPhone;
            requestClientData.CountryCode = int.Parse(businessRequestData.countryCode);
            requestClientData.Intdate = businessRequestData.dateOfBirth.Day;
            requestClientData.Strmonth = businessRequestData.dateOfBirth.Month.ToString();
            requestClientData.Intyear = businessRequestData.dateOfBirth.Year;
            requestClientData.Street = businessRequestData.street;
            requestClientData.City = businessRequestData.city;
            requestClientData.State = businessRequestData.state;
            requestClientData.Zipcode = businessRequestData.zipcode;
            requestClientData.Notes = businessRequestData.symptoms;
            _context.Add(requestClientData);
            _context.SaveChanges();

            return "Success";
        }

        public string UploadDoc(int reqId, ViewDocuments model)
        {
            foreach (var file in model.Files)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var fileId = _context.Requestwisefiles.Count() + 1;
                string fileNameWithPath = Path.Combine(path, file.FileName);
                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                Requestwisefile fileData = new Requestwisefile();
                fileData.Filename = file.FileName;
                fileData.Requestid = reqId;
                _context.Requestwisefiles.Add(fileData);
                _context.SaveChanges();
            }
            return "Success";
        }

        public string EditData(PatientDashboardViewModel model,HttpContext httpContext)
        {
            int userId = int.Parse(httpContext.Request.Cookies["UserId"]);
            var data = _context.Users.Find(userId);

            if (data != null)
            {
                data.Firstname = model.editdata.firstname;
                data.Lastname = model.editdata.lastname;
                data.DateofBirth = model.editdata.dateofbirth;
                data.Mobile = model.editdata.phone;
                data.CountryCode = int.Parse(model.editdata.countryCode);
                data.Street = model.editdata.street;
                data.City = model.editdata.city;
                data.State = model.editdata.state;
                data.Zipcode = model.editdata.zipcode;

                _context.Users.Update(data);
                _context.SaveChanges();

                return "Success";
            }
            return "Error";
        }

        public string ForMe(PatientRequest data)
        {
            var firsttwocharsfromfname = data.FirstName.Substring(0, 2);
            var lasttwocharsfromlname = data.LastName.Substring(0, 2);
            var stateabbr = data.state.Substring(0, 2);
            var date = DateTime.Now.Day.ToString("00");
            var month = DateTime.Now.Month.ToString("00");
            var totalRequests = _context.Requests.Where(r => r.Createddate.Date == DateTime.Now.Date).Count().ToString("0000");

            User userFromDb = _context.Users.FirstOrDefault(u => u.Email == data.Email);

            Request requestTableData = new Request();
            requestTableData.Requesttypeid = (int)TypeOfRequest.Patient;
            requestTableData.Requestid = (int)(_context.Requests.Max(a => a.Requestid)) + 1;
            requestTableData.Userid = userFromDb.Userid;
            requestTableData.Firstname = data.FirstName;
            requestTableData.Lastname = data.LastName;
            requestTableData.Email = data.Email;
            requestTableData.CountryCode = int.Parse(data.countryCode);
            requestTableData.Phonenumber = data.phone;
            requestTableData.Status = (int)StatusOfRequest.New;
            requestTableData.Createduserid = 1;
            requestTableData.Createddate = DateTime.Now;
            requestTableData.Isurgentemailsent = true;
            requestTableData.Confirmationnumber = (stateabbr + date + month + lasttwocharsfromlname + firsttwocharsfromfname + totalRequests).ToUpper();
            _context.Add(requestTableData);
            _context.SaveChanges();


            Requestclient requestClientData = new Requestclient();
            requestClientData.Requestid = requestTableData.Requestid;
            requestClientData.Firstname = data.FirstName;
            requestClientData.Lastname = data.LastName;
            requestClientData.Email = data.Email;
            requestClientData.Phonenumber = data.phone;
            requestClientData.CountryCode = int.Parse(data.countryCode);
            requestClientData.Intdate = data.DateOfBirth.Day;
            requestClientData.Strmonth = data.DateOfBirth.Month.ToString();
            requestClientData.Intyear = data.DateOfBirth.Year;
            requestClientData.Street = data.street;
            requestClientData.City = data.city;
            requestClientData.State = data.state;
            requestClientData.Zipcode = data.zipcode;
            requestClientData.Notes = data.symptoms;
            _context.Add(requestClientData);
            _context.SaveChanges();


            if (data.documents != null && data.documents.Count() > 0)
            {
                foreach (var file in data.documents)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    string fileNameWithPath = Path.Combine(path, file.FileName);
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    Requestwisefile fileData = new Requestwisefile();
                    fileData.Filename = file.FileName;
                    fileData.Requestid = requestTableData.Requestid;
                    _context.Requestwisefiles.Add(fileData);
                    _context.SaveChanges();
                }
            }
            return "Success";
        }
        public string SomeoneElse(PatientRequest data)
        {
            var firsttwocharsfromfname = data.FirstName.Substring(0, 2);
            var lasttwocharsfromlname = data.LastName.Substring(0, 2);
            var stateabbr = data.state.Substring(0, 2);
            var date = DateTime.Now.Day.ToString("00");
            var month = DateTime.Now.Month.ToString("00");
            var totalRequests = _context.Requests.Where(r => r.Createddate.Date == DateTime.Now.Date).Count().ToString("0000");

            User userFromDb = _context.Users.FirstOrDefault(u => u.Email == data.Email);

            Request requestTableData = new Request();
            requestTableData.Requesttypeid = (int)TypeOfRequest.Patient;
            requestTableData.Requestid = (int)(_context.Requests.Max(a => a.Requestid)) + 1;
            requestTableData.Userid = userFromDb.Userid;
            requestTableData.Firstname = data.FirstName;
            requestTableData.Lastname = data.LastName;
            requestTableData.Email = data.Email;
            requestTableData.CountryCode = int.Parse(data.countryCode);
            requestTableData.Phonenumber = data.phone;
            requestTableData.Status = (int)StatusOfRequest.New;
            requestTableData.Createduserid = 1;
            requestTableData.Createddate = DateTime.Now;
            requestTableData.Isurgentemailsent = true;
            requestTableData.Confirmationnumber = (stateabbr + date + month + lasttwocharsfromlname + firsttwocharsfromfname + totalRequests).ToUpper();
            _context.Add(requestTableData);
            _context.SaveChanges();


            Requestclient requestClientData = new Requestclient();
            requestClientData.Firstname = data.FirstName;
            requestClientData.Lastname = data.LastName;
            requestClientData.Email = data.Email;
            requestClientData.Phonenumber = data.phone;
            requestClientData.CountryCode = int.Parse(data.countryCode);
            requestClientData.Intdate = data.DateOfBirth.Day;
            requestClientData.Strmonth = data.DateOfBirth.Month.ToString();
            requestClientData.Intyear = data.DateOfBirth.Year;
            requestClientData.Street = data.street;
            requestClientData.City = data.city;
            requestClientData.State = data.state;
            requestClientData.Zipcode = data.zipcode;
            requestClientData.Notes = data.symptoms;
            _context.Add(requestClientData);
            _context.SaveChanges();


            if (data.documents != null && data.documents.Count() > 0)
            {
                foreach (var file in data.documents)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    string fileNameWithPath = Path.Combine(path, file.FileName);
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    Requestwisefile fileData = new Requestwisefile();
                    fileData.Filename = file.FileName;
                    fileData.Requestid = requestTableData.Requestid;
                    _context.Requestwisefiles.Add(fileData);
                    _context.SaveChanges();
                }
            }
            return "Success";
        }
    }
}
