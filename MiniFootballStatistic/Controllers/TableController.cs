using MiniFootballStatistic.Models.Table;
using MiniFootballStatistic.Services.Api;

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

        public IActionResult Dynamic(TableCreateModel model)
        {

            var team = apiService.FindTeam(model.TournamentId, model.TeamId);

            if (team is null)
            {
                return BadRequest();
            }

            var players = apiService.GetTeams(team);

            return Ok(players);
        }
    }
}
