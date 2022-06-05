using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniFootballStatistic.Services.Schema;

namespace MiniFootballStatistic.Controllers
{
    public class ChampionshipController : Controller
    {
        private readonly ISchemaService schemaService;

        public ChampionshipController(ISchemaService schemaService)
        {
            this.schemaService = schemaService;
        }

        [Authorize]
        public IActionResult FirstStep()
        {
            var model = schemaService.GetSchemas();

            return View(model);
        }
    }
}
