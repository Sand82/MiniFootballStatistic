using Microsoft.EntityFrameworkCore;
using MiniFootballStatistic.Data;
using MiniFootballStatistic.Data.Models;

using MiniFootballStatistic.Models.ThirdPart;
using MiniFootballStatistic.Models.Tournament;

namespace MiniFootballStatistic.Services.Tournaments
{
    public class TournamentService : ITournamentService
    {
        private readonly FoodballStatisticDbContext data;

        public TournamentService(FoodballStatisticDbContext data)
        {
            this.data = data;
        }

        public void CreateChampionship(TournamentPostModel model, string userId)
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
                    TournamentId = tournament.Id,
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

        public List<TournamentViewModel> GetSchemas()
        {
            List<TournamentViewModel>? schemas = null;

            Task.Run(() => 
            { 
                schemas = this.data.Schemas.Select(x => new TournamentViewModel 
                { 
                    Name = x.Name,
                    ImageUrl = x.ImageUrl,
                    UserId = x.UserId,
                    PositionCount = x.PositionCount,

                }).ToList();

            }).GetAwaiter().GetResult();

            return schemas;
        }

        public TournamentCreatePlayersModel GetTeams(string userId)
        {
            TournamentCreatePlayersModel? model = null;

            Task.Run(() =>
            {
                model = this.data.Tournaments
                .Include(t => t.Teams)
                .ThenInclude(t => t.Players)
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.Id)
                .Select(t => new TournamentCreatePlayersModel
                {
                    Name = t.Name,
                    Teams = t.Teams.Select(te => new TeamViewModel
                    {
                        Name = te.Name,
                        Id = te.Id,                        
                    })
                    .ToList()                    
                })
                .FirstOrDefault();            
               
            }).GetAwaiter().GetResult();

            return model;
        }
    }
}
