using MiniFootballStatistic.Data;
using MiniFootballStatistic.Models.Home;

namespace MiniFootballStatistic.Services.Home
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
            var tournamens = data.TournamentCategories.Select(c => new IndexViewModel 
                { 
                    Name = c.Name,
                    Descrioption = c.Descrioption,
                    ImageUrl = c.ImageUrl,

                }).ToList();
            
            return tournamens;
        }
    }
}
