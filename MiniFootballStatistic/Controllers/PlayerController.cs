using MiniFootballStatistic.Infrastructure;
using MiniFootballStatistic.Models.Players;
using MiniFootballStatistic.Services.Players;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MiniFootballStatistic.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayerService playerService;

        public PlayerController(IPlayerService playerService)
        {
            this.playerService = playerService;
        }

        [Authorize]
        public IActionResult Add()
        {
            var userId = User.GetId();

            var model = playerService.GetTeamByTeamId(userId);          

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(TeamPlayerPostModel model, int modelId, int TeamsCount)
        {
            //int registeredPlayerCounter = model.Players.Where(p => p.Name == null).Count();            

            //if (registeredPlayerCounter > 4)
            //{
            //    ModelState.AddModelError("Players", "Cannot be lest than 4 peopels.");
            //}

            if (!ModelState.IsValid)
            {
                var userId = User.GetId();

                model = playerService.GetTeamByTeamId(userId);

                return View(model);
            }

            int counter = 1;

            playerService.CreatePlayers(model, modelId);

            if (counter < TeamsCount)
            {
                counter++;

                var userId = User.GetId();

                model = playerService.GetTeamByTeamId(userId);

                return RedirectToAction("Player", "Add");
            }

            return RedirectToAction("/");
        }
    }
}
