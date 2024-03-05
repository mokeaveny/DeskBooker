using DeskBooker.Core.Domain;
using DeskBooker.DataAccess.Repository;
using System.Data.SqlClient;

namespace DeskBooker.DataAccess.Tests.Repository
{
    public class DeskRepositoryTests
    {
        private readonly string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DeskBooker;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private readonly DeskRepository deskRepository;

        public DeskRepositoryTests()
        {
            this.deskRepository = new DeskRepository(new SqlConnection(connectionString));
        }

        [Fact]
        public async void ShouldReturnAvailableDesks()
        {
            // Arrange
            DateTime date = new DateTime(2024, 3, 5);

            // Act
            List<Desk> desks = await this.deskRepository.GetAvailableDesks(date);

            // Assert
            Assert.Equal(2, desks.Count());
            Assert.Contains(desks, d => d.Id == 1);
            Assert.Contains(desks, d => d.Id == 2);
            Assert.DoesNotContain(desks, d => d.Id == 3);
        }
    }
}
