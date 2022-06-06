using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniFootballStatistic.Infrastructure;
using MiniFootballStatistic.Models.Tournament;
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
            TournamentPostModel model = new ();

            model.TournamentPositions = positionCount;

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult SecondStep(TournamentPostModel model)
        {
            if (!ModelState.IsValid)
            {
                TournamentPostModel newModel = new ();

                newModel.TournamentPositions = model.Teams.Count();

                return View(newModel);
            }

            model.TournamentPositions = model.Teams.Count();

            var userId = User.GetId();

            tournamentService.CreateChampionship(model, userId);

            return RedirectToAction("ThirdStep", "Championship");
        }

        [Authorize]
        public IActionResult ThirdStep()
        {
            var userId = User.GetId();

            var model = tournamentService.GetTeams(userId);

            return View(model);
        }
    }
}
