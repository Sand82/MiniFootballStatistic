using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniFootballStatistic.Models.Event;
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
            var model = eventService.GetTournamentModelById(id);

            if (model is null)
            {
                return NotFound();
            }

            var blankModels = CreateEmptyTeamModels(model.ShcemaLength);

            model.Teams.AddRange(blankModels);

            return View(model);
        }

        private List<InfoTeamModel> CreateEmptyTeamModels(int shcemaLength)
        {
            var emptyModels = new List<InfoTeamModel>();

            for (int i = 0; i < shcemaLength - 2; i++)
            {
                var model = new InfoTeamModel { Name = "Not playe"};

                emptyModels.Add(model);
            }

            return emptyModels;
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
