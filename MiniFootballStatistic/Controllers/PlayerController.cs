using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniFootballStatistic.Models.Players;
using MiniFootballStatistic.Models.ThirdPart;
using MiniFootballStatistic.Services.Players;

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
        public IActionResult Add(int teamId)
        {
            var model = playerService.GetTeamById(teamId);          

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(TeamPlayerPostModel model)
        {
            if (model.Players.Count < 4)
            {
                ModelState.AddModelError("Players", "Cannot be lest than 4 peopels.");
            }

            if (!ModelState.IsValid)
            {
                model = playerService.GetTeamById(model.Id);

                return View(model);
            }

            return View(model);
        }
    }
}
