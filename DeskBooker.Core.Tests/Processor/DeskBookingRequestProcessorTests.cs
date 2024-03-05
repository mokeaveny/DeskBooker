using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Domain;
using Moq;

namespace DeskBooker.Core.Processor
{
    public class DeskBookingRequestProcessorTests
    {
        private readonly DeskBookingRequest request;
        private readonly List<Desk> availableDesks;
        private readonly Mock<IDeskBookingRepository> deskBookingRepositoryMock;
        private readonly Mock<IDeskRepository> deskRepositoryMock;
        private readonly DeskBookingRequestProcessor processor;

        public DeskBookingRequestProcessorTests()
        {
            request = new DeskBookingRequest();
            request.FirstName = "Thomas";
            request.LastName = "Huber";
            request.Email = "thomas@thomasclaudiushuber.com";
            request.Date = new DateTime(2020, 1, 28);

            availableDesks = new List<Desk> { new Desk { Id = 7 } };

            deskBookingRepositoryMock = new Mock<IDeskBookingRepository>();
            deskRepositoryMock = new Mock<IDeskRepository>();
            deskRepositoryMock.Setup(x => x.GetAvailableDesks(request.Date))
                .Returns(availableDesks);

            processor = new DeskBookingRequestProcessor(deskBookingRepositoryMock.Object,
                deskRepositoryMock.Object);
        }

        [Fact]
        public void ShouldReturnDeskBookingResultWithRequestValues()
        {
            // Act
            DeskBookingResult result = processor.BookDesk(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(request.FirstName, result.FirstName);
            Assert.Equal(request.LastName, result.LastName);
            Assert.Equal(request.Email, result.Email);
            Assert.Equal(request.Date, result.Date);
        }

        [Fact]
        public void ShouldThrowExceptionIfRequestIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => processor.BookDesk(null));

            Assert.Equal("request", exception.ParamName);
        }

        [Fact]
        public void ShouldSaveDeskBooking()
        {
            // Arrange
            DeskBooking savedDeskBooking = null;
            deskBookingRepositoryMock.Setup(x => x.Save(It.IsAny<DeskBooking>()))
                .Callback<DeskBooking>(deskBooking =>
                {
                    savedDeskBooking = deskBooking;
                });

            // Act
            processor.BookDesk(request);

            deskBookingRepositoryMock.Verify(x => x.Save(It.IsAny<DeskBooking>()), Times.Once);

            // Assert
            Assert.NotNull(savedDeskBooking);
            Assert.Equal(request.FirstName, savedDeskBooking.FirstName);
            Assert.Equal(request.LastName, savedDeskBooking.LastName);
            Assert.Equal(request.Email, savedDeskBooking.Email);
            Assert.Equal(request.Date, savedDeskBooking.Date);
            Assert.Equal(availableDesks.First().Id, savedDeskBooking.DeskId);
        }

        [Fact]
        public void ShouldNotSaveDeskBookingIfNoDeskIsAvailable()
        {
            availableDesks.Clear();

            // Act
            processor.BookDesk(request);

            deskBookingRepositoryMock.Verify(x => x.Save(It.IsAny<DeskBooking>()), Times.Never);
        }

        [Theory]
        [InlineData(DeskBookingResultCode.Success, true)]
        [InlineData(DeskBookingResultCode.NoDeskAvailable, false)]
        public void ShouldReturnExpectedResultCode(DeskBookingResultCode expectedResultCode,
            bool isDeskAvailable)
        {
            if (isDeskAvailable == false)
            {
                availableDesks.Clear();
            }

            DeskBookingResult result = processor.BookDesk(request);

            Assert.Equal(expectedResultCode, result.Code);
        }

        [Theory]
        [InlineData(5, true)]
        [InlineData(null, false)]
        public void ShouldReturnExpectedDeskBookingId(int? expectedDeskBookingId, 
            bool isDeskAvailable)
        {
            if (!isDeskAvailable)
            {
                availableDesks.Clear();
            }
            else
            {
                deskBookingRepositoryMock.Setup(x => x.Save(It.IsAny<DeskBooking>()))
                    .Callback<DeskBooking>(deskBooking =>
                    {
                        deskBooking.Id = expectedDeskBookingId.Value;
                    });
            }

            DeskBookingResult result = processor.BookDesk(request);

            Assert.Equal(expectedDeskBookingId, result.DeskBookingId);
        }
    }
}
