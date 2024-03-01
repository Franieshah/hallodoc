using HalloDocApp.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocApp.Entities.ViewModels.Admin
{
    public class ViewCase
    {
        public Requestclient requestclient { get; set; } = new Requestclient();

        public int status { get; set; }

        public int requestType { get; set; }
        public string confirmationNumber { get; set; }
    }
}
