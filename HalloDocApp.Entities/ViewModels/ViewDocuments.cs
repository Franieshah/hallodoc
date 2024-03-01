using Microsoft.AspNetCore.Http;
using HalloDocApp.Entities.DataModels;
namespace HalloDocApp.Entities.ViewModels
{
    public class ViewDocuments
    {

        public IEnumerable<Requestwisefile>? requestWiseFiles { get; set; }
        public Request request { get; set; } = new Request();

        public Requestclient requestClient { get; set; } = new Requestclient();
        public List<IFormFile>? Files { get; set; }
    }
}
