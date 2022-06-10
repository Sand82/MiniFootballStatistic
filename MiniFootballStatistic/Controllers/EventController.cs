using MiniFootballStatistic.Services.Events;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniFootballStatistic.Models.Tournament.TurnamentEdit;

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
           
            return View(model);
        }

        public IActionResult Info(int id)
        {
            var model = eventService.GetInfoViewModel(id);

            if (model is null)
            {
                return NotFound();
            }
            
            return View(model);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var model = eventService.GetTournamentEditModel(id);

            if (model is null)
            {
                return NotFound();
            }         
                      
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(TournamentEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return Redirect("/");
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

            return Redirect("/");
        }        
    }
}
