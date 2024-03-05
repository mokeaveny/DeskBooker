using DeskBooker.Core.Domain;
using DeskBooker.DataAccess.Repository;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace DeskBooker.DataAccess.Tests.Repository
{
    public class DeskBookingRepositoryTests
    {
        private readonly string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DeskBooker;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private readonly DeskBookingRepository deskBookingRepository;

        public DeskBookingRepositoryTests()
        {
            this.deskBookingRepository = new DeskBookingRepository(new SqlConnection(connectionString));
        }

        [Fact]
        public async void ShouldInsertDeskBooking()
        {
            // Arrange
            DeskBooking newDeskBooking = new DeskBooking();
            newDeskBooking.Id = 8;
            newDeskBooking.FirstName = "Michael";
            newDeskBooking.LastName = "Keaveny";
            newDeskBooking.Email = "test123@gmail.com";
            newDeskBooking.Date = new DateTime(2024, 2, 29, 9, 0, 0);
            newDeskBooking.DeskId = 1;

            // Act 
            await this.deskBookingRepository.InsertDeskBooking(newDeskBooking);
            DeskBooking retrievedDeskBooking = await this.deskBookingRepository.
                GetDeskBooking(newDeskBooking.Id);

            // Assert
            Assert.Equal(newDeskBooking, retrievedDeskBooking, new DeskBookingEqualityComparer());
        }

        [Fact]
        public async void ShouldDeleteDeskBooking()
        {
            // Act
            await this.deskBookingRepository.DeleteDeskBooking(1);
            DeskBooking retrievedDeskBooking = await this.deskBookingRepository.GetDeskBooking(1);

            // Assert
            Assert.Null(retrievedDeskBooking);
        }

        private class DeskBookingEqualityComparer : IEqualityComparer<DeskBooking>
        {
            public bool Equals([AllowNull] DeskBooking x, [AllowNull] DeskBooking y)
            {
                return x.Id == y.Id && x.FirstName == y.FirstName && x.LastName == y.LastName
                    && x.Email == y.Email && x.Date == y.Date && x.DeskId == y.DeskId;
            }

            public int GetHashCode([DisallowNull] DeskBooking obj)
            {
                return obj.Id.GetHashCode();
            }
        }
    }
}
