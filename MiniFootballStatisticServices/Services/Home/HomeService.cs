using Microsoft.EntityFrameworkCore;
using MiniFootballStatisticData.Data;
using MiniFootballStatisticServices.Models.Home;

namespace MiniFootballStatisticServices.Services.Home
{
    public class HomeService : IHomeService
    {
        private readonly FoodballStatisticDbContext data;

        public HomeService(FoodballStatisticDbContext data)
        {            
            this.data = data;
        }

        public async Task<IEnumerable<IndexViewModel>> GetTournaments()
        {
            var tournamens =await data.TournamentCategories.Select(c => new IndexViewModel 
                { 
                    Name = c.Name,
                    Descrioption = c.Descrioption,
                    ImageUrl = c.ImageUrl,

                }).ToListAsync();
            
            return tournamens;
        }
    }
}
