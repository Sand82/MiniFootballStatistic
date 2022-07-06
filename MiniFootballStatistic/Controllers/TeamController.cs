using MiniFootballStatisticServices.Models.Team;
using MiniFootballStatisticServices.Services.Api;

using Microsoft.AspNetCore.Mvc;

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

        public async Task<IActionResult> Name(GetNameEditModel model) 
        {
            var team = await teamService.FindTeam(model.TournamentId, model.TeamId);

            if (team is null)
            {
                return NotFound();
            }

            await teamService.SetName(team, model.TeamName);

            return Ok();
        }
    }
}
