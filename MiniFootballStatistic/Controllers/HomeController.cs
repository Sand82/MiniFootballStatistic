﻿using MiniFootballStatisticServices.Models;
using MiniFootballStatisticServices.Services.Home;

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MiniFootballStatistic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IHomeService homeService;

        public HomeController(ILogger<HomeController> logger, IHomeService homeService)
        {
            this.logger = logger;
            this.homeService = homeService;
        }

        public async Task<IActionResult> Index()
        {
           var model = await homeService.GetTournaments();

            return View(model);
        }

        public IActionResult Unavailable()
        {           
            return View();
        }
               
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}