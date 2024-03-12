using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DeskBooker.DataAccess.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DeskBookingController : Controller
    {
        private readonly IDeskBookingRepository deskBookingRepository;
        public DeskBookingController(IDeskBookingRepository deskBookingRepository)
        {
            this.deskBookingRepository = deskBookingRepository;
        }

        [HttpGet]
        public async Task<ActionResult<DeskBooking>> Get(int id)
        {
            DeskBooking deskBooking = await deskBookingRepository.GetDeskBooking(id);
            if (deskBooking == null)
            {
                return NotFound();
            }
            return Ok(deskBooking);
        }

        [HttpPost]
        public async Task Post(DeskBooking deskBooking)
        {
            await deskBookingRepository.InsertDeskBooking(deskBooking);
        }
    }
}
