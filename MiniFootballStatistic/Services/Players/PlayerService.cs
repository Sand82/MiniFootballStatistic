using MiniFootballStatistic.Data;
using MiniFootballStatistic.Models.Players;

namespace MiniFootballStatistic.Services.Players
{
    public class PlayerService : IPlayerService
    {
        private readonly FoodballStatisticDbContext data;

        public PlayerService(FoodballStatisticDbContext data)
        {
            this.data = data;
        }

        public TeamPlayerPostModel GetTeamById(int teamId)
        {
            TeamPlayerPostModel? model = null;

            Task.Run(() =>
            {
               model = this.data.Team
                .Where(t => t.Id == teamId)
                .Select(t => new TeamPlayerPostModel 
                { 
                    Id = t.Id,
                    Name = t.Name,                    
                })
                .FirstOrDefault();                

            }).GetAwaiter().GetResult();

            return model;
        }
    }
}
