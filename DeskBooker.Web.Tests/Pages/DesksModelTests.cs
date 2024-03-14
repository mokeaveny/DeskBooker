using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Domain;
using DeskBooker.Web.Integrations;
using DeskBooker.Web.Pages;
using Moq;

namespace DeskBooker.Web.Tests.Pages
{
    public class DesksModelTests
    {
        [Fact]
        public async Task ShouldGetAllDesks()
        {
            // Arrange
            List<Desk> desks = new List<Desk>();
            desks.Add(new Desk());
            desks.Add(new Desk());
            desks.Add(new Desk());

            Mock<IDeskBookerDataAccessApiClient> apiClientMock = new Mock<IDeskBookerDataAccessApiClient>();
            apiClientMock.Setup(x => x.GetAllDesks())
                .ReturnsAsync(desks);

            DesksModel desksModel = new DesksModel(apiClientMock.Object);

            // Act
            await desksModel.OnGet();

            // Assert
            Assert.Equal(desks, desksModel.Desks);
        }
    }
}
