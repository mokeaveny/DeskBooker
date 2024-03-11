using Microsoft.AspNetCore.Mvc;

namespace DeskBooker.DataAccess.Controllers
{
    public class DeskController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
