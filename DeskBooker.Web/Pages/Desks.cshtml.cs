using DeskBooker.Core.Domain;
using DeskBooker.Web.Integrations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeskBooker.Web.Pages
{
    public class DesksModel : PageModel
    {
        private readonly IDeskBookerDataAccessApiClient deskBookerDataAccessApiClient;

        public DesksModel(IDeskBookerDataAccessApiClient deskBookerDataAccessApiClient)
        {
            this.deskBookerDataAccessApiClient = deskBookerDataAccessApiClient;
        }

        public List<Desk> Desks { get; set; }

        public async Task OnGet()
        {
            this.Desks = await deskBookerDataAccessApiClient.GetAllDesks();
        }
    }
}
