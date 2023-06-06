using RoomBookingApp.Core.Enums;
using RoomBookingApp.Core.Models.Base;

namespace RoomBookingApp.Core.Models
{
    public class RoomBookingResult : RoomBookingBase
    {
        public BookingResultFlag Flag { get; set; }

        public int? RoomBookingId { get; set; }
    }
}