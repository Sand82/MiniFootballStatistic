using Microsoft.AspNetCore.Mvc;

namespace MiniFootballStatistic.Controllers
{
    public class EventController : Controller
    {
        public IActionResult All()
        {
            return View();
        }
    }
}
