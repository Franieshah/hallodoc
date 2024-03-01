using HalloDocApp.Entities.DataContext;
using HalloDocApp.Entities.ViewModels.Admin;
using HalloDocApp.Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using HalloDocApp.Entities.Enum;

namespace HalloDocApp.Controllers
{
    public class AdminController : Controller
    {

        private readonly ApplicationDBContext _context;
        public AdminController(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("admin/index", Name = "AdminLogin")]
        public IActionResult AdminLogin()
        {
            return View();
        }

        [Route("admin/forgot-password", Name = "ForgotPassword")]
        public IActionResult AdminForgotPassword()
        {
            return View();
        }

        [Route("admin/dashboard", Name = "Dashboard")]
        public IActionResult Dashboard()
        {
            CountRequest data = new CountRequest();
            data.Requests = _context.Requests.ToList();

            data.newState = data.Requests.Count(a => a.Status == 1);
            data.pendingState = data.Requests.Count(a => a.Status == 2);
            data.activeState = data.Requests.Count(a => a.Status == 3);
            data.concludeState = data.Requests.Count(a => a.Status == 4);
            data.closedState = data.Requests.Count(a => a.Status == 5);
            data.unpaidState = data.Requests.Count(a => a.Status == 6);

            data.regions = _context.Regions.ToList();
            return View(data);
        }

        [Route("admin/ProviderLocation", Name = "ProviderLocation")]
        public IActionResult ProviderLocation()
        {
            return View();
        }

        [Route("admin/Provider", Name = "Provider")]
        public IActionResult Provider()
        {
            return View();
        }

        [Route("admin/Partners", Name = "Partners")]
        public IActionResult Partners()
        {
            return View();
        }

        [Route("admin/Profile", Name = "Profile")]
        public IActionResult MyProfile()
        {
            return View();
        }

        [Route("admin/AccessRecords", Name = "AccessRecords")]
        public IActionResult AccessRecords()
        {
            return View();
        }

        [Route("admin/New", Name = "New")]
        public IActionResult NewState(int status, int region, int requestType, string searchString)
        {
            Dashboard data = new Dashboard();
            List<Request> requests=new List<Request>();
            List<Requestclient> nullList = new List<Requestclient>();
            if (requestType == 0)

            {
                requests = _context.Requests.Where(a => a.Status == 1).ToList();
            }
            else
            {
                requests = _context.Requests.Where(a => a.Status == 1 && a.Requesttypeid == requestType).ToList();

            }
            if (searchString == null || searchString == "")
            {
                data.RequestClients = _context.Requestclients.ToList();
            }
            else
            {
                data.RequestClients = _context.Requestclients.Where(a => a.Firstname.ToLower().Contains(searchString.Trim().ToLower()) || a.Lastname.ToLower().Contains(searchString.Trim().ToLower())).ToList();
            }
            foreach (var client in data.RequestClients)
            {
                var temp = requests.FirstOrDefault(a => a.Requestid == client.Requestid);
                if (temp != null)
                {
                    data.Requests.Add(temp);
                }
                else
                {
                    nullList.Add(client);
                }
            }
            foreach(var i in nullList)


            {
                data.RequestClients.Remove(i);
            }


            return View(data);
        }

        [Route("admin/Pending", Name = "Pending")]
        public IActionResult PendingState()
        {
            Dashboard data = new Dashboard();
            data.Requests = _context.Requests.Where(a => a.Status == 2).ToList();

            foreach (var request in data.Requests)
            {
                data.RequestClients.Add(_context.Requestclients.FirstOrDefault(a => a.Requestid == request.Requestid));
            }
            return View(data);
        }

        [Route("admin/Active", Name = "Active")]
        public IActionResult ActiveState()
        {
            Dashboard data = new Dashboard();
            data.Requests = _context.Requests.Where(a => a.Status == 3).ToList();

            foreach (var request in data.Requests)
            {
                data.RequestClients.Add(_context.Requestclients.FirstOrDefault(a => a.Requestid == request.Requestid));
            }
            return View(data);
        }

        [Route("admin/Conclude", Name = "Conclude")]
        public IActionResult ConcludeState()
        {
            Dashboard data = new Dashboard();
            data.Requests = _context.Requests.Where(a => a.Status == 4).ToList();

            foreach (var request in data.Requests)
            {
                data.RequestClients.Add(_context.Requestclients.FirstOrDefault(a => a.Requestid == request.Requestid));
            }
            return View(data);
        }

        [Route("admin/Close", Name = "Close")]
        public IActionResult CloseState()
        {
            Dashboard data = new Dashboard();
            data.Requests = _context.Requests.Where(a => a.Status == 5).ToList();

            foreach (var request in data.Requests)
            {
                data.RequestClients.Add(_context.Requestclients.FirstOrDefault(a => a.Requestid == request.Requestid));
            }
            return View(data);
        }

        [Route("admin/Unpaid", Name = "Unpaid")]
        public IActionResult UnpaidState()
        {
            Dashboard data = new Dashboard();
            data.Requests = _context.Requests.Where(a => a.Status == 6).ToList();

            foreach (var request in data.Requests)
            {
                data.RequestClients.Add(_context.Requestclients.FirstOrDefault(a => a.Requestid == request.Requestid));
            }
            return View(data);
        }

        [Route("admin/viewCase", Name = "viewCase")]
        public IActionResult ViewCase(int reqId)
        {
            ViewCase viewCaseData = new ViewCase();
            viewCaseData.requestclient = _context.Requestclients.FirstOrDefault(a => a.Requestclientid == reqId);

            var requestData = _context.Requests.FirstOrDefault(a => a.Requestid == viewCaseData.requestclient.Requestid);
            viewCaseData.requestType = requestData.Requesttypeid;
            viewCaseData.status = requestData.Status;
            viewCaseData.confirmationNumber = requestData.Confirmationnumber;
            return View(viewCaseData);
        }

        [HttpPost]
        [Route("admin/CancelCase", Name = "CancelCase")]
        public IActionResult cancelCase(int reqId, string reason, string notes)
        {
            var data = _context.Requests.FirstOrDefault(a => a.Requestid == reqId);
            data.Status = (int)StatusOfRequest.ToClose;
            _context.Requests.Update(data);
            _context.SaveChanges();

            Requeststatuslog logData = new Requeststatuslog();
            logData.Requestid = data.Requestid;
            logData.Status = (int)StatusOfRequest.ToClose;
            logData.Createddate = DateTime.Now;
            _context.Add(logData);
            _context.SaveChanges();

            //Requestclosed closeRequestData = new Requestclosed();
            //closeRequestData.Requeststatuslogid = (_context.Requeststatuslogs.Max(a=>a.Requestid))+1;
            //closeRequestData.Requestid = data.Requestid;
            //closeRequestData.Phynotes = notes;

            Requestnote notesData = new Requestnote();
            notesData.Createddate = DateTime.Now;
            notesData.Createdby = "Admin";
            notesData.Requestid = data.Requestid;
            notesData.Adminnotes = reason;
            _context.Requestnotes.Add(notesData);
            _context.SaveChanges();
            return Json(new { success = true, message = "Case canceled successfully." });
        }

        [Route("admin/ViewNotes", Name = "ViewNotes")]
        public IActionResult ViewNotes(int reqId)
        {
            var requestData = _context.Requestclients.FirstOrDefault(a => a.Requestclientid == reqId);

            var data = _context.Requestnotes.FirstOrDefault(a => a.Requestid == requestData.Requestid);

           
            ViewNotes notesData = new ViewNotes();
            if (data == null)
            {
                notesData.requestId = requestData.Requestid;
                notesData.adminNotes = "-";
                notesData.physicianNotes = "-";
                notesData.transferNotes = "-";
            }
            else
            {
                notesData.requestId = requestData.Requestid;
                notesData.requestId = requestData.Requestid;
                notesData.adminNotes = data.Adminnotes;
                notesData.physicianNotes = data.Physiciannotes == null ? "-" : data.Physiciannotes;
            }
            return View(notesData);
        }

        [HttpPost]
        [Route("admin/GetDataByRegion", Name = "RegionFilter")]
        public IActionResult regionFilter()
        {
            return View();
        }

        [HttpPost]
        [Route("admin/BlockCase",Name = "BlockCase")]
        public IActionResult BlockCase(int reqId,string reason,string email,string phone)
        {
            var requestData = _context.Requests.FirstOrDefault(a => a.Requestid == reqId);
            requestData.Status = (int)StatusOfRequest.Block;
            _context.Update(requestData);
            _context.SaveChanges();

            Blockrequest blockrequest = new Blockrequest();
            blockrequest.Email = email;
            blockrequest.Phonenumber = phone;
            blockrequest.Reason = reason;
            blockrequest.Requestid = reqId.ToString();
            blockrequest.Createddate = DateTime.Now;
            _context.Add(blockrequest);
            _context.SaveChanges();

            return Json(new { success = true, message = "Case Blocked Successfully.." });
        }
    }
}
