using DeskBooker.Core.Domain;

namespace DeskBooker.Core.DataInterface
{
    public interface IDeskBookingRepository
    {
        Task InsertDeskBooking(DeskBooking deskBooking);
        Task<DeskBooking> GetDeskBooking(int id);
        Task<List<DeskBooking>> GetAllDeskBookings();
    }
}
