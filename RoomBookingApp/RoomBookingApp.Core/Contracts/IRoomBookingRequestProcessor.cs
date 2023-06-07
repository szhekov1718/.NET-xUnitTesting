using RoomBookingApp.Core.Models;

namespace RoomBookingApp.Core.Contracts
{
    public interface IRoomBookingRequestProcessor
    {
        RoomBookingResult BookRoom(RoomBookingRequest bookingRequest);
    }
}