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

        public async Task<IActionResult> All()
        {
            var model = await eventService.GetTournaments();
           
            return View(model);
        }

        public async Task<IActionResult> Info(int id)
        {
            var model = await eventService.GetInfoViewModel(id);

            if (model is null)
            {
                return NotFound();
            }
            
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await eventService.GetTournamentEditModel(id);

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
        public async Task<IActionResult> Delete(int id)
        {
            var tournament = await eventService.GetTournamentById(id);

            if (tournament is null)
            {
                return NotFound();
            }

            await eventService.DeleteTournament(tournament);

            return Redirect("/");
        }        
    }
}
