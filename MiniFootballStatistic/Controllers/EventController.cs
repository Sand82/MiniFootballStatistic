using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniFootballStatistic.Services.Events;

namespace MiniFootballStatistic.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        public IActionResult All()
        {
            var model = eventService.GetTournaments();

            if (!model.Any())
            {
                return NotFound();
            }

            return View(model);
        }
        
        public IActionResult Info(int id)
        {
            var tournament = eventService.GetTournamentById(id);

            if (tournament is null)
            {
                return NotFound();
            }           

            return View();
        }


        [Authorize]
        public IActionResult Delete(int id)
        {            
            var tournament = eventService.GetTournamentById(id);

            if (tournament is null)
            {
                return NotFound();
            }

            eventService.DeleteTournament(tournament);            

            return Redirect("");
        }
    }
}
