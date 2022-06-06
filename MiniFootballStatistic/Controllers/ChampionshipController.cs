using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniFootballStatistic.Infrastructure;
using MiniFootballStatistic.Models.Championship;
using MiniFootballStatistic.Services.Championship;

namespace MiniFootballStatistic.Controllers
{
    public class ChampionshipController : Controller
    {
        private readonly IChampionshipService championshipService;

        public ChampionshipController(IChampionshipService championshipService)
        {
            this.championshipService = championshipService;
        }

        [Authorize]
        public IActionResult FirstStep()
        {
            var model = championshipService.GetSchemas();

            return View(model);
        }

        [Authorize]
        public IActionResult SecondStep(int positionCount)
        {
            ChampionshipPostModel model = new ();

            model.TournamentPositions = positionCount;

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult SecondStep(ChampionshipPostModel model)
        {
            if (!ModelState.IsValid)
            {
                ChampionshipPostModel newModel = new ();

                newModel.TournamentPositions = model.Teams.Count();

                return View(newModel);
            }

            model.TournamentPositions = model.Teams.Count();

            var userId = User.GetId();

            championshipService.CreateChampionship(model, userId);

            return RedirectToAction("ThirdPart", "Championship");
        }

        [Authorize]
        public IActionResult ThirdPart()
        {           

            return View();
        }
    }
}
