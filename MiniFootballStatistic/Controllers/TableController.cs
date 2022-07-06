using MiniFootballStatisticServices.Models.Table;
using MiniFootballStatisticServices.Services.Api;

using Microsoft.AspNetCore.Mvc;

namespace MiniFootballStatistic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TableController : Controller
    {
        private readonly IApiService apiService;

        public TableController(IApiService apiService)
        {
            this.apiService = apiService;
        }

        public async Task<IActionResult> Dynamic(TableCreateModel model)
        {

            var team = await apiService.FindTeam(model.TournamentId, model.TeamId);

            if (team is null)
            {
                return BadRequest();
            }

            var players = await apiService.GetTeams(team);

            return Ok(players);
        }
    }
}
