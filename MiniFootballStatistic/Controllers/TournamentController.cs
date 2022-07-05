
using MiniFootballStatistic.Infrastructure;
using MiniFootballStatistic.Models.Tournament.TournamentPost;
using MiniFootballStatistic.Services.Tournaments;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> FirstStep()
        {
            var model = await tournamentService.GetSchemasAsync();

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> SecondStep(int positionCount)
        {
            var model = await GeneratePostDataModel(positionCount);

            model.TournamentPositions = positionCount;

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SecondStep(TournamentPostModel model)
        {
            var neededCount = model.Teams.Count();

            model.TournamentPositions = neededCount;

            var isNameFree = await tournamentService.CheckForFreeTournamentName(model.Name);

            if (isNameFree)
            {
                ModelState.AddModelError("Tournament", "This Tournament name is already taken.");
            }

            if (!ModelState.IsValid)
            {              
               return View(model);
            }            

            var userId = User.GetId();

            var creationDate = DateTime.UtcNow;

            var isAddedInDataBase = await tournamentService.CreateTournament(model, userId, creationDate);

            if (isAddedInDataBase)
            {
               await tournamentService.FinishedTournament(userId);
            }
            else 
            {
                return BadRequest();
            }

            return RedirectToAction("Index", "Home");
        }

        private Task<TournamentPostModel> GeneratePostDataModel(int positionCount)
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

            return Task.FromResult(model);
        }
    }
}
