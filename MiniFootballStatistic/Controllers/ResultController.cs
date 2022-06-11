using Microsoft.AspNetCore.Mvc;
using MiniFootballStatistic.Models.Team;
using MiniFootballStatistic.Services.Api;

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
            

            if (team is null)
            {
                return NotFound();
            }

            teamService.SetStatistic(team, model.Goals);


            if (model.TeamId % 2 == 0)
            {
                var previusTeam = teamService.FindTeam(model.TournamentId, model.TeamId - 1);

                teamService.AdjustStatistic(previusTeam, team);
            }            

            return Ok();
        }
    }
}
