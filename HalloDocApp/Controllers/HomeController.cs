
using HalloDocApp.Entities.DataContext;
using HalloDocApp.Entities.DataModels;
using HalloDocApp.Entities.ViewModels;
using HalloDocApp.Entities.Enum;
using HalloDocApp.Repositories.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.IO.Compression;

namespace HalloDocApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPatientLoginRepository _request;




        private readonly ApplicationDBContext _context;
        private readonly IPatientRepository _patient;

        public HomeController(ApplicationDBContext context, IPatientLoginRepository request, IPatientRepository patient)
        {
             _context = context;
            _request = request;
            _patient = patient;
        }

        [Route("", Name = "Default")]
        [Route("patient/index", Name = "PatientSite")]
        public IActionResult PatientSite()
        {
            return View();
        }
        [Route("patient/SignUp", Name = "PatientSignUp")]
        public IActionResult PatientSignUp()
        {
            return View();
        }

        [HttpPost]
        [Route("patient/SignUp", Name = "PatientSignUp")]
        public IActionResult PatientSignUp(PatientSignUp signUpData)
        {
            if (signUpData.Password == signUpData.ConfirmPassword)
            {
                if (ModelState.IsValid)
                {
                    var ans = _patient.SignUp(signUpData);
                    if (ans == "Success")
                    {
                        return RedirectToAction("PatientLogin");
                    }
                    else
                    {
                        return View(signUpData);
                    }
                }
                else
                {
                    return View(signUpData);
                }
            }
            else
            {
                return View(signUpData);
            }
        }

        [Route("patient/Login", Name = "PatientLogin")]
        public IActionResult PatientLogin()
        {
            return View();
        }

        [HttpPost]
        [Route("patient/Login", Name = "PatientLogin")]
        public IActionResult PatientLogin(PatientLogin user)
        {
            if (ModelState.IsValid)
            {
                var result = _patient.Login(user, HttpContext);
                if (result == "Success")
                {
                    return RedirectToAction("PatientDashboard");
                }
                else if (result == "InvalidUsername")
                {
                    ModelState.AddModelError(nameof(user.Username), "Invalid Username");
                    return View(user);
                }
                else
                {
                    ModelState.AddModelError(nameof(user.Password), "Invalid Password");
                    return View(user);
                }
            }
            return View(user);
        }

        public IActionResult PatientLogOut()
        {

            Response.Cookies.Delete("UserId");
            return RedirectToAction("PatientLogin");
        }

        [Route("patient/ForgotPassword", Name = "PatientForgotPassword")]
        public IActionResult PatientForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("patient/ForgotPassword", Name = "PatientForgotPassword")]
        public async Task<IActionResult> PatientForgotPassword(ForgotPassword model)
        {
            var userExist = _context.Users.FirstOrDefault(a => a.Email == model.Email);
            if (userExist != null)
            {
                var link = Request.Scheme + "://" + Request.Host + "/patient/resetPassword";
                //var msg = await SendEmail(model.Email, link);
                return View(model);
            }
            else
            {
                ModelState.AddModelError(nameof(model.Email), "User doesn't exist..");
                return View(model);
            }

        }

        [Route("patient/resetPassword", Name = "PatientResetPassword")]
        public IActionResult PatientResetPassword()
        {
            return View();
        }


        [HttpPost]
        [Route("patient/resetPassword", Name = "PatientResetPassword")]
        public IActionResult PatientResetPassword(PatientResetPassword model)
        {         
            return RedirectToAction("PatientLogin");
        }


        [Route("patient/submitRequest", Name = "PatientSubmitRequest")]
        public IActionResult PatientSubmitRequest()
        {
            return View();
        }

        [Route("patient/PatientCreateRequest", Name = "PatientCreateRequest")]
        public IActionResult PatientCreateRequest()
        {
            return View();
        }

        [HttpPost]
        [Route("patient/PatientCreateRequest", Name = "PatientCreateRequest")]
        public IActionResult PatientCreateRequest(PatientRequest requestData)
        {
            if (ModelState.IsValid)
            {
                var result = _patient.PatientCreateRequest(requestData);
                if (result["IsUserNull"] && result["IsPasswordNull"])
                {
                    return RedirectToAction("PatientSignUp");
                }
                else if (!result["IsUserNull"] && result["IsPasswordNull"])
                {
                    return RedirectToAction("PatientSignUp");
                }
                else
                {
                    return RedirectToAction("PatientLogin");
                }
            }
            return View();
        }

        [Route("patient/FamilyFriendRequest", Name = "PatientFamilyFriendRequest")]
        public IActionResult PatientFamilyFriendRequest()
        {
            return View();
        }

        [HttpPost]
        [Route("patient/FamilyFriendRequest", Name = "PatientFamilyFriendRequest")]
        public IActionResult PatientFamilyFriendRequest(FamilyRequest familyRequestData)
        {
            if (ModelState.IsValid)
            {
                var result = _patient.FamilyCreateRequest(familyRequestData);
                if (result == "Success")
                {
                    return RedirectToAction("PatientLogin");
                }
                else
                {
                    return View(familyRequestData);
                }
            }
            return View();
        }

        [Route("patient/ConciergeRequest", Name = "PatientConciergeRequest")]
        public IActionResult PatientConciergeRequest()
        {
            return View();
        }

        [HttpPost]
        [Route("patient/ConciergeRequest", Name = "PatientConciergeRequest")]
        public IActionResult PatientConciergeRequest(ConciergeRequest conciergeRequestData)
        {
            if (ModelState.IsValid)
            {
                var result = _patient.ConciergeCreateRequest(conciergeRequestData);
                if (result == "Success")
                {
                    return RedirectToAction("PatientLogin");
                }
                else
                {
                    return View(conciergeRequestData);
                }
            }
            return View(conciergeRequestData);
        }

        [Route("patient/BusinessRequest", Name = "PatientBusinessRequest")]
        public IActionResult PatientBusinessRequest()
        {
            return View();
        }

        [HttpPost]
        [Route("patient/BusinessRequest", Name = "PatientBusinessRequest")]
        public IActionResult PatientBusinessRequest(BusinessRequest businessRequestData)
        {
            if (ModelState.IsValid)
            {
                var result = _patient.BusinessCreateRequest(businessRequestData);
                if (result == "Success")
                {
                    return RedirectToAction("PatientLogin");
                }
                else
                {
                    return View(businessRequestData);
                }
            }
            return View(businessRequestData);
        }

        [Route("patient/Dashboard", Name = "PatientDashboard")]
        public IActionResult PatientDashboard()
        {
            var dashboardData = _patient.dashboard(HttpContext);
            return View(dashboardData);
        }

        [Route("patient/ViewDocuments", Name = "ViewDocuments")]
        public IActionResult ViewDocuments(int reqId, ViewDocuments model)
        {
            var viewDocData = _patient.viewDoc(reqId, model);
            return View(viewDocData);
        }

        [HttpPost]
        public IActionResult UploadDocuments(int reqId, ViewDocuments model)
        {
            if (model.Files != null && model.Files.Count() > 0)
            {
                var result = _patient.UploadDoc(reqId, model);
                if(result == "Success")
                {
                    return RedirectToAction("PatientDashboard");
                }
                else
                {
                    ModelState.AddModelError(nameof(model.Files),"There is an error while uploading your document.");
                    return View(model);
                }
            }
            return View(model);
        }

        public IActionResult DownloadFile(int documentid)
        {
            var filename = _context.Requestwisefiles.FirstOrDefault(u => u.Requestwisefileid == documentid);
            //  var filepath = Path.Combine(hostingEnvironment.WebRootPath, "uploads", filename.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", filename.Filename);

            return File(System.IO.File.ReadAllBytes(filePath), "multipart/form-data", System.IO.Path.GetFileName(filePath));

        }

        public IActionResult EditProfileData(PatientDashboardViewModel model)
        {
            var result = _patient.EditData(model,HttpContext);
            if (result == "Success")
            {
                return RedirectToAction("PatientDashboard");
            }
            else
            {
                return View(model);
            }
        }
        public IActionResult DownLoadAll(int reqId)
        {
            var files = _context.Requestwisefiles.Where(a => a.Requestid == reqId).ToList();
            if (files == null || files.Count == 0)
            {
                return NotFound();
            }

            string zipFileName = $"Request_{reqId}_Files.zip";
            string zipFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/downloads", zipFileName);

            using (var zipArchive = new ZipArchive(new FileStream(zipFilePath, FileMode.Create), ZipArchiveMode.Create))
            {
                foreach (var file in files)
                {
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", file.Filename);
                    if (System.IO.File.Exists(filePath))
                    {
                        zipArchive.CreateEntryFromFile(filePath, file.Filename);
                    }
                }
            }

            byte[] zipBytes = System.IO.File.ReadAllBytes(zipFilePath);
            /* System.IO.File.Delete(zipFilePath);*/ // Delete the zip file after reading its content

            return File(zipBytes, "application/zip", zipFileName);
        }

        [Route("patient/SubmitInformationForMe", Name = "SubmitInformationForMe")]
        public IActionResult SubmitInformationForMe()
        {
            return View();
        }

        [HttpPost]
        [Route("patient/SubmitInformationForMe", Name = "SubmitInformationForMe")]
        public IActionResult SubmitInformationForMe(PatientRequest data)
        {
            var result = _patient.ForMe(data);
            if (result == "Success")
            {
                return RedirectToAction("PatientDashboard");
            }
            else{
                return View(data);
            }
           
        }

        [Route("patient/SubmitRequestForSomeoneElse", Name = "SubmitRequestForSomeoneElse")]
        public IActionResult SubmitRequestForSomeoneElse()
        {
            return View();
        }

        [HttpPost]
        [Route("patient/SubmitRequestForSomeoneElse", Name = "SubmitRequestForSomeoneElse")]
        public IActionResult SubmitRequestForSomeoneElse(PatientRequest data)
        {
            var result = _patient.SomeoneElse(data);
            if(result == "Success")
            {
                return RedirectToAction("PatientDashboard");
            }
            else
            {
                return View(data);
            }
           
        }

        public IActionResult Privacy()
        {
            return View();
        }


    }
}