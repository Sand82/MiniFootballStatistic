using Microsoft.AspNetCore.Mvc;
using MiniFootballStatistic.Models.Team;
using MiniFootballStatistic.Services.Api;

namespace MiniFootballStatistic.Controllers
{
    [ApiController]   
    [Route("api/[controller]")]
    public class TeamNameController : Controller
    {
        private readonly IApiService teamService;

        public TeamNameController(IApiService teamService)
        {
            this.teamService = teamService;
        }

        public IActionResult Name(GetNameEditModel model) 
        {
            var team = teamService.FindTeam(model.TournamentId, model.TeamId);

            if (team is null)
            {
                return NotFound();
            }

            teamService.SetName(team, model.TeamName);

            return Ok();
        }
    }
}
