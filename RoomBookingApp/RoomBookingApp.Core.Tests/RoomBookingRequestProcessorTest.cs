﻿using Moq;
using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Domain;
using RoomBookingApp.Core.Enums;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;
using Shouldly;

namespace RoomBookingApp.Core.Tests
{
    public class RoomBookingRequestProcessorTest
    {
        private readonly RoomBookingRequestProcessor _processor;
        private readonly RoomBookingRequest _roomBookingRequest;
        private readonly List<Room> _availableRooms;
        private Mock<IRoomBookingService> _roomBookingServiceMock;

        public RoomBookingRequestProcessorTest()
        {
            _roomBookingRequest = new RoomBookingRequest()
            {
                FullName = "Test Test",
                Email = "test@abv.bg",
                Date = new DateTime(2023, 6, 5)
            };

            _availableRooms = new List<Room>() { new Room() { Id = 1 } };

            _roomBookingServiceMock = new Mock<IRoomBookingService>();

            _roomBookingServiceMock.Setup(r => r.GetAvailableRooms(_roomBookingRequest.Date)).Returns(_availableRooms);

            _processor = new RoomBookingRequestProcessor(_roomBookingServiceMock.Object);
        }

        [Fact]
        public void Should_Return_Room_Booking_Response_With_Request_Values()
        {
            var request = new RoomBookingRequest
            {
                FullName = "Test Name",
                Email = "test@request.com",
                Date = new DateTime(2021, 10, 20)
            };

            RoomBookingResult result = _processor.BookRoom(request);

            // Assert.NotNull(result);
            // Assert.Equal(request.FullName, result.FullName);
            // Assert.Equal(request.Email, result.Email);
            // Assert.Equal(request.Date, result.Date);

            result.ShouldNotBeNull();
            result.FullName.ShouldBe(request.FullName);
            result.Email.ShouldBe(request.Email);
            result.Date.ShouldBe(request.Date);
        }

        [Fact]
        public void Should_Throw_Exception_For_Null_Request()
        {
            var exception = Should.Throw<ArgumentNullException>(() => _processor.BookRoom(null));

            _roomBookingServiceMock.Verify(q => q.Save(It.IsAny<RoomBooking>()), Times.Never);

            exception.ParamName.ShouldBe("bookingRequest");
        }

        [Fact]
        public void Should_Save_Room_Booking_Request()
        {
            RoomBooking savedBooking = null;

            _roomBookingServiceMock.Setup(q => q.Save(It.IsAny<RoomBooking>()))
            .Callback<RoomBooking>(booking =>
                {
                    savedBooking = booking;
                });

            _processor.BookRoom(_roomBookingRequest);

            _roomBookingServiceMock.Verify(q => q.Save(It.IsAny<RoomBooking>()), Times.Once);

            savedBooking.ShouldNotBeNull();
            savedBooking.FullName.ShouldBe(_roomBookingRequest.FullName);
            savedBooking.Email.ShouldBe(_roomBookingRequest.Email);
            savedBooking.Date.ShouldBe(_roomBookingRequest.Date);
            savedBooking.RoomId.ShouldBe(_availableRooms.First().Id);
        }

        [Fact]
        public void Should_Not_Save_Room_Booking_Request_If_None_Available()
        {
            _availableRooms.Clear();

            _processor.BookRoom(_roomBookingRequest);

            _roomBookingServiceMock.Verify(q => q.Save(It.IsAny<RoomBooking>()), Times.Never);
        }

        [Theory]
        [InlineData(BookingResultFlag.Failure, false)]
        [InlineData(BookingResultFlag.Success, true)]
        public void Should_Return_SuccessOrFailure_Flag_In_Result(BookingResultFlag bookingSuccessFlag, bool isAvailable)
        {
            if (!isAvailable)
            {
                _availableRooms.Clear();
            }

            var result = _processor.BookRoom(_roomBookingRequest);

            bookingSuccessFlag.ShouldBe(result.Flag);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(null, false)]
        public void Should_Return_RoomBookingId_In_Result(int? roomBookingId, bool isAvailable)
        {
            if (!isAvailable)
            {
                _availableRooms.Clear();
            }
            else
            {
                _roomBookingServiceMock.Setup(q => q.Save(It.IsAny<RoomBooking>()))
               .Callback<RoomBooking>(booking =>
               {
                   booking.Id = roomBookingId.Value;
               });
            }

            var result = _processor.BookRoom(_roomBookingRequest);

            result.RoomBookingId.ShouldBe(roomBookingId);
        }
    }
}