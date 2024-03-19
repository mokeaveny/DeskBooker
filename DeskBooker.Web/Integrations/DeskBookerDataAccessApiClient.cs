using DeskBooker.Core.Domain;

namespace DeskBooker.Web.Integrations
{
    public class DeskBookerDataAccessApiClient : IDeskBookerDataAccessApiClient
    {
        private readonly HttpClient httpClient;

        public DeskBookerDataAccessApiClient(IConfiguration config, HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri(config.GetValue<string>("DeskBookerDataAccessApiUrl"));
        }

        public async Task<List<Desk>> GetAllDesks()
        {
            var requestUri = "Desk/GetAllDesks";
            return await httpClient.GetFromJsonAsync<List<Desk>>(requestUri);
        }

        public async Task<List<Desk>> GetAvailableDesks(DateTime date)
        {
            var requestUri = "Desk/GetAvailableDesks?date=" + date;
            return await httpClient.GetFromJsonAsync<List<Desk>>(requestUri);
        }

        public async Task<List<DeskBooking>> GetAllDeskBookings()
        {
            var requestUri = "DeskBooking/GetAllDeskBookings";
            return await httpClient.GetFromJsonAsync<List<DeskBooking>>(requestUri);
        }
    }
}
