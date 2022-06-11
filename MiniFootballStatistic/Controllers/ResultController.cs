using MiniFootballStatistic.Models.Team;
using MiniFootballStatistic.Services.Api;

using static MiniFootballStatistic.GlobalConstants.Constans;

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

            var IsEmptyTeamScore = false;

            if (team.PositionResult == null)
            {
                IsEmptyTeamScore = true;
            }            

            if (team is null)
            {
                return NotFound();
            }

            teamService.SetStatistic(team, model.Goals);


            if (model.TeamId % 2 == 0 && IsEmptyTeamScore)
            {
                var previusTeam = teamService.FindTeam(model.TournamentId, model.TeamId - 1);

                teamService.AdjustStatistic(previusTeam, team);
            }            

            return Ok();
        }
    }
}
