using DeskBooker.Core.Domain;

namespace DeskBooker.Web.Integrations
{
    public interface IDeskBookerDataAccessApiClient
    {
        Task<List<Desk>> GetAllDesks();
        Task<List<Desk>> GetAvailableDesks(DateTime date);
    }
}
