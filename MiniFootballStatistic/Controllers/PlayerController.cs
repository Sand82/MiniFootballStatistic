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
        private readonly IApiService apiService;

        public PlayerController(IPlayerService playerService, IApiService apiService)
        {
            this.playerService = playerService;
            this.apiService = apiService;
        }

        [Authorize]
        public async Task<IActionResult> Statistic(int tournamentId, int teamId, string teamName)
        {
            var model = await playerService.FindPlayers(tournamentId, teamId, teamName);

            if (model == null)
            {
                return BadRequest();
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Statistic(PlayerTeamEditModel model)
        {
            if (!ModelState.IsValid)
            {
                model = await playerService.FindPlayers(model.TournamentId, model.Id, model.Name);

                return View(model);
            }

            var team = await apiService.FindTeam(model.TournamentId, model.Id);

            if (team is null)
            {
                return BadRequest();
            }

            await playerService.SetPlayersStatistic(model, team);

            var id = model.TournamentId;

            return RedirectToAction("Edit", "Event", new { id });
        }

        public async Task<IActionResult> Info(int tournamentId)
        {
            var tournament = await playerService.GetTournament(tournamentId);

            if (tournament is null)
            {
                return BadRequest();
            }

            var model = await playerService.GetTopPlayersStatistic(tournament);

            return View(model);
        }
    }
}
