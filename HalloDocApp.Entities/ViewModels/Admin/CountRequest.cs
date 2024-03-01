using HalloDocApp.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocApp.Entities.ViewModels.Admin
{
    public class CountRequest
    {
        public List<Request> Requests { get; set; }
 
        public int newState { get; set; }
        public int pendingState { get; set; }
        public int activeState { get; set; }
        public int concludeState { get; set; }
        public int closedState { get; set; }
        public int unpaidState { get; set; }
        public List<Region> regions { get; set; } = new List<Region>();

    }
}
