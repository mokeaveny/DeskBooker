using DeskBooker.Core.Domain;
using DeskBooker.Web.Integrations;
using DeskBooker.Web.Pages;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooker.Web.Tests.Pages
{
    public class DeskBookingsModelTests
    {
        [Fact]
        public async Task ShouldGetAllDeskBookings()
        {
            // Arrange
            List<DeskBooking> deskBookings = new List<DeskBooking>();
            deskBookings.Add(new DeskBooking());
            deskBookings.Add(new DeskBooking());
            deskBookings.Add(new DeskBooking());

            Mock<IDeskBookerDataAccessApiClient> apiClientMock = new Mock<IDeskBookerDataAccessApiClient>();
            apiClientMock.Setup(x => x.GetAllDeskBookings())
                .ReturnsAsync(deskBookings);

            DeskBookingsModel deskBookingsModel = new DeskBookingsModel(apiClientMock.Object);

            // Act
            await deskBookingsModel.OnGet();

            // Assert
            Assert.Equal(deskBookings, deskBookingsModel.DeskBookings);
        }
    }
}
