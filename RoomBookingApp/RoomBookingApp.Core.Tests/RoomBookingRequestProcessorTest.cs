using Moq;
using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;
using Shouldly;

namespace RoomBookingApp.Core.Tests
{
    public class RoomBookingRequestProcessorTest
    {
        private readonly RoomBookingRequestProcessor _processor;
        private readonly RoomBookingRequest _roomBookingRequest;
        private Mock<IRoomBookingService> _roomBookingServiceMock;

        public RoomBookingRequestProcessorTest()
        {
            _roomBookingRequest = new RoomBookingRequest()
            {
                FullName = "Test Test",
                Email = "test@abv.bg",
                Date = new DateTime(2023, 6, 5)
            };

            _roomBookingServiceMock = new Mock<IRoomBookingService>();

            _processor = new RoomBookingRequestProcessor(_roomBookingServiceMock.Object);
        }

        [Fact]
        public void Should_Return_Room_Booking_Response_With_Request_Values()
        {
            //Arrange

            var request = new RoomBookingRequest
            {
                FullName = "Test Name",
                Email = "test@request.com",
                Date = new DateTime(2021, 10, 20)
            };

            //Act

            RoomBookingResult result = _processor.BookRoom(request);

            //Assert

            // Assert.NotNull(result);
            // Assert.Equal(request.FullName, result.FullName);
            // Assert.Equal(request.Email, result.Email);
            // Assert.Equal(request.Date, result.Date);

            result.ShouldNotBeNull();
            result.FullName.ShouldBe(request.FullName);
            result.Email.ShouldBe(request.Email);
            result.Date.ShouldBe(request.Date);
        }
    }
}