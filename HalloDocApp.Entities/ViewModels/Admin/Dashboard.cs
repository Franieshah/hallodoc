using HalloDocApp.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocApp.Entities.ViewModels.Admin
{
    public class Dashboard
    {
        public List<Request> Requests { get; set; } = new List<Request>();

        public List<Requestclient> RequestClients { get; set; } = new List<Requestclient>();
    }
}

