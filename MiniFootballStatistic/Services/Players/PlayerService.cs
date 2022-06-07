using MiniFootballStatistic.Data;
using MiniFootballStatistic.Data.Models;
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

        public void CreatePlayers(TeamPlayerPostModel model, int modelId)
        {
            ICollection<Player>? players = null;

            Task.Run(() =>
            {
                //players = model.Players
                //.Where(p => p.Name != null)
                //.Select(p => new Player
                //{
                //    Name = p.Name,
                //    TeamId = modelId,
                //    Assists = 0,
                //    Goals = 0,
                //    MachesCount = 0,
                //})
                //.ToList();               

            }).GetAwaiter().GetResult();

            AddPlayers(players);
        }       

        public TeamPlayerPostModel GetTeamByTeamId(string userId)
        {
            TeamPlayerPostModel? model = null;

            Task.Run(() =>
            {
                 model = this.data.Tournaments
                .Where(t => t.UserId == userId)
                .Select(t => new TeamPlayerPostModel 
                { 
                    Id = t.Id,
                    Name = t.Name,
                    TeamsCount = t.ShcemaLength
                })
                .FirstOrDefault();                

            }).GetAwaiter().GetResult();

            return model;
        }

        private void AddPlayers(ICollection<Player> players)
        {
            Task.Run(() =>
            {
                this.data.Player.AddRange(players);

                this.data.SaveChanges();

            }).GetAwaiter().GetResult();
        }
    }
}
