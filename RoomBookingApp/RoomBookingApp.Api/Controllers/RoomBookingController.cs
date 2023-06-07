using Microsoft.AspNetCore.Mvc;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;

namespace RoomBookingApp.Api.Controllers
{
    public class RoomBookingController : Controller
    {
        private readonly IRoomBookingRequestProcessor _processor;

        public RoomBookingController(IRoomBookingRequestProcessor processor)
        {
            _processor = processor;
        }

        //public Task BookRoom(RoomBookingRequest request)
        //{
        //    throw new NotImplementedException();
        //}

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
