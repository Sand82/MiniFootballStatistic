using MiniFootballStatistic.Data;
using MiniFootballStatistic.Data.Models;
using MiniFootballStatistic.Models.Championship;
using System.Collections.Generic;

namespace MiniFootballStatistic.Services.Championship
{
    public class ChampionshipService  : IChampionshipService
    {
        private readonly FoodballStatisticDbContext data;

        public ChampionshipService(FoodballStatisticDbContext data)
        {
            this.data = data;
        }

        public void CreateChampionship(ChampionshipPostModel model, string userId)
        {
            Tournament tournament = new();           

            Task.Run(() =>
            {
                tournament.UserId = userId;
                tournament.ShcemaLength = model.TournamentPositions;
                tournament.Name = model.Name;

                tournament.Teams = model.Teams.Select(t => new Team
                {
                    Name = t.Name,
                    TeamId = tournament.Id,
                    TournamentPosition = t.TournamentPosition,
                })
                .ToList();

            }).GetAwaiter().GetResult();

            AddToDatabase(tournament);
        }

        private void AddToDatabase(Tournament tournament)
        {
            Task.Run(() =>
            {
                this.data.Tournaments.Add(tournament);

                this.data.SaveChanges();

            }).GetAwaiter().GetResult();
        }

        public List<ChampionshipViewModel> GetSchemas()
        {
            List<ChampionshipViewModel>? schemas = null;

            Task.Run(() => 
            { 
                schemas = this.data.Schemas.Select(x => new ChampionshipViewModel 
                { 
                    Name = x.Name,
                    ImageUrl = x.ImageUrl,
                    UserId = x.UserId,
                    PositionCount = x.PositionCount,

                }).ToList();

            }).GetAwaiter().GetResult();

            return schemas;
        }
    }
}
