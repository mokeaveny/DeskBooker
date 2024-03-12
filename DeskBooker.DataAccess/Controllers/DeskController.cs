using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DeskBooker.DataAccess.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DeskController : Controller
    {
        private readonly IDeskRepository deskRepository;
        public DeskController(IDeskRepository deskRepository)
        {
            this.deskRepository = deskRepository;
        }

        [HttpGet]
        public ActionResult<List<Desk>> GetAvailableDesks(DateTime date)
        {
            List<Desk> desks = deskRepository.GetAvailableDesks(date);
            if (desks == null)
            {
                return NotFound();
            }
            return Ok(desks);
        }
    }
}
