using MiniFootballStatistic.Services.Players;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniFootballStatistic.Models.Player;
using MiniFootballStatistic.Services.Api;

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
        public IActionResult Statistic(int tournamentId, int teamId, string teamName)
        {
            var model = playerService.FindPlayers(tournamentId, teamId, teamName);

            if (model == null)
            {
                return BadRequest();
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Statistic(PlayerTeamEditModel model)
        {
            if (!ModelState.IsValid)
            {
                model = playerService.FindPlayers(model.TournamentId, model.Id, model.Name);

                return View(model);
            }

            var team = playerService.FindTeam(model.TournamentId, model.Id);

            if (team is null)
            {
                return BadRequest();
            }

            playerService.SetPlayersStatistic(model, team);

            var id = model.TournamentId;

            return RedirectToAction("Edit", "Event", new { id });
        }
    }
}
