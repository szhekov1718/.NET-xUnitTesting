﻿using RoomBookingApp.Core.Models.Base;

namespace RoomBookingApp.Core.Domain
{
    public class RoomBooking : RoomBookingBase
    {
        public int RoomId { get; set; }

        public int Id { get; set; }
    }
}
