using DeskBooker.Core.Domain;

namespace DeskBooker.Core.DataInterface
{
    public interface IDeskRepository
    {
        List<Desk> GetAvailableDesks(DateTime date);
    }
}
