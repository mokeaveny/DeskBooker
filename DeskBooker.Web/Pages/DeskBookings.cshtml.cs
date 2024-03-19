using DeskBooker.Core.Domain;
using DeskBooker.Web.Integrations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeskBooker.Web.Pages
{
    public class DeskBookingsModel : PageModel
    {
        private readonly IDeskBookerDataAccessApiClient deskBookerDataAccessApiClient;
        public DeskBookingsModel(IDeskBookerDataAccessApiClient deskBookerDataAccessApiClient)
        {
            this.deskBookerDataAccessApiClient = deskBookerDataAccessApiClient;
        }

        public List<DeskBooking> DeskBookings;

        public async Task OnGet()
        {
            this.DeskBookings = await this.deskBookerDataAccessApiClient.GetAllDeskBookings();
        }
    }
}
