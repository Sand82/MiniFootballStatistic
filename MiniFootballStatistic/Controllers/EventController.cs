using MiniFootballStatistic.Models.Event;
using MiniFootballStatistic.Services.Events;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniFootballStatistic.Models.Tournament.TurnamentEdit;
using MiniFootballStatistic.Models.Tournament.TournamentEdit;

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
            var model = eventService.GetInfoViewModel(id);

            if (model is null)
            {
                return NotFound();
            }

            var blankModels = CreateEmptyTeamInfoModels(model.ShcemaLength, model.Teams.Count); // TODO send object and sheck for missing nested objects

            model.Teams.AddRange(blankModels);

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

            var blankModels = CreateEmptyTeamEditModels(model.SchemaLenght, model.Teams.Count); // TODO send object and sheck for missing nested objects

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

        public IActionResult Test()
        {
            return View();
        }

        private List<InfoTeamModel> CreateEmptyTeamInfoModels(int shcemaLength, int existingModelsCount)
        {
            var emptyModels = new List<InfoTeamModel>();

            for (int i = existingModelsCount; i < (shcemaLength * 2) - 2; i++)
            {
                var model = new InfoTeamModel { Name = "Not played yet" };

                emptyModels.Add(model);
            }

            return emptyModels;
        }

        private List<TeamEditModel> CreateEmptyTeamEditModels(int shcemaLength, int existingModelsCount)
        {
            var emptyModels = new List<TeamEditModel>();

            for (int i = existingModelsCount; i < (shcemaLength * 2) - 2; i++)
            {
                var model = new TeamEditModel { Name = "Not played yet", TournamentPosition = i + 1, PositionResult = 0};

                emptyModels.Add(model);
            }

            return emptyModels;
        }
    }
}
