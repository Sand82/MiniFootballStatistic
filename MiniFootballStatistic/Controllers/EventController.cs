using MiniFootballStatistic.Models.Event;
using MiniFootballStatistic.Services.Events;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

            var blankModels = CreateEmptyTeamModels(model.ShcemaLength, model.Teams.Count);

            model.Teams.AddRange(blankModels);

            return View(model);
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

        private List<InfoTeamModel> CreateEmptyTeamModels(int shcemaLength, int existingModelsCount)
        {
            var emptyModels = new List<InfoTeamModel>();

            for (int i = existingModelsCount; i < (shcemaLength * 2) - 2; i++)
            {
                var model = new InfoTeamModel { Name = "Not played yet" };

                emptyModels.Add(model);
            }

            return emptyModels;
        }

    }
}
