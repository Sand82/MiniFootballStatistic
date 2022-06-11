using MiniFootballStatistic.Models.Team;
using MiniFootballStatistic.Services.Api;

using Microsoft.AspNetCore.Mvc;

namespace MiniFootballStatistic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultController : Controller
    {
        private readonly IApiService teamService;

        public ResultController(IApiService teamService)
        {
            this.teamService = teamService;
        }

        public IActionResult GoalsScored(GetGoalsEditModel model)
        {
            var team = teamService.FindTeam(model.TournamentId, model.TeamId);

            var initialTeamScore = team.ScoredGoals;

            teamService.SetStatistic(team, model.Goals);

            team.ScoredGoals = model.Goals;

            var positionId = model.TeamId + 1;

            if (model.TeamId % 2 == 0)
            {
                positionId -= 2;
                initialTeamScore = model.Goals;
            }

            var opponentTeam = teamService.FindTeam(model.TournamentId, positionId);

            var shemaPosition = model.SchemaLength + model.GroupNumber;

            if (initialTeamScore != null)
            {
                if (opponentTeam.ScoredGoals != team.ScoredGoals)
                {
                    teamService.AdjustStatistic(team, opponentTeam, shemaPosition, model.SchemaLength);
                }                
            }

            return Ok();
        }
    }
}
