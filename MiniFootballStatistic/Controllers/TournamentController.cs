using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MiniFootballStatistic.Infrastructure;
using MiniFootballStatistic.Models.Tournament.TournamentPost;
using MiniFootballStatistic.Services.Tournaments;

namespace MiniFootballStatistic.Controllers
{
    public class TournamentController : Controller
    {
        private readonly ITournamentService tournamentService;

        public TournamentController(ITournamentService tournamentService)
        {
            this.tournamentService = tournamentService;
        }

        [Authorize]
        public IActionResult FirstStep()
        {
            var model = tournamentService.GetSchemas();

            return View(model);
        }

        [Authorize]
        public IActionResult SecondStep(int positionCount)
        {
            var model = GeneratePostDataModel(positionCount);

            model.TournamentPositions = positionCount;

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult SecondStep(TournamentPostModel model)
        {
            var neededCount = model.Teams.Count();

            model.TournamentPositions = neededCount;

            if (!ModelState.IsValid)
            {              
               return View(model);
            }            

            var userId = User.GetId();

            var creationDate = DateTime.UtcNow;

            var isAddedInDataBase = tournamentService.CreateTournament(model, userId, creationDate);

            if (isAddedInDataBase)
            {
                tournamentService.FinishedTournament(userId);
            }

            return RedirectToAction("Index", "Home");
        }

        private TournamentPostModel GeneratePostDataModel(int positionCount)
        {
            TournamentPostModel model = new();

            for (int i = 0; i < positionCount; i++)
            {
                var team = new TeamPostModel();

                for (int j = 0; j < 4; j++)
                {
                    team.Players.Add(new PlayerPostModel());
                }

                model.Teams.Add(team);
            }

            return model;
        }
    }
}
